using System.Collections.Generic;

namespace Trie
{
    public class Trie : ITrie
    {
        private const int INITIAL_CAPACITY = 8;
        private const int ROOT = 0;

        private readonly List<Vertex> _data;
        private int _size;

        public Trie()
        {
            _data = new List<Vertex>(INITIAL_CAPACITY) { new Vertex() };
        }

        public bool Add(string str)
        {
            if (Contains(str))
            {
                return false;
            }

            var currentVertex = _data[ROOT];
            ++currentVertex.WordsWithSamePrefixCount;
            foreach (var c in str)
            {
                if (!currentVertex.Next.ContainsKey(c))
                {
                    _data.Add(new Vertex());
                    currentVertex.Next[c] = _data.Count - 1;
                    currentVertex = _data[_data.Count - 1];
                }
                else
                {
                    currentVertex = _data[currentVertex.Next[c]];
                }
                ++currentVertex.WordsWithSamePrefixCount;
            }
            currentVertex.IsTerminal = true;
            _size++;

            return true;
        }

        public bool Contains(string str)
        {
            var v = Find(str);
            return v != null && v.IsTerminal;
        }

        public bool Remove(string str)
        {
            if (!Contains(str)) return false;

            var currentVertex = _data[0];
            currentVertex.WordsWithSamePrefixCount--;
            foreach (var c in str)
            {
                var nextVertex = _data[currentVertex.Next[c]];
                if (nextVertex.WordsWithSamePrefixCount == 1) currentVertex.Next.Remove(c);
                else nextVertex.WordsWithSamePrefixCount--;
                currentVertex = nextVertex;
            }
            currentVertex.IsTerminal = false;
            _size--;
            return true;
        }

        private Vertex Find(string str)
        {
            var currentVertex = _data[ROOT];
            foreach (var c in str)
            {
                if (!currentVertex.Next.ContainsKey(c))
                {
                    return null;
                }
                currentVertex = _data[currentVertex.Next[c]];
            }
            return currentVertex;
        }

        public int Size()
        {
            return _size;
        }

        public int HowManyStartsWithPrefix(string prefix)
        {
            var v = Find(prefix);
            return v != null ? v.WordsWithSamePrefixCount : 0;
        }

        private class Vertex
        {
            public Dictionary<char, int> Next { get; }
            public bool IsTerminal { get; set; }

            public int WordsWithSamePrefixCount;

            public Vertex()
            {
                Next = new Dictionary<char, int>();
            }
        }
    }
}
