using System;
using System.Collections.Generic;
using System.Text;

namespace City_Search
{
    class UserInput
    {
        public void readLoop(Trie trie)
        {
            string result;
            string searchterm;
            
            Console.WriteLine("Axa City Search (type exit to quit):"); // Prompt

            while (true)
            {                
                searchterm = Console.ReadLine(); // Get string from user               

                if (searchterm == "exit") // Check string
                {
                    break;
                }

                if (trie.search(searchterm) == true)
                {
                    result = searchterm + " --- present in trie");
                }
                else
                {
                    result = searchterm + " --- Not Present in trie");
                }

                Console.WriteLine(result);
            }
        }
    }
}
