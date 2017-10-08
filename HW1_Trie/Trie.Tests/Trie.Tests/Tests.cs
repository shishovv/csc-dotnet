using NUnit.Framework;

namespace Trie.Tests
{
    [TestFixture]
    public class Tests
    {        
        [Test]
        public void TestOperations()
        {
            Trie trie = new Trie();
            trie.Add("a");
            trie.Add("ab");
            trie.Add("abc");
            trie.Add("abce");
            trie.Add("abcde");
            Assert.True(trie.Size() == 5);
            Assert.True(trie.Contains("a"));
            Assert.True(trie.Contains("abc"));
            Assert.True(trie.Contains("abcde"));
            Assert.True(!trie.Contains("abcc"));
            Assert.True(trie.HowManyStartsWithPrefix("a") == 5);
            Assert.True(trie.HowManyStartsWithPrefix("b") == 0);
            Assert.True(trie.HowManyStartsWithPrefix("ab") == 4);
            Assert.True(trie.HowManyStartsWithPrefix("abce") == 1);
            Assert.True(trie.HowManyStartsWithPrefix("") == trie.Size());
            Assert.True(trie.Size() == 5);
            
            trie.Remove("abce");
            Assert.True(trie.Size() == 4);
            Assert.True(trie.Contains("a"));
            Assert.True(trie.Contains("abc"));
            Assert.True(trie.Contains("abcde"));
            Assert.True(!trie.Contains("abce"));
            Assert.True(trie.HowManyStartsWithPrefix("a") == 4);
            Assert.True(trie.HowManyStartsWithPrefix("b") == 0);
            Assert.True(trie.HowManyStartsWithPrefix("ab") == 3);
            Assert.True(trie.HowManyStartsWithPrefix("abce") == 0);
            Assert.True(trie.HowManyStartsWithPrefix("") == trie.Size());
            
            trie.Add("abce");
            Assert.True(trie.Size() == 5);
            Assert.True(trie.Contains("a"));
            Assert.True(trie.Contains("abc"));
            Assert.True(trie.Contains("abcde"));
            Assert.True(!trie.Contains("abcc"));
            Assert.True(trie.HowManyStartsWithPrefix("a") == 5);
            Assert.True(trie.HowManyStartsWithPrefix("b") == 0);
            Assert.True(trie.HowManyStartsWithPrefix("ab") == 4);
            Assert.True(trie.HowManyStartsWithPrefix("abce") == 1);
            Assert.True(trie.HowManyStartsWithPrefix("") == trie.Size());

            Assert.True(trie.HowManyStartsWithPrefix("abcjsk") == 0);
            Assert.True(!trie.Add("a"));
            Assert.True(trie.Add("ba"));
            Assert.True(trie.HowManyStartsWithPrefix("b") == 1);

            var size = trie.Size();
            trie.Add("a");
            trie.Add("ab");
            Assert.True(trie.Size() == size);
            trie.Remove("a");
            Assert.True(!trie.Contains("a"));
        }
    }
}