using System;

namespace City_Search
{
    class Program
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
                    pCrawl.children[index] = new TrieNode();

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
        static bool search(String key)
        {
            int level;
            int length = key.Length;
            int index;
            TrieNode pCrawl = root;

            for (level = 0; level < length; level++)
            {
                index = getIndex(key, level);

                if (pCrawl.children[index] == null)
                    return false;

                pCrawl = pCrawl.children[index];
            }

            return (pCrawl != null && pCrawl.isEndOfWord);
        }

        static void Main(string[] args)
        {            
            String[] keys = {"the ", "a", "there", "answer",
                        "any", "by", "bye", "their", "testing"};
            String[] output = { "Not present in trie", "Present in trie" };


            root = new TrieNode();

            // Construct trie 
            int i;
            for (i = 0; i < keys.Length; i++)
                insert(keys[i]);

            // Search for different keys
            String searchterm = "the ";

            if (search(searchterm) == true)
                Console.WriteLine(searchterm + " --- " + output[1]);
            else Console.WriteLine(searchterm + " --- " + output[0]);

        }
    }
}
