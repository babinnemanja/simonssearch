using SimonsSearch.Service.Constants;
using SimonsSearch.Service.DataModels;

namespace SimonsSearch.Service
{
    public class SearchEngineWeigthCalculator
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
    }
}
