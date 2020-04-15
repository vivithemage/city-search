using System;


namespace CitySearch
{
    class UserInput
    {
        private string SanitizeInput(String input)
        {
            return input.ToLower();
        }

        private string AvailableNextCharactersMessage(ICityResult result)
        {
            string message = "The following next characters are available:";

            foreach (string letter in result.NextLetters)
            {
                message = $"{message} {letter},";
            }

            message = message.TrimEnd(',');

            return message;
        }

        private string AvailableNextCitiesMessage(ICityResult result)
        {
            string message = "The following cities are available:";

            foreach (string city in result.NextCities)
            {
                message = $"{message} {city},";
            }

            message = message.TrimEnd(',');

            return message;
        }

        private void displayWelcomeMessage()
        {
            Console.WriteLine("Axa City Search (type exit to quit):");
        }

        public void ReadLoop(Trie trie)
        {
            string message = "";
            string searchterm;
            string sanitizedSearchTerm;

            displayWelcomeMessage();

            while (true)
            {
                message = "";
                searchterm = Console.ReadLine(); 
                sanitizedSearchTerm = SanitizeInput(searchterm);

                ICityResult result = trie.Search(sanitizedSearchTerm);

                if (sanitizedSearchTerm == "exit")
                {
                    break;
                }
                else if (result.NextLetters.Count.Equals(0) && result.NextCities.Count.Equals(0))
                {
                    message = "Input does not bear resemblance to anything. Unable to suggest next characters or cities";
                }
                else
                {
                    if (result.CityFound)
                    {
                        message = sanitizedSearchTerm + " is a city\n";
                    }

                    message = message + AvailableNextCitiesMessage(result) + "\n" + AvailableNextCharactersMessage(result);                    
                }

                Console.WriteLine(" -- Start of results for '" + searchterm + "' --");
                Console.WriteLine(message);
                Console.WriteLine(" -- End of results for '" + searchterm + "' --");
            }
        }
    }
}
