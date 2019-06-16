using System;

namespace SimonsSearch.Service.DataModels
{
    public class Lock
    {
        public Guid Id { get; set; }
        public Guid BuildingId { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SerialNumber { get; set; }
        public string Floor { get; set; }
        public string RoomNumber { get; set; }

        public override string ToString()
        {
            return $"{Type.ToLowerInvariant()} {Name.ToLowerInvariant()} {Description.ToLowerInvariant()} {SerialNumber.ToLowerInvariant()} {Floor.ToLowerInvariant()} {RoomNumber.ToLowerInvariant()}";
        }

    }
}
