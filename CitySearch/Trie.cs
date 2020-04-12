using System;
using System.Collections.Generic;

namespace CitySearch
{
    public class Trie : ICityFinder
    {
        // Alphabet size (26) and increased by two to include spaces and dashes.
        static readonly int ALPHABET_SIZE = 28;

        // trie node 
        class TrieNode
        {
            public TrieNode[] children = new TrieNode[ALPHABET_SIZE];

            // isEndOfWord is true if the node represents 
            // end of a word 
            public bool isEndOfWord;
            public bool visited;

            public TrieNode()
            {
                isEndOfWord = false;
                visited = false;
                for (int i = 0; i < ALPHABET_SIZE; i++)
                    children[i] = null;
            }
        };

        static TrieNode root;


        static bool IsValid(String key)
        {
            int level;
            int length = key.Length;
            int index;

            for (level = 0; level < length; level++)
            {
                index = GetIndex(key, level);
                
                if ((index > 28) || (index < 0))
                {
                    return false;
                }
            }

            return true;
        }

        // If not present, inserts key into trie 
        // If the key is prefix of trie node,  
        // just marks leaf node 
        static void Insert(String key)
        {
            int level;
            int length = key.Length;
            int index;

            TrieNode pCrawl = root;

            for (level = 0; level < length; level++)
            {
                // numerical index of alphabet?
                index = GetIndex(key, level);

                if (pCrawl.children[index] == null)
                {
                    pCrawl.children[index] = new TrieNode();
                }

                pCrawl = pCrawl.children[index];                
            }

            // mark last node as leaf 
            pCrawl.isEndOfWord = true;
        }

        static char GetCharacterFromIndex(int index)
        {
            char character;
            int unicodeStartingPosition = 97;

            switch (index)
            {
                case 26:
                    character = ' ';
                    break;
                case 27:
                    character = '-';
                    break;
                default:
                    character = (char)(unicodeStartingPosition + index);
                    break;
            }

            return character;
        }

        // Needed to handle extra characters
        static int GetIndex(String key, int level)
        {
            int index;

            switch (key[level])
            {
                case ' ':
                    index = 26;
                    break;
                case '-':
                    index = 27;
                    break;
                default:
                    index = key[level] - 'a';
                    break;
            }

            return index;
        }

        private static bool IsLastNode(TrieNode pCrawl)
        {
            for (int level = 0; level < 28; level++)
            {
                if (pCrawl.children[level] != null)
                {
                    return false;
                } 
            }

            return true;
        }

        /*
         * Pass cities by reference so no need to return.
         * Run DFS recursively to build the array
         */
        private static void DFSPreorder(ref string currentCity, ref ICollection<string> nextCities, TrieNode pCrawl, TrieNode pRoot,string key)
        {
            pCrawl.visited = true;

            for (int level = 0; level < 28; level++)
            {
                if (pCrawl.children[level] != null)
                {
                    currentCity = currentCity + GetCharacterFromIndex(level);

                    if (pCrawl.children[level].isEndOfWord)
                    {
                        //pCrawl.children[level].visited = true;
                        Console.WriteLine(System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(key) + currentCity);


                        // If all next stages are null, clear city
                        //TODO - this is flawed. It's clearing a current city when there could be others that build on it
                        if (IsLastNode(pCrawl.children[level]) && pCrawl.children[level].visited != true)
                        {
                            currentCity = "";
                            //DFSPreorder(ref currentCity, ref nextCities, pRoot, pRoot);                            
                        }

                    }

                    DFSPreorder(ref currentCity, ref nextCities, pCrawl.children[level], pRoot,key);
                }
            }
        }

        /* 
         * Returns all next potential cities
         * Use depth-first traversal - Preorder (Root, Left, Right).
         * 
         * TODO: Allow for each of the letters in each note to be set as 'visited'
         * 
         * Plan.
         * Create a string
         * Traverse the tree and as each letter is passed, mark it as visited and append it to the string.
         * If the node is an end of word node, stop the traverse, log the string as a city and start the traversal again.
         * All traversals will ignore any visted nodes and by using the preorder method of traversal the city names should be yielded.
         * After everything has been traversed in the tree, reset all the visited flags to 'not visited' ready for another search.
         */
        private static ICollection<string> AutocompleteCities(TrieNode pCrawl, int length,string key)
        {
            ICollection<string> nextCities = new List<string>();
            string currentCity = "";

            DFSPreorder(ref currentCity, ref nextCities, pCrawl, pCrawl,key);

            return nextCities;
        }

        /* 
         * Returns next three characters. Requires length to determine
         * which child to start with.
         */
        private static ICollection<string> AutocompleteCharacters(TrieNode pCrawl, int length)
        {
            ICollection<string> nextCharacters = new List<string>();

            char currentNextCharacter;

            for (int level = 0; level < 28; level++)
            {
                if (pCrawl.children[level] != null)
                {
                    currentNextCharacter = GetCharacterFromIndex(level);
                    nextCharacters.Add(currentNextCharacter.ToString());
                }
            }

            return nextCharacters;
        }

        static bool ExactMatch(TrieNode pCrawl)
        {
            return (pCrawl != null && pCrawl.isEndOfWord);
        }
        
        /*
         * Returns an empty CityResult object if a city is found,
         * if not, potential next letters and cities are returned if a partial
         * match is found.
         */
        public ICityResult Search(String key)
        {
            int level;
            int length = key.Length;
            int index;
            TrieNode pCrawl = root;

            CityResult result = new CityResult();

            for (level = 0; level < length; level++)
            {
                index = GetIndex(key, level);

                if (pCrawl.children[index] == null)
                {
                    return result;
                }

                pCrawl = pCrawl.children[index];
            }

            /*
             * If there is an exact match, the object lists will remain empty and the CityFound flag will be set to true.                          
             */
            if (ExactMatch(pCrawl))
            {
                result.CityFound = true;
                return result;
            }

            /* 
             * Have not been able to find a city so return the next
             * three characters and cities
             */
            result.NextLetters = AutocompleteCharacters(pCrawl, length);
            result.NextCities = AutocompleteCities(pCrawl, length,key);

            return result;
        }

        /* 
         * Builds trie structure with world cities.
         * Uses the following as the city data source:
         * https://github.com/dr5hn/countries-states-cities-database/blob/master/cities.json
         */
        public void Build()
        {
            List<Cities.City> citiesGroup = Cities.Get();

            root = new TrieNode();

            Console.WriteLine("Loading, please wait...");

            foreach (Cities.City singleCity in citiesGroup)
            {
                if (IsValid(singleCity.Name.ToLower()))
                {
                    Insert(singleCity.Name.ToLower());
                }
                else
                {
                    /*Console.WriteLine("Unable to add {0} as it contains a character that " +
                        "is not in the 26 letter alphabet and is not a space or a dash",
                        singleCity.Name.ToLower());*/
                }
            }            
        }
    }
}
