using System;

namespace SimonsSearch.Service.DataModels
{
    public class Building
    {
        public Guid Id { get; set; }
        public string ShortCut { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return $"{ShortCut.ToLowerInvariant()} {Name.ToLowerInvariant()} {Description.ToLowerInvariant()}";
        }
    }
}
