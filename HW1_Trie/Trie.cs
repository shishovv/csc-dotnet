using System.Collections.Generic;

namespace HomeWork1
{
    public class Trie
    {
        private const int INITIAL_CAPACITY = 8;
        private const int ROOT = 0;
        private List<Vertex> data;
        private int size;

        public Trie()
        {
            data = new List<Vertex>(INITIAL_CAPACITY);
            data.Add(new Vertex());
        }
        public bool Add(string str)
        {
            if (Contains(str))
            {
                return false;
            }

            int currentVertex = ROOT;
            foreach (char c in str)
            {
                data[currentVertex].wordsWithSamePrefixCount++;
                if (!data[currentVertex].next.ContainsKey(c))
                {
                    data.Add(new Vertex());
                    data[currentVertex].next[c] = data.Count - 1;
                    currentVertex = data.Count - 1;
                } 
                else
                {
                    currentVertex = data[currentVertex].next[c];
                }
            }
            data[currentVertex].isTerminal = true;
            size++;

            return true;
        }

        public bool Contains(string str) 
        {
            int v = find(str);
            return v != -1 && data[v].isTerminal;
        }

        public bool Remove(string str)
        {
            return Remove(0, str, out bool _);
        }

        private bool Remove(int vertex, string str, out bool vertexWasRemoved)
        {
            if (string.IsNullOrEmpty(str))
            {
                if (data[vertex].isTerminal)
                {
                    data[vertex].wordsWithSamePrefixCount--;
                    size--;
                    vertexWasRemoved = true;
                    return true;
                }
                else
                {
                    vertexWasRemoved = false;
                    return false;
                }
            }

            char c = str[0];
            if (data[vertex].next.ContainsKey(c))
            {
                bool removed = Remove(data[vertex].next[c], str.Substring(1), out bool charWasRemoved);
                bool needRemoveNextVertex = removed && charWasRemoved && data[vertex].next.Count > 1;
                if (needRemoveNextVertex) 
                {
                    vertexWasRemoved = true;
                    data[vertex].next.Remove(c);
                } 
                else 
                {
                    vertexWasRemoved = false;
                    if (removed)
                    {
                        data[vertex].wordsWithSamePrefixCount--;
                    }
                }
                return removed;
            } 
            else 
            {
                vertexWasRemoved = false;
                return false;
            }
        }

        private int find(string str)
        {
            int currentVertex = ROOT;
            foreach (char c in str)
            {
                if (!data[currentVertex].next.ContainsKey(c)) 
                {
                    return -1;
                } 
                else 
                {
                    currentVertex = data[currentVertex].next[c];
                }
            }
            return currentVertex;
        }

        public int Size()
        {
            return size;
        }

        public int HowManyStartsWithPrefix(string prefix)
        {
            int v = find(prefix);
            return v == -1 ? 0 : data[v].wordsWithSamePrefixCount + (data[v].isTerminal ? 1 : 0);
        }

        private class Vertex
        {
            public Dictionary<char, int> next;
            public bool isTerminal;

            public int wordsWithSamePrefixCount;

            public Vertex()
            {
                next = new Dictionary<char, int>();
            }
        }
    }
}
