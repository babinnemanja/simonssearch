using NUnit.Framework;
using SimonsSearch.Service.DataModels;
using System;

namespace SimonsSearch.Service.Test
{
    [TestFixture]
    public class SearchEngineWeightCalculatorTests
    {
        [Test]
        public void When_Building_Matched_On_One_Property_Weigth_Should_Be_Calculated()
        {
            var building = new Building(Guid.NewGuid(), "TB", "Test Building", "This is unit test building");

            var calculator = new SearchEngineWeigthCalculator();

            var searchResult = calculator.ToSearchResult(building, "unit");

            Assert.IsNotNull(searchResult);
            Assert.AreEqual(building.Description, searchResult.Description);
            Assert.AreEqual(building.Name, searchResult.Name);
            Assert.AreEqual(5, searchResult.Weight);
        }

        [Test]
        public void When_Building_Matched_On_More_Then_One_Property_Weigth_Should_Be_Calculated()
        {
            var building = new Building(Guid.NewGuid(), "TB", "Test Building", "This is unit test building");

            var calculator = new SearchEngineWeigthCalculator();

            var searchResult = calculator.ToSearchResult(building, "test");

            Assert.IsNotNull(searchResult);
            Assert.AreEqual(building.Description, searchResult.Description);
            Assert.AreEqual(building.Name, searchResult.Name);
            Assert.AreEqual(14, searchResult.Weight);
        }

        [Test]
        public void When_Lock_Matched_On_One_Property_Weigth_Should_Be_Calculated()
        {
            var lck = new Lock(Guid.NewGuid(), Guid.NewGuid(), "Lock", "Test Lock", "This is unit test building", "T123T", "12", "13");

            var calculator = new SearchEngineWeigthCalculator();

            var searchResult = calculator.ToSearchResult(lck, "unit");

            Assert.IsNotNull(searchResult);
            Assert.AreEqual(lck.Description, searchResult.Description);
            Assert.AreEqual(lck.Name, searchResult.Name);
            Assert.AreEqual(6, searchResult.Weight);
        }

        [Test]
        public void When_Lock_Matched_On_More_Then_One_Property_Weigth_Should_Be_Calculated()
        {
            var lck = new Lock(Guid.NewGuid(), Guid.NewGuid(), "Lock", "Test Lock", "This is unit test lock", "T123T", "12", "13");

            var calculator = new SearchEngineWeigthCalculator();

            var searchResult = calculator.ToSearchResult(lck, "lock");

            Assert.IsNotNull(searchResult);
            Assert.AreEqual(lck.Description, searchResult.Description);
            Assert.AreEqual(lck.Name, searchResult.Name);
            Assert.AreEqual(46, searchResult.Weight);
        }

        [Test]
        public void When_Lock_Matched_As_Transient_On_Zero_Properties_Weigth_Should_Be_Calculated()
        {
            var building = new Building(Guid.NewGuid(), "TB", "Test Building", "This is unit test building");
            var lck = new Lock(Guid.NewGuid(), building.Id, "Lock", "Test Lock", "This is unit test lock", "T123T", "12", "13");

            var calculator = new SearchEngineWeigthCalculator();

            var searchResult = calculator.ToTransientSearchResult(lck, building, "building");

            Assert.IsNotNull(searchResult);
            Assert.AreEqual(lck.Description, searchResult.Description);
            Assert.AreEqual(lck.Name, searchResult.Name);
            Assert.AreEqual(8, searchResult.Weight);
        }

        [Test]
        public void When_Lock_Matched_As_Transient_On_Owne_Properties_Weigth_Should_Be_Calculated()
        {
            var building = new Building(Guid.NewGuid(), "TB", "Test Building", "This is unit test building");
            var lck = new Lock(Guid.NewGuid(), building.Id, "Lock", "Test Lock", "This is unit test lock", "T123T", "12", "13");

            var calculator = new SearchEngineWeigthCalculator();

            var searchResult = calculator.ToTransientSearchResult(lck, building, "test");

            Assert.IsNotNull(searchResult);
            Assert.AreEqual(lck.Description, searchResult.Description);
            Assert.AreEqual(lck.Name, searchResult.Name);
            Assert.AreEqual(24, searchResult.Weight);
        }
    }
}
