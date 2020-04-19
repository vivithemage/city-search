using NUnit.Framework;

/*
 * Tests for the autocompletion of the next cities. This test case is very specific and
 * depends on the prefix being 'darlingt' and then checks whether the expected next cities are returned.
 */
namespace CitySearch.Tests
{
    public class CheckAutocompleteNextCity
    {
        private bool ReturnedResults(ICityResult result)
        {
            if (result.NextCities.Count == 0)
            {
                return false;
            }

            return true;
        }

        private bool ReturnedExpectedNextCities(ICityResult result)
        {
            bool firstCity = false;
            bool secondCity = false;
            bool thirdCity = false;

            foreach (string nextCity in result.NextCities)
            {
                if (nextCity.Equals("darlington")) 
                {
                    firstCity = true;
                }
                
                if (nextCity.Equals("darlington county"))
                {
                    secondCity = true;
                }
                
                if (nextCity.Equals("darlington point"))
                {
                    thirdCity = true;
                }
            }

            if (firstCity && secondCity && thirdCity)
            {
                return true;
            }

            return false;
        }

        [Test]
        public void Main()
        {
            Trie trie = new Trie();
            trie.Build();

            const string sanitizedSearchTerm = "darlingt";

            ICityResult result = trie.Search(sanitizedSearchTerm);

            Assert.AreEqual(ReturnedResults(result), true);
            Assert.AreEqual(ReturnedExpectedNextCities(result), true);
        }
    }
}