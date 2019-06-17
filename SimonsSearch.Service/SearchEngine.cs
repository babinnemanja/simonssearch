using System.Collections.Generic;
using System.Linq;
using SimonsSearch.Service.DataModels;
using SimonsSearch.Service.Interfaces;

namespace SimonsSearch.Service
{
    public class SearchEngine : ISearchEngine
    {
        private readonly ISearchEngineWeigthCalculator _searchEngineWeigthCalculator;
        private static DataFile _data;

        public SearchEngine(ISearchRepository searchRepository, ISearchEngineWeigthCalculator searchEngineWeigthCalculator)
        {
            _searchEngineWeigthCalculator = searchEngineWeigthCalculator;

            if(_data == null)
            {
                _data = searchRepository.LoadData();
            }
        }

        public IReadOnlyList<SearchResult> GetSearchResult(string term)
        {
            var termSanitized = term.Trim().ToLowerInvariant();

            var searchResults = ProcessBuildingsAndLocks(termSanitized);
            
            return searchResults.OrderByDescending(o => o.Weight).ToList();
        }

        private List<SearchResult> ProcessBuildingsAndLocks(string term)
        {
            var searchResults = new List<SearchResult>();

            foreach (var building in _data.Buildings.Where(w => w.ToString().Contains(term)))
           {
               //add and boost entity 
               searchResults.Add(_searchEngineWeigthCalculator.ToSearchResult(building, term));

               //add and boost all the child entities 
               searchResults.AddRange(_data.Locks.Where(w => w.BuildingId == building.Id)
                   .Select(lck => _searchEngineWeigthCalculator.ToTransientSearchResult(lck, building, term)));
           }

            ProcessLocks(term, searchResults);

            return searchResults;
        }

        private void ProcessLocks(string term, List<SearchResult> searchResults)
        {
            foreach(var lck in _data.Locks.Where(w => w.ToString().Contains(term) && !searchResults.Select(s => s.Id).Contains(w.Id)))
            {
                searchResults.Add(_searchEngineWeigthCalculator.ToSearchResult(lck, term));
            }
        }
    }
}
