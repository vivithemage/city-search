using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

using Newtonsoft.Json;

namespace City_Search
{
    class Cities
    {
        public class City
        {
            public string Name { get; set; }
        }

        static string RemoveDiacritics(string text)
        {
            var normalizedString = text.Normalize(NormalizationForm.FormD);
            var stringBuilder = new StringBuilder();

            foreach (var c in normalizedString)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        /*
         * Pulls through all cities from json file
         */
        public static List<City> Get()
        {
            string citiesFilePath = "C:\\Users\\User\\source\\repos\\City Search\\City Search\\data\\cities.json";

            string json = File.ReadAllText(citiesFilePath);

            string jsonSantiized = RemoveDiacritics(json);

            List<City> citiesGroup = JsonConvert.DeserializeObject<List<City>>(jsonSantiized);

            return citiesGroup;
        }

        /*
         * Pulls latest data set from repository and saves to the data folder.
         */
        static void Refresh()
        {

        }
    }
}
