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
        private ISearchEngineWeightCalculator _searchEngineWeigthCalculator;
        private ISearchRepository _searchRepository;

        private static Guid _buildingId = Guid.NewGuid();
        private static Guid _groupId = Guid.NewGuid();

        private static DataFile _dataFile = new DataFile
        {
            Buildings = new List<Building> { new Building(_buildingId, "BOne", "Unit test builiding", "This is unit test building") },
            Locks = new List<Lock>
                {
                    new Lock(Guid.NewGuid(), _buildingId, "Lock", "Test Lock", "This is unit test lock", "T123T", "12", "13"),
                    new Lock(Guid.NewGuid(), _buildingId, "Lock", "Test Lock", "This is unit test lock", "T123T", "12", "13"),
                    new Lock(Guid.NewGuid(), Guid.NewGuid(), "Lock", "Random Lock", "This is random lock", "T123T", "12", "13")
                },
            Groups = new List<Group> { new Group(_groupId, "Term Group", "Term group") },
            Media = new List<Media>
                {
                    new Media(Guid.NewGuid(), _groupId, "Media", "Term owner", "This is term media", "M123"),
                    new Media(Guid.NewGuid(), _groupId, "Media", "Term owner", "This is term media", "M123"),
                    new Media(Guid.NewGuid(), Guid.NewGuid(), "Media", "Random owner", "This is random media", "M123")
                }
        };

        [SetUp]
        public void SetUp()
        {
            _searchEngineWeigthCalculator = Substitute.For<ISearchEngineWeightCalculator>();
            _searchRepository = Substitute.For<ISearchRepository>();
            _searchRepository.LoadData().Returns(_dataFile);
        }

        [Test]
        public void When_GetSearchResult_Buliding_Lock_Called_It_Should_Return_Correct_Result()
        {
            var term = "test";

            var building = _dataFile.Buildings.FirstOrDefault();
            var lockOne = _dataFile.Locks[0];
            var lockTwo = _dataFile.Locks[1];
            var lockThree = _dataFile.Locks[2]; 

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

            var result = searchEngine.GetSearchResult(term);

            Assert.IsNotNull(result);
            _searchEngineWeigthCalculator.Received(1).ToTransientSearchResult(lockOne, building, term);
            _searchEngineWeigthCalculator.Received(1).ToTransientSearchResult(lockTwo, building, term);
            _searchEngineWeigthCalculator.Received(0).ToTransientSearchResult(lockThree, building, term);
            _searchEngineWeigthCalculator.Received(1).ToSearchResult(building, term);
            Assert.AreEqual(3, result.Count());
        }

        [Test]
        public void When_GetSearchResult_Group_Media_Called_It_Should_Return_Correct_Result()
        {
            var term = "term";

            var group = _dataFile.Groups.FirstOrDefault();
            var mediaOne = _dataFile.Media[0];
            var mediaTwo = _dataFile.Media[1];
            var mediaThree = _dataFile.Media[2];

            _searchRepository.LoadData().Returns(_dataFile);

            var searchEngine = new SearchEngine(_searchRepository, _searchEngineWeigthCalculator);

            _searchEngineWeigthCalculator.ToSearchResult(group, term).Returns(new SearchResult
            {
                Id = group.Id,
                Description = group.Description,
                Name = group.Name,
                Weight = 15
            });

            _searchEngineWeigthCalculator.ToTransientSearchResult(mediaOne, group, term).Returns(new SearchResult
            {
                Id = mediaOne.Id,
                Description = mediaOne.Description,
                Name = string.Empty,
                Weight = 55
            });

            _searchEngineWeigthCalculator.ToTransientSearchResult(mediaTwo, group, term).Returns(new SearchResult
            {
                Id = mediaTwo.Id,
                Description = mediaTwo.Description,
                Name = string.Empty,
                Weight = 55
            });

            _searchEngineWeigthCalculator.ToTransientSearchResult(mediaThree, group, term).Returns(new SearchResult
            {
                Id = mediaThree.Id,
                Description = mediaThree.Description,
                Name = string.Empty,
                Weight = 55
            });

            var result = searchEngine.GetSearchResult(term);

            Assert.IsNotNull(result);
            _searchEngineWeigthCalculator.Received(1).ToTransientSearchResult(mediaOne, group, term);
            _searchEngineWeigthCalculator.Received(1).ToTransientSearchResult(mediaTwo, group, term);
            _searchEngineWeigthCalculator.Received(0).ToTransientSearchResult(mediaThree, group, term);
            _searchEngineWeigthCalculator.Received(1).ToSearchResult(group, term);
            Assert.AreEqual(3, result.Count());
        }
    }
}
