namespace City_Search
{
    class Program
    {
        static void Main(string[] args)
        {
            Trie trie = new Trie();

            trie.build();
            UserInput userInput = new UserInput();


            userInput.readLoop(trie);

            //trie.buildTrie();
            
        }
    }
}
