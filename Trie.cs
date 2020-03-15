using System;
using System.Collections.Generic;

namespace City_Search
{
    class Trie
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


        static bool isValid(String key)
        {
            int level;
            int length = key.Length;
            int index;

            for (level = 0; level < length; level++)
            {
                index = getIndex(key, level);
                
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
        static void insert(String key)
        {
            int level;
            int length = key.Length;
            int index;

            TrieNode pCrawl = root;

            for (level = 0; level < length; level++)
            {
                // numerical index of alphabet?
                index = getIndex(key, level);

                if (pCrawl.children[index] == null)
                {
                    pCrawl.children[index] = new TrieNode();
                }

                pCrawl = pCrawl.children[index];                
            }

            // mark last node as leaf 
            pCrawl.isEndOfWord = true;
        }

        // Needed to handle extra characters
        static int getIndex(String key, int level)
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

        // Returns true if key  
        // presents in trie, else false 
        public bool search(String key)
        {
            int level;
            int length = key.Length;
            int index;
            TrieNode pCrawl = root;

            for (level = 0; level < length; level++)
            {
                index = getIndex(key, level);

               if (pCrawl.children[index] == null)
                {
                    return false;
                }                    

                pCrawl = pCrawl.children[index];
            }

            return (pCrawl != null && pCrawl.isEndOfWord);
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

            foreach (Cities.City singleCity in citiesGroup)
            {
                if (isValid(singleCity.Name.ToLower()))
                {
                    insert(singleCity.Name.ToLower());
                }
                else
                {
                    Console.WriteLine("Unable to add {0} as it contains a character that " +
                        "is not in the 26 letter alphabet and is not a space or a dash",
                        singleCity.Name.ToLower());
                }
            }            
        }
    }
}
