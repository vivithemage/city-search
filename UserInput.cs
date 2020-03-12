using System;


namespace City_Search
{
    class UserInput
    {
        private String sanitizeInput(String input)
        {
            return input.ToLower();
        }

        public void readLoop(Trie trie)
        {
            string result;
            string searchterm;
            String sanitizedSearchTerm;

            Console.WriteLine("Axa City Search (type exit to quit):"); // Prompt

            while (true)
            {                
                searchterm = Console.ReadLine(); 
                sanitizedSearchTerm = sanitizeInput(searchterm);

                if (sanitizedSearchTerm == "exit")
                {
                    break;
                } 
                else if (trie.search(sanitizedSearchTerm) == true)
                {
                    result = sanitizedSearchTerm + " --- present in trie";
                }
                else
                {
                    result = sanitizedSearchTerm + " --- Not Present in trie";
                }

                Console.WriteLine(result);
            }
        }
    }
}
