using System;
using System.Collections.Generic;
using System.Text;

namespace SimonsSearch.Service.DataModels
{
    public class SearchResult
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public int Weight { get; set; }
    }
}
