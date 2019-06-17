using SimonsSearch.Service.DataModels;

namespace SimonsSearch.Service.Interfaces
{
    public interface ISearchEngineWeigthCalculator
    {
        SearchResult ToSearchResult(Building building, string term);
        SearchResult ToSearchResult(Lock lck, string term);
        SearchResult ToTransientSearchResult(Lock lck, Building building, string term);
    }
}
