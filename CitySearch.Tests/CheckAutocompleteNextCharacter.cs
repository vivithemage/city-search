using NUnit.Framework;

namespace CitySearch.Tests
{
    public class CheckAutocompleteNextCharacter
    {
        /*
         * Tests for the autocompletion of the next character. This test case is very specific and
         * depends on the prefix being 'darling' as specific next characters are checked for.
         */
        private bool ReturnedResults(ICityResult result)
        {
            if (result.NextCities.Count == 0) { 
                return false;
            }

            return true;
        }

        private bool ReturnedExpectedNextCharacters(ICityResult result)
        {
            bool containsE = false;
            bool containsH = false;
            bool containsT = false;

            foreach (string nextCharacter in result.NextLetters)
            {
                if (nextCharacter.Equals("e"))
                {
                    containsE = true;
                }
                
                if (nextCharacter.Equals("h"))
                {
                    containsH = true;
                }
                
                if (nextCharacter.Equals("t"))
                {
                    containsT = true;
                }
            }

            if (containsT && containsH && containsE)
            {
                return true;
            }

            return false;            
        }

        [Test]
        public void Main()
        {
            Trie trie = new Trie();
            trie.Init();
            
            const string sanitizedSearchTerm = "darling";
            
            ICityResult result = trie.Search(sanitizedSearchTerm);

            Assert.AreEqual(ReturnedResults(result), true);
            Assert.AreEqual(ReturnedExpectedNextCharacters(result), true);
        }
    }
}