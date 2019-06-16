using System.Collections.Generic;
using System.Linq;
using SimonsSearch.Service.DataModels;
using SimonsSearch.Service.Interfaces;

namespace SimonsSearch.Service
{
    public class SearchEngine : ISearchEngine
    {
        private readonly SearchEngineWeigthCalculator _searchEngineWeigthCalculator = new SearchEngineWeigthCalculator();
        private static DataFile _data;

        public SearchEngine(ISearchRepository searchRepository)
        {
            if(_data == null)
            {
                _data = searchRepository.LoadData();
            }
        }

        public List<SearchResult> GetSearchResult(string term)
        {
            var termSanitized = term.Trim().ToLowerInvariant();

            var searchResults = new List<SearchResult>();

            GetBuildings(termSanitized, searchResults);
            GetLocks(termSanitized, searchResults);

            return searchResults;
        }

        private void GetBuildings(string term, List<SearchResult> searchResults)
        {
           foreach(var building in _data.Buildings.Where(w => w.ToString().Contains(term)))
           {
               //add and boost entity 
               searchResults.Add(_searchEngineWeigthCalculator.ToSearchResult(building, term));

               //add and boost all the child entities 
               searchResults.AddRange(_data.Locks.Where(w => w.BuildingId == building.Id)
                   .Select(lck => _searchEngineWeigthCalculator.ToTransientSearchResult(lck, building, term)));
           }
        }

        private void GetLocks(string term, List<SearchResult> searchResults)
        {
            foreach(var lck in _data.Locks.Where(w => w.ToString().Contains(term)))
            {
                searchResults.Add(_searchEngineWeigthCalculator.ToSearchResult(lck, term));
            }
        }
    }
}
