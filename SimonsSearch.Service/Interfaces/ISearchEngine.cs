using SimonsSearch.Service.DataModels;
using System.Collections.Generic;

namespace SimonsSearch.Service.Interfaces
{
    public interface ISearchEngine
    {
        List<SearchResult> GetSearchResult(string term);
    }
}
