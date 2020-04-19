using NUnit.Framework;
using System.Collections.Generic;
using static CitySearch.Cities;

/*
 * This checks the file can be opened and a minor check to confirm the list contains cities.
 * If it loads and the key city is available, the test is passed.         * 
 */
namespace CitySearch.Tests
{
    public class CheckKeyCityAvailable
    {
        [Test]
        public void Main()
        {
            //Retrieves all the cities from the json file.
            List <City> citiesGroup = Cities.Get();
            
            bool keyCityCheck = false;

            // Checks for a key city.
            foreach (Cities.City singleCity in citiesGroup)
            {
                if (singleCity.Name.ToLower() == "darlington")
                {
                    keyCityCheck = true;
                    break;
                }
            }

            Assert.AreEqual(keyCityCheck, true);
        }
    }
}