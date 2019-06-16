using SimonsSearch.Service.Constants;
using SimonsSearch.Service.DataModels;

namespace SimonsSearch.Service
{
    public class SearchEngineWeigthCalculator
    {
        public SearchResult ToSearchResult(Building building, string term)
        {
            var weigth = CalculateWeigth(building.Name.ToLowerInvariant(), term, new PropertyDefinition(nameof(Building.Name), nameof(Building)));
            weigth += CalculateWeigth(building.Description.ToLowerInvariant(), term, new PropertyDefinition(nameof(Building.Description), nameof(Building)));
            weigth += CalculateWeigth(building.ShortCut.ToLowerInvariant(), term, new PropertyDefinition(nameof(Building.ShortCut), nameof(Building)));

            return new SearchResult
            {
                Id = building.Id,
                Name = building.Name,
                Description = building.Description,
                Weight = weigth
            };

        }

        public SearchResult ToSearchResult(Lock lck, string term)
        {
            var weigth = CalculateLockWeigth(lck, term);

            return new SearchResult
            {
                Id = lck.Id,
                Name = lck.Name,
                Description = lck.Description,
                Weight = weigth
            };

        }

        public SearchResult ToTransientSearchResult(Lock lck, Building building, string term)
        {
            var weigth = CalculateTransientWeigth(building.Name.ToLowerInvariant(), term, new PropertyDefinition(nameof(Building.Name), nameof(Building)));
            weigth += CalculateTransientWeigth(building.Description.ToLowerInvariant(), term, new PropertyDefinition(nameof(Building.Description), nameof(Building)));
            weigth += CalculateTransientWeigth(building.ShortCut.ToLowerInvariant(), term, new PropertyDefinition(nameof(Building.ShortCut), nameof(Building)));

            weigth += CalculateLockWeigth(lck, term);

            return new SearchResult
            {
                Id = lck.Id,
                Description = lck.Description,
                Name = lck.Name,
                Weight = weigth
            };
        }

        private int CalculateLockWeigth(Lock lck, string term)
        {
            var weigth = CalculateWeigth(lck.Name.ToLowerInvariant(), term, new PropertyDefinition(nameof(Lock.Name), nameof(Lock)));
            weigth += CalculateWeigth(lck.Description.ToLowerInvariant(), term, new PropertyDefinition(nameof(Lock.Description), nameof(Lock)));
            weigth += CalculateWeigth(lck.Floor.ToLowerInvariant(), term, new PropertyDefinition(nameof(Lock.Floor), nameof(Lock)));
            weigth += CalculateWeigth(lck.RoomNumber.ToLowerInvariant(), term, new PropertyDefinition(nameof(Lock.RoomNumber), nameof(Lock)));
            weigth += CalculateWeigth(lck.SerialNumber.ToLowerInvariant(), term, new PropertyDefinition(nameof(Lock.SerialNumber), nameof(Lock)));
            weigth += CalculateWeigth(lck.Type.ToLowerInvariant(), term, new PropertyDefinition(nameof(Lock.Type), nameof(Lock)));

            return weigth;
        }

        private int CalculateWeigth(string propValue, string term, PropertyDefinition propDefinition)
        {
            var weigth = 0;

            if (propValue.Contains(term))
            {
                if (PropertyWeights.PopertyWeigths.TryGetValue(propDefinition, out int propWeigth))
                {
                    weigth += propWeigth;
                }

                if (propValue == term)
                {
                    weigth *= 10;
                }
            }

            return weigth;
        }

        private int CalculateTransientWeigth(string propValue, string term, PropertyDefinition propDefinition)
        {
            var weigth = 0;

            if (propValue.Contains(term))
            {
                if (PropertyWeights.TransientPropertyWeigths.TryGetValue(propDefinition, out int propWeigth))
                {
                    weigth += propWeigth;
                }
            }

            return weigth;
        }
    }
}
