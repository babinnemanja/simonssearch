using System;

namespace SimonsSearch.Service.DataModels
{
    public class Lock
    {
        private string _cache = string.Empty;

        public Lock(Guid id, Guid buildingId, string type, string name, string description, string serialNumber, string floor, string roomNumber)
        {
            Id = id;
            BuildingId = buildingId;
            Type = type ?? string.Empty;
            Name = name ?? string.Empty;
            Description = description ?? string.Empty;
            SerialNumber = serialNumber ?? string.Empty;
            Floor = floor ?? string.Empty;
            RoomNumber = roomNumber ?? string.Empty;
        }

        public Guid Id { get; private set; }
        public Guid BuildingId { get; private set; }
        public string Type { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string SerialNumber { get; private set; }
        public string Floor { get; private set; }
        public string RoomNumber { get; private set; }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(_cache))
            {
                _cache = $"{Type.ToLowerInvariant()} {Name.ToLowerInvariant()} {Description.ToLowerInvariant()} {SerialNumber.ToLowerInvariant()} {Floor.ToLowerInvariant()} {RoomNumber.ToLowerInvariant()}";
            }

            return _cache;
        }

    }
}
