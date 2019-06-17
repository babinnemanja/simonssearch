using System.Collections.Generic;
using System.Linq;
using SimonsSearch.Service.DataModels;
using SimonsSearch.Service.Interfaces;

namespace SimonsSearch.Service
{
    public class SearchEngine : ISearchEngine
    {
        private readonly ISearchEngineWeightCalculator _searchEngineWeigthCalculator;
        private static DataFile _data;

        public SearchEngine(ISearchRepository searchRepository, ISearchEngineWeightCalculator searchEngineWeigthCalculator)
        {
            _searchEngineWeigthCalculator = searchEngineWeigthCalculator;

            if (_data == null)
            {
                _data = searchRepository.LoadData();
            }
        }

        public IReadOnlyList<SearchResult> GetSearchResult(string term)
        {
            var termSanitized = term.Trim().ToLowerInvariant();

            var searchResults = ProcessBuildingsAndLocks(termSanitized);
            searchResults.AddRange(ProcessGroupsAndMedia(termSanitized));

            return searchResults.OrderByDescending(o => o.Weight).ToList();
        }

        private List<SearchResult> ProcessBuildingsAndLocks(string term)
        {
            var searchResults = new List<SearchResult>();

            if (_data.Buildings == null) { return searchResults; }

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

        private List<SearchResult> ProcessGroupsAndMedia(string term)
        {
            var searchResults = new List<SearchResult>();

            if (_data.Groups == null) { return searchResults; }

            foreach (var group in _data.Groups.Where(w => w.ToString().Contains(term)))
            {
                //add and boost entity 
                searchResults.Add(_searchEngineWeigthCalculator.ToSearchResult(group, term));

                //add and boost all the child entities 
                searchResults.AddRange(_data.Media.Where(w => w.GroupId == group.Id)
                    .Select(media => _searchEngineWeigthCalculator.ToTransientSearchResult(media, group, term)));
            }

            ProcessMedia(term, searchResults);

            return searchResults;
        }

        private void ProcessLocks(string term, List<SearchResult> searchResults)
        {
            if (_data.Locks == null) { return; }

            foreach (var lck in _data.Locks.Where(w => w.ToString().Contains(term) && !searchResults.Select(s => s.Id).Contains(w.Id)))
            {
                searchResults.Add(_searchEngineWeigthCalculator.ToSearchResult(lck, term));
            }
        }

        private void ProcessMedia(string term, List<SearchResult> searchResults)
        {
            if (_data.Media == null) { return; }

            foreach (var media in _data.Media.Where(w => w.ToString().Contains(term) && !searchResults.Select(s => s.Id).Contains(w.Id)))
            {
                searchResults.Add(_searchEngineWeigthCalculator.ToSearchResult(media, term));
            }
        }
    }
}
