using SimonsSearch.Service.Constants;
using SimonsSearch.Service.DataModels;
using SimonsSearch.Service.Interfaces;

namespace SimonsSearch.Service
{
    public class SearchEngineWeightCalculator : ISearchEngineWeightCalculator
    {
        public SearchResult ToSearchResult(Building building, string term)
        {
            var weigth = CalculateWeigth(building.Name, term, new PropertyDefinition(nameof(Building.Name), nameof(Building)));
            weigth += CalculateWeigth(building.Description, term, new PropertyDefinition(nameof(Building.Description), nameof(Building)));
            weigth += CalculateWeigth(building.ShortCut, term, new PropertyDefinition(nameof(Building.ShortCut), nameof(Building)));

            return new SearchResult
            {
                Id = building.Id,
                Name = building.Name,
                Description = building.Description,
                Weight = weigth,
                EntityType = EntityType.Building.ToString()
            };

        }

        public SearchResult ToSearchResult(Lock @lock, string term)
        {
            return new SearchResult
            {
                Id = @lock.Id,
                Name = @lock.Name,
                Description = @lock.Description,
                Weight = CalculateLockWeight(@lock, term),
                EntityType = EntityType.Lock.ToString(),
                SerialNumber = @lock.SerialNumber,
                Floor = @lock.Floor,
                RoomNumber = @lock.RoomNumber,
                Type = @lock.Type
            };
        }

        public SearchResult ToTransientSearchResult(Lock @lock, Building building, string term)
        {
            var weigth = CalculateTransientWeigth(building.Name, term, new PropertyDefinition(nameof(Building.Name), nameof(Building)));
            weigth += CalculateTransientWeigth(building.Description, term, new PropertyDefinition(nameof(Building.Description), nameof(Building)));
            weigth += CalculateTransientWeigth(building.ShortCut, term, new PropertyDefinition(nameof(Building.ShortCut), nameof(Building)));

            weigth += CalculateLockWeight(@lock, term);

            return new SearchResult
            {
                Id = @lock.Id,
                Description = @lock.Description,
                Name = @lock.Name,
                Weight = weigth,
                SerialNumber = @lock.SerialNumber,
                EntityType = EntityType.Lock.ToString(),
                Floor = @lock.Floor,
                RoomNumber = @lock.RoomNumber,
                Type = @lock.Type
            };
        }

        public SearchResult ToSearchResult(Group group, string term)
        {
            var weigth = CalculateWeigth(group.Name, term, new PropertyDefinition(nameof(Group.Name), nameof(Group)));
            weigth += CalculateWeigth(group.Description, term, new PropertyDefinition(nameof(Group.Description), nameof(Group)));

            return new SearchResult
            {
                Id = group.Id,
                Description = group.Description,
                Name = group.Name,
                Weight = weigth,
                EntityType = EntityType.Group.ToString()
            };
        }

        public SearchResult ToSearchResult(Media media, string term)
        {         
            return new SearchResult
            {
                Id = media.Id,
                Description = media.Description,
                Name = string.Empty,
                Weight = CalculateMediaWeight(media, term),
                EntityType = EntityType.Media.ToString(),
                SerialNumber = media.SerialNumber,
                Owner = media.Owner,
                Type = media.Type
            };
        }

        public SearchResult ToTransientSearchResult(Media media, Group group,string term)
        {
            var weigth = CalculateTransientWeigth(group.Name, term, new PropertyDefinition(nameof(Group.Name), nameof(Group)));
            weigth += CalculateTransientWeigth(group.Description, term, new PropertyDefinition(nameof(Group.Description), nameof(Group)));

            weigth += CalculateMediaWeight(media, term);

            return new SearchResult
            {
                Id = media.Id,
                Description = media.Description,
                Name = string.Empty,
                Weight = weigth,
                EntityType = EntityType.Media.ToString(),
                SerialNumber = media.SerialNumber,
                Owner = media.Owner,
                Type = media.Type
            };
        }

        private int CalculateLockWeight(Lock lck, string term)
        {
            var weigth = CalculateWeigth(lck.Name, term, new PropertyDefinition(nameof(Lock.Name), nameof(Lock)));
            weigth += CalculateWeigth(lck.Description, term, new PropertyDefinition(nameof(Lock.Description), nameof(Lock)));
            weigth += CalculateWeigth(lck.Floor, term, new PropertyDefinition(nameof(Lock.Floor), nameof(Lock)));
            weigth += CalculateWeigth(lck.RoomNumber, term, new PropertyDefinition(nameof(Lock.RoomNumber), nameof(Lock)));
            weigth += CalculateWeigth(lck.SerialNumber, term, new PropertyDefinition(nameof(Lock.SerialNumber), nameof(Lock)));
            weigth += CalculateWeigth(lck.Type, term, new PropertyDefinition(nameof(Lock.Type), nameof(Lock)));

            return weigth;
        }

        private int CalculateMediaWeight(Media media, string term)
        {
            var weigth = CalculateWeigth(media.Owner, term, new PropertyDefinition(nameof(Media.Owner), nameof(Media)));
            weigth += CalculateWeigth(media.Description, term, new PropertyDefinition(nameof(Media.Description), nameof(Media)));
            weigth += CalculateWeigth(media.SerialNumber, term, new PropertyDefinition(nameof(Media.SerialNumber), nameof(Media)));
            weigth += CalculateWeigth(media.Type, term, new PropertyDefinition(nameof(Media.Type), nameof(Media)));

            return weigth;
        }

        private int CalculateWeigth(string propValue, string term, PropertyDefinition propDefinition)
        {
            var propValueLower = propValue?.ToLowerInvariant();

            if (string.IsNullOrEmpty(propValueLower) || !propValueLower.Contains(term)) { return 0; }

            if (PropertyWeights.PopertyWeigths.TryGetValue(propDefinition, out int propWeigth))
            {
                return propValueLower == term ? propWeigth * 10 : propWeigth;
            }

            return 0;
        }

        private int CalculateTransientWeigth(string propValue, string term, PropertyDefinition propDefinition)
        {
          
            if (string.IsNullOrEmpty(propValue) || !propValue.ToLowerInvariant().Contains(term)) { return 0; }
           
            if (PropertyWeights.TransientPropertyWeigths.TryGetValue(propDefinition, out int propWeigth))
            {
                return propWeigth;
            }

            return 0;
        }
    }
}
