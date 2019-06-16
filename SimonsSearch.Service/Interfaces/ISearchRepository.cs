using SimonsSearch.Service.DataModels;

namespace SimonsSearch.Service.Interfaces
{
    public interface ISearchRepository
    {
        DataFile LoadData();
    }
}
