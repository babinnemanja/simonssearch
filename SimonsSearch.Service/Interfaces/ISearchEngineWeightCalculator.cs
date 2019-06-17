using SimonsSearch.Service.DataModels;

namespace SimonsSearch.Service.Interfaces
{
    public interface ISearchEngineWeightCalculator
    {
        SearchResult ToSearchResult(Building building, string term);
        SearchResult ToSearchResult(Lock lck, string term);
        SearchResult ToSearchResult(Group group, string term);
        SearchResult ToSearchResult(Media media, string term);
        SearchResult ToTransientSearchResult(Lock lck, Building building, string term);
        SearchResult ToTransientSearchResult(Media media, Group group, string term);
    }
}
