namespace City_Search
{
    class Program
    {
        static void Main(string[] args)
        {
            Trie trie = new Trie();
            UserInput userInput = new UserInput();


            userInput.readLoop(trie);

            //trie.buildTrie();
            
        }
    }
}
