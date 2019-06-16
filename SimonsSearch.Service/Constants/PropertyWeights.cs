using SimonsSearch.Service.DataModels;
using System.Collections.Generic;

namespace SimonsSearch.Service.Constants
{
    public static class PropertyWeights
    {
        public static Dictionary<PropertyDefinition, int> PopertyWeigths = new Dictionary<PropertyDefinition, int> {
            { new PropertyDefinition(nameof(Building.ShortCut), nameof(Building)), 7 },
            { new PropertyDefinition(nameof(Building.Name), nameof(Building)), 9},
            { new PropertyDefinition(nameof(Building.Description),nameof(Building)), 5},
            { new PropertyDefinition(nameof(Lock.Type), nameof(Lock)), 3 },
            { new PropertyDefinition(nameof(Lock.Name), nameof(Lock)), 10},
            { new PropertyDefinition(nameof(Lock.SerialNumber), nameof(Lock)), 8},
            { new PropertyDefinition(nameof(Lock.Floor), nameof(Lock)), 6 },
            { new PropertyDefinition(nameof(Lock.RoomNumber), nameof(Lock)), 6},
            { new PropertyDefinition(nameof(Lock.Description), nameof(Lock)), 6},
            { new PropertyDefinition(nameof(Group.Name), nameof(Group)), 9},
            { new PropertyDefinition(nameof(Group.Description), nameof(Group)), 5},
            { new PropertyDefinition(nameof(Media.Type), nameof(Media)), 3 },
            { new PropertyDefinition(nameof(Media.Owner), nameof(Media)), 10},
            { new PropertyDefinition(nameof(Media.SerialNumber), nameof(Media)), 8},
            { new PropertyDefinition(nameof(Media.Description),  nameof(Media)), 6}
        };

        public static Dictionary<PropertyDefinition, int> TransientPropertyWeigths = new Dictionary<PropertyDefinition, int> {
            { new PropertyDefinition(nameof(Building.ShortCut), nameof(Building)), 5 },
            { new PropertyDefinition(nameof(Building.Name), nameof(Building)), 8},
            { new PropertyDefinition(nameof(Building.Description), nameof(Building)), 0},
            { new PropertyDefinition(nameof(Group.Name), nameof(Group)), 8},
            { new PropertyDefinition(nameof(Group.Description), nameof(Group)), 0}
        };

    }
}
