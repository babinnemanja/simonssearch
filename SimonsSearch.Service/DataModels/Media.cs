using System;

namespace SimonsSearch.Service.DataModels
{
    public class Media
    {
        public Guid Id { get; set; }
        public Guid GroupId { get; set; }
        public string Type { get; set; }
        public string Owner { get; set; }
        public string Description { get; set; }
        public string SerialNumber { get; set; }

        public override string ToString()
        {
            return $"{Type.ToLowerInvariant()} {Owner.ToLowerInvariant()} {Description.ToLowerInvariant()} {SerialNumber.ToLowerInvariant()}";
        }
    }
}
