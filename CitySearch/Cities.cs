using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

using Newtonsoft.Json;

namespace CitySearch
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
            string json = "";
            string workingDirectory = System.IO.Path.GetFullPath(@"..\..\..\");
            string currentDirectory = Directory.GetParent(workingDirectory).Parent.FullName;

            try
            {
                json = File.ReadAllText(currentDirectory + "\\CitySearch\\data\\cities.json");
            } 
            catch (System.IO.FileNotFoundException e)
            {                
                Console.WriteLine(e.Message);
                Console.WriteLine("Exiting application");
                System.Environment.Exit(0);
            }
                       

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
