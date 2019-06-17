using SimonsSearch.Service.DataModels;
using System.Collections.Generic;

namespace SimonsSearch.Service.Interfaces
{
    public interface ISearchEngine
    {
        IReadOnlyList<SearchResult> GetSearchResult(string term);
    }
}
