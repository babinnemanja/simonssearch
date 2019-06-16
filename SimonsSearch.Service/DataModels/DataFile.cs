using System.Collections.Generic;

namespace SimonsSearch.Service.DataModels
{
    public class DataFile
    {
        public HashSet<Building> Buildings { get; set; }
        public HashSet<Lock> Locks { get; set; }
        public HashSet<Group> Groups { get; set; }
        public HashSet<Media> Media { get; set; }
    }
}
