using System;

namespace HomeWork1
{
    class TrieTest
    {
        static void Main(string[] args)
        {
            testTrie();
            Console.WriteLine("All tests passed");
        }

        static void testTrie()
        {
            Trie trie = new Trie();
            trie.Add("a");
            trie.Add("ab");
            trie.Add("abc");
            trie.Add("abce");
            trie.Add("abcde");
            AssertTrue(trie.Size() == 5);
            AssertTrue(trie.Contains("a"));
            AssertTrue(trie.Contains("abc"));
            AssertTrue(trie.Contains("abcde"));
            AssertTrue(!trie.Contains("abcc"));
            AssertTrue(trie.HowManyStartsWithPrefix("a") == 5);
            AssertTrue(trie.HowManyStartsWithPrefix("b") == 0);
            AssertTrue(trie.HowManyStartsWithPrefix("ab") == 4);
            AssertTrue(trie.HowManyStartsWithPrefix("abce") == 1);
            AssertTrue(trie.HowManyStartsWithPrefix("") == trie.Size());

            trie.Remove("abce");
            AssertTrue(trie.Size() == 4);
            AssertTrue(trie.Contains("a"));
            AssertTrue(trie.Contains("abc"));
            AssertTrue(trie.Contains("abcde"));
            AssertTrue(!trie.Contains("abce"));
            AssertTrue(trie.HowManyStartsWithPrefix("a") == 4);
            AssertTrue(trie.HowManyStartsWithPrefix("b") == 0);
            AssertTrue(trie.HowManyStartsWithPrefix("ab") == 3);
            AssertTrue(trie.HowManyStartsWithPrefix("abce") == 0);
            AssertTrue(trie.HowManyStartsWithPrefix("") == trie.Size());

            trie.Add("abce");
            AssertTrue(trie.Size() == 5);
            AssertTrue(trie.Contains("a"));
            AssertTrue(trie.Contains("abc"));
            AssertTrue(trie.Contains("abcde"));
            AssertTrue(!trie.Contains("abcc"));
            AssertTrue(trie.HowManyStartsWithPrefix("a") == 5);
            AssertTrue(trie.HowManyStartsWithPrefix("b") == 0);
            AssertTrue(trie.HowManyStartsWithPrefix("ab") == 4);
            AssertTrue(trie.HowManyStartsWithPrefix("abce") == 1);
            AssertTrue(trie.HowManyStartsWithPrefix("") == trie.Size());

            AssertTrue(trie.HowManyStartsWithPrefix("abcjsk") == 0);
            AssertTrue(trie.Add("a") == false);
            AssertTrue(trie.Add("ba") == true);
            AssertTrue(trie.HowManyStartsWithPrefix("b") == 1);

            int size = trie.Size();
            trie.Add("a");
            trie.Add("ab");
            AssertTrue(trie.Size() == size);
            trie.Remove("a");
            AssertTrue(!trie.Contains("a"));
        }

        static void AssertTrue(bool e)
        {
            if (!e)
            {
                throw new Exception("TEST FAILED");
            }
        }
    }
}
