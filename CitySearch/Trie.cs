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

            public TrieNode()
            {
                isEndOfWord = false;                
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
                
                if ((index > ALPHABET_SIZE) || (index < 0))
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
            for (int level = 0; level < ALPHABET_SIZE; level++)
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
        private static void DFSPreorder(ref ICollection<string> nextCities, TrieNode pRoot, string currentPrefix)
        {
            if (pRoot.isEndOfWord)
            {
                nextCities.Add(currentPrefix);                
            }

            if (IsLastNode(pRoot)) 
            {
                return;
            }

            for (int level = 0; level < ALPHABET_SIZE; level++)
            {
                if (pRoot.children[level] != null)
                {                    
                    DFSPreorder(ref nextCities, pRoot.children[level], currentPrefix + GetCharacterFromIndex(level));                    
                }
            }
        }

        /* 
         * Returns all next potential cities
         * Use depth-first traversal - Preorder (Root, Left, Right).
         */
        private static ICollection<string> AutocompleteCities(TrieNode pCrawl, string key)
        {
            ICollection<string> nextCities = new List<string>();

            DFSPreorder(ref nextCities, pCrawl, key);

            return nextCities;
        }

        /* 
         * Returns next three characters. Requires length to determine
         * which child to start with.
         */
        private static ICollection<string> AutocompleteCharacters(TrieNode pCrawl)
        {
            ICollection<string> nextCharacters = new List<string>();

            char currentNextCharacter;

            for (int level = 0; level < ALPHABET_SIZE; level++)
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
            int index;
            int length = key.Length;
            
            TrieNode pCrawl = root;

            CityResult result = new CityResult();

            for (level = 0; level < length; level++)
            {
                if (pCrawl == null)
                {
                    return result;
                }

                index = GetIndex(key, level);
                pCrawl = pCrawl.children[index];
            }

            /*
             * If there is an exact match, the object lists will remain empty and the CityFound flag will be set to true.                          
             */
            if (ExactMatch(pCrawl))
            {
                result.CityFound = true;
            }

            /* Indicates the prefix does not match any city and there are no next character suggestions
             * so just return the empty result 
             */
            if (pCrawl == null)
            {
                return result;
            }

            /* 
             * Have not been able to find a city so return the next
             * three characters and cities
             */            
            bool isLast = IsLastNode(pCrawl);

            if (!isLast)
            {
                result.NextCities = AutocompleteCities(pCrawl, key);
            }

            result.NextLetters = AutocompleteCharacters(pCrawl);

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
