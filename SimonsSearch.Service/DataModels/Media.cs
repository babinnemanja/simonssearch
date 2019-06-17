using System;

namespace SimonsSearch.Service.DataModels
{
    public class Media
    {
        private string _cache = string.Empty;

        public Media(Guid id, Guid groupId, string type, string owner, string description, string serialNumber)
        {
            Id = id;
            GroupId = groupId;
            Type = type ?? string.Empty;
            Owner = owner ?? string.Empty;
            Description = description ?? string.Empty;
            SerialNumber = serialNumber ?? string.Empty;
        }

        public Guid Id { get; private set; }
        public Guid GroupId { get; private set; }
        public string Type { get; private set; }
        public string Owner { get; private set; }
        public string Description { get; private set; }
        public string SerialNumber { get; private set; }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(_cache))
            {
                _cache = $"{Type.ToLowerInvariant()} {Owner.ToLowerInvariant()} {Description.ToLowerInvariant()} {SerialNumber.ToLowerInvariant()}";
            }

            return _cache;
        }
    }
}
