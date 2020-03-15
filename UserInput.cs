using System;


namespace City_Search
{
    class UserInput
    {
        private string SanitizeInput(String input)
        {
            return input.ToLower();
        }

        public void ReadLoop(Trie trie)
        {
            string result;
            string searchterm;
            string sanitizedSearchTerm;

            Console.WriteLine("Axa City Search (type exit to quit):"); // Prompt

            while (true)
            {                
                searchterm = Console.ReadLine(); 
                sanitizedSearchTerm = SanitizeInput(searchterm);

                if (sanitizedSearchTerm == "exit")
                {
                    break;
                } 
                else if (trie.Search(sanitizedSearchTerm) == true)
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
