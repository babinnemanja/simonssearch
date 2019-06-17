using System;

namespace SimonsSearch.Service.DataModels
{
    public class Building
    {
        private string _cache = string.Empty;

        public Building(Guid id, string shortCut, string name, string description)
        {
            Id = id;
            ShortCut = shortCut ?? string.Empty;
            Name = name ?? string.Empty;
            Description = description ?? string.Empty;
        }

        public Guid Id { get; private set; }
        public string ShortCut { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(_cache))
            {
               _cache = $"{ShortCut.ToLowerInvariant()} {Name.ToLowerInvariant()} {Description.ToLowerInvariant()}";
            }

            return _cache;
        }
    }
}
