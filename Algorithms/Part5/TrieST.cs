using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Part5
{
    public class TrieST<Value>
    {
        private const int R = 256;
        private Node _root;
        private int _n;

        private class Node
        {
            public object Value { get; set; }
            public Node[] Next { get; set; }
            public Node() { this.Next = new Node[R]; }
        }

        public TrieST() { }

        public Value Get(string key)
        {
            Node x = this.get(this._root, key, 0);
            if (x == null) return default(Value);
            return (Value)x.Value;
        }
        private Node get(Node x, string key, int d)
        {
            if (x == null) return null;
            if (d == key.Length) return x;
            char c = Convert.ToChar(key.Substring(d, 1));
            return this.get(x.Next[c], key, d + 1);
        }

        public bool Contains(string key) { return !this.Get(key).Equals(default(Value)); }

        public int Size() { return this._n; }

        public bool IsEmpty() { return this.Size() == 0; }

        public void Put(string key, Value value)
        {
            if (value.Equals(default(Value))) this.Delete(key);
            else this._root = this.put(this._root, key, value, 0);
        }
        private Node put(Node x, string key, Value value, int d)
        {
            if (x == null) x = new Node();
            if (d == key.Length)
            {
                if (x.Value == null) this._n++;
                x.Value = value;
                return x;
            }

            char c = Convert.ToChar(key.Substring(d, 1));
            x.Next[c] = this.put(x.Next[c], key, value, d + 1);

            return x;
        }

        public void Delete(string key)
        {
            this._root = this.delete(this._root, key, 0);
        }
        private Node delete(Node x, string key, int d)
        {
            if (x == null) return null;
            if (d == key.Length)
            {
                if (x.Value != null) this._n--;
                x.Value = default(Value);
            }
            else
            {
                char c = Convert.ToChar(key.Substring(d, 1));
                x = this.delete(x.Next[c], key, d + 1);
            }

            if (x.Value != null) return x;
            for (int c = 0; c < R; c++)
                if (x.Next[c] != null)
                    return x;

            return null;
        }

        /// <summary>
        /// Returns the string in the symbol table that is the longest prefix of query, or null, if no such string.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public string LongestPrefixOf(string query)
        {
            int length = this.longestPrefixOf(this._root, query, 0, -1);

            if (length == -1)
                return null;
            else
                return query.Substring(0, length);
        }
        private int longestPrefixOf(Node x, string query, int d, int length)
        {
            if (x == null) return length;
            if (x.Value != null) length = d;
            if (d == query.Length) return length;
            char c = Convert.ToChar(query.Substring(d, 1));
            return this.longestPrefixOf(x.Next[c], query, d + 1, length);
        }

        /// <summary>
        /// Returns all of the keys in the set that start with prefix.
        /// </summary>
        /// <param name="prefix"></param>
        /// <returns></returns>
        public IEnumerable<string> KeysWithPrefix(string prefix)
        {
            Queue<string> results = new Queue<string>();
            Node x = this.get(this._root, prefix, 0);
            this.collect(x, new StringBuilder(prefix), results);
            return results;
        }
        private void collect(Node x, StringBuilder prefix, Queue<string> results)
        {
            if (x == null) return;
            if (x.Value != null) results.Enqueue(prefix.ToString());
            for (int c = 0; c < R; c++)
            {
                this.collect(x.Next[c], prefix.Append((char)c), results);
                prefix.Remove(prefix.Length - 1, 1);
            }
        }

        public IEnumerable<string> Keys() { return this.KeysWithPrefix(string.Empty); }

        /// <summary>
        /// Returns all of the keys in the symbol table that match pattern, where . symbol is treated as a wildcard character.
        /// </summary>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public IEnumerable<string> KeysThatMatch(string pattern)
        {
            Queue<string> results = new Queue<string>();
            this.collect(this._root, new StringBuilder(), pattern, results);
            return results;
        }
        private void collect(Node x, StringBuilder prefix, string pattern, Queue<string> results)
        {
            if (x == null) return;

            int d = prefix.Length;
            if (d == pattern.Length && x.Value != null) results.Enqueue(prefix.ToString());
            if (d == pattern.Length) return;

            char next = Convert.ToChar(pattern.Substring(d, 1));
            for (int c = 0; c < R; c++)
                if (next == '.' || next == c)
                {
                    this.collect(x.Next[(char)c], prefix.Append((char)c), pattern, results);
                    prefix.Remove(prefix.Length - 1, 1);
                }
        }
    }
}
