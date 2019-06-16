using System;

namespace SimonsSearch.Service.DataModels
{
    public class Group
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public override string ToString()
        {
            return $"{Name.ToLowerInvariant()} {Description.ToLowerInvariant()}";
        }
    }
}
