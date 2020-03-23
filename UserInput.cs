using System;


namespace City_Search
{
    class UserInput
    {
        private string SanitizeInput(String input)
        {
            return input.ToLower();
        }

        private string AvailableNextCharactersMessage(ICityResult result)
        {
            string message = "The following characters are available: ";

            return message;
        }

        private string AvailableNextCitiesMessage(ICityResult result)
        {
            string message = "The following cities are available: ";

            return message;
        }

        public void ReadLoop(Trie trie)
        {
            string message;
            string searchterm;
            string sanitizedSearchTerm;

            Console.WriteLine("Axa City Search (type exit to quit):"); // Prompt

            while (true)
            {                
                searchterm = Console.ReadLine(); 
                sanitizedSearchTerm = SanitizeInput(searchterm);

                ICityResult result = trie.Search(sanitizedSearchTerm);

                if (sanitizedSearchTerm == "exit")
                {
                    break;
                }
                else if (result.NextLetters.Count.Equals(0))
                {
                    message = sanitizedSearchTerm + " is a city";
                }
                else
                {
                    message = AvailableNextCitiesMessage(result) + AvailableNextCitiesMessage(result);                    
                }

                Console.WriteLine(message);
            }
        }
    }
}
