using SimonsSearch.Service.Constants;
using SimonsSearch.Service.DataModels;
using SimonsSearch.Service.Interfaces;

namespace SimonsSearch.Service
{
    public class SearchEngineWeigthCalculator : ISearchEngineWeigthCalculator
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
            var weigth = CalculateTransientWeigth(building.Name, term, new PropertyDefinition(nameof(Building.Name), nameof(Building)));
            weigth += CalculateTransientWeigth(building.Description, term, new PropertyDefinition(nameof(Building.Description), nameof(Building)));
            weigth += CalculateTransientWeigth(building.ShortCut, term, new PropertyDefinition(nameof(Building.ShortCut), nameof(Building)));

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
            var weigth = CalculateWeigth(lck.Name, term, new PropertyDefinition(nameof(Lock.Name), nameof(Lock)));
            weigth += CalculateWeigth(lck.Description, term, new PropertyDefinition(nameof(Lock.Description), nameof(Lock)));
            weigth += CalculateWeigth(lck.Floor, term, new PropertyDefinition(nameof(Lock.Floor), nameof(Lock)));
            weigth += CalculateWeigth(lck.RoomNumber, term, new PropertyDefinition(nameof(Lock.RoomNumber), nameof(Lock)));
            weigth += CalculateWeigth(lck.SerialNumber, term, new PropertyDefinition(nameof(Lock.SerialNumber), nameof(Lock)));
            weigth += CalculateWeigth(lck.Type, term, new PropertyDefinition(nameof(Lock.Type), nameof(Lock)));

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
