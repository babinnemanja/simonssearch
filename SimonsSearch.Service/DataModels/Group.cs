using System;

namespace SimonsSearch.Service.DataModels
{
    public class Group
    {
        private string _cache = string.Empty;

        public Group(Guid id, string name, string description)
        {
            Id = id;
            Name = name ?? string.Empty;
            Description = description ?? string.Empty;
        }

        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(_cache))
            {
                _cache = $"{Name.ToLowerInvariant()} {Description.ToLowerInvariant()}";
            }
            return _cache;
        }
    }
}
