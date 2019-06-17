using NSubstitute;
using NUnit.Framework;
using SimonsSearch.Service.DataModels;
using SimonsSearch.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SimonsSearch.Service.Test
{
    [TestFixture]
    public class SearchEngineTests
    {
        private ISearchEngineWeigthCalculator _searchEngineWeigthCalculator;
        private ISearchRepository _searchRepository;

        [SetUp]
        public void SetUp()
        {
            _searchEngineWeigthCalculator = Substitute.For<ISearchEngineWeigthCalculator>();
            _searchRepository = Substitute.For<ISearchRepository>();
        }

        [Test]
        public void When_GetSearchResult_Called_It_Should_Return_Correct_Result()
        {
            var bulidingId = Guid.NewGuid();
            var term = "test";

            var data = new DataFile
            {
                Buildings = new List<Building> { new Building(bulidingId, "BOne", "Unit test builiding", "This is unit test building")},
                Locks = new List<Lock>
                {
                    new Lock(Guid.NewGuid(), bulidingId, "Lock", "Test Lock", "This is unit test lock", "T123T", "12", "13"),
                    new Lock(Guid.NewGuid(), bulidingId, "Lock", "Test Lock", "This is unit test lock", "T123T", "12", "13"),
                    new Lock(Guid.NewGuid(), Guid.NewGuid(), "Lock", "Random Lock", "This is random lock", "T123T", "12", "13")
                }
            };

            var building = data.Buildings.FirstOrDefault();
            var lockOne = data.Locks[0];
            var lockTwo = data.Locks[1];
            var lockThree = data.Locks[2];

            _searchRepository.LoadData().Returns(data);

            var searchEngine = new SearchEngine(_searchRepository, _searchEngineWeigthCalculator);

            _searchEngineWeigthCalculator.ToSearchResult(building, term).Returns(new SearchResult {
                Id = building.Id,
                Description = building.Description,
                Name = building.Name,
                Weight = 15
            });

            _searchEngineWeigthCalculator.ToTransientSearchResult(lockOne, building, term).Returns(new SearchResult
            {
                Id = lockOne.Id,
                Description = lockOne.Description,
                Name = lockOne.Name,
                Weight = 55
            });

            _searchEngineWeigthCalculator.ToTransientSearchResult(lockTwo, building, term).Returns(new SearchResult
            {
                Id = lockTwo.Id,
                Description = lockTwo.Description,
                Name = lockTwo.Name,
                Weight = 55
            });

            _searchEngineWeigthCalculator.ToTransientSearchResult(lockThree, building, term).Returns(new SearchResult
            {
                Id = lockThree.Id,
                Description = lockThree.Description,
                Name = lockThree.Name,
                Weight = 55
            });

            var result = searchEngine.GetSearchResult("test");

            Assert.IsNotNull(result);
            _searchEngineWeigthCalculator.Received(1).ToTransientSearchResult(lockOne, building, term);
            _searchEngineWeigthCalculator.Received(1).ToTransientSearchResult(lockTwo, building, term);
            _searchEngineWeigthCalculator.Received(0).ToTransientSearchResult(lockThree, building, term);
            _searchEngineWeigthCalculator.Received(1).ToSearchResult(building, term);
            Assert.AreEqual(3, result.Count());
        }
    }
}
