using System.Collections.Generic;
using System.Linq;
using SimonsSearch.Service.DataModels;
using SimonsSearch.Service.Interfaces;

namespace SimonsSearch.Service
{
    public class SearchEngine : ISearchEngine
    {
        private readonly ISearchRepository _searchRepository;

        public SearchEngine(ISearchRepository searchRepository)
        {
            _searchRepository = searchRepository;
        }

        public List<SearchResult> GetSearchResult(string term)
        {
            var termSanitized = term.Trim().ToLowerInvariant();
            var result = new List<SearchResult>();

            var data = _searchRepository.LoadData();


            return result;
        }
    }
}
