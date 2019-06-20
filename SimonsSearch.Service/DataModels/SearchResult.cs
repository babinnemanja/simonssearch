using SimonsSearch.Service.Constants;
using System;

namespace SimonsSearch.Service.DataModels
{
    public class SearchResult
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public int Weight { get; set; }
        public string EntityType { get; set; }
        public string SerialNumber { get; set; }
    }
}
