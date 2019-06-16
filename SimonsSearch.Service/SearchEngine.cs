using System.Collections.Generic;
using System.Linq;
using SimonsSearch.Service.DataModels;
using SimonsSearch.Service.Interfaces;

namespace SimonsSearch.Service
{
    public class SearchEngine : ISearchEngine
    {
        private readonly ISearchRepository _searchRepository;
        private readonly SearchEngineWeigthCalculator _searchEngineWeigthCalculator = new SearchEngineWeigthCalculator();

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

        private List<SearchResult> GetBuildings(string term, DataFile data)
        {
           var searchResult = new List<SearchResult>();

           foreach(var building in data.Buildings)
            {
                if (building.ToString().Contains(term))
                {
                    searchResult.Add(_searchEngineWeigthCalculator.ToSearchResult(building, term));
                }
            }

            return searchResult;


        }

        private void CalculateBoost(List<SearchResult> searchResults) 
        {

        }
    }
}
