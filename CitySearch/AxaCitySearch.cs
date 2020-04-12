namespace CitySearch
{
    class AxaCitySearch
    {
        static void Main(string[] args)
        {
            Trie trie = new Trie();
            trie.Build();

            UserInput userInput = new UserInput();
            userInput.ReadLoop(trie);
        }
    }
}
