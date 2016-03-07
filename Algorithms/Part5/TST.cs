using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Part5
{
    public class TST<Value>
    {
        private Node _root;
        private int _n;

        private class Node
        {
            public char C { get; set; }
            public Node Left { get; set; }
            public Node Mid { get; set; }
            public Node Right { get; set; }
            public object Value { get; set; }
        }

        public TST() { }

        public Value Get(string key)
        {
            Node x = this.get(this._root, key, 0);
            if (x == null) return default(Value);
            return (Value)x.Value;
        }
        private Node get(Node x, string key, int d)
        {
            if (x == null) return null;
            if (key.Length == 0) return x;

            char c = Convert.ToChar(key.Substring(d, 1));
            if (c < x.C) return this.get(x.Left, key, d);
            else if (c > x.C) return this.get(x.Right, key, d);
            else if (d < key.Length - 1) return this.get(x.Mid, key, d + 1);
            else return x;
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
            char c = Convert.ToChar(key.Substring(d, 1));
            if (x == null)
            {
                x = new Node();
                x.C = c;
            }
            if (c < x.C) x.Left = this.put(x.Left, key, value, d);
            else if (c > x.C) x.Right = this.put(x.Right, key, value, d);
            else if (d < key.Length - 1) x.Mid = this.put(x.Mid, key, value, d + 1);
            else
            {
                if (x.Value == null) this._n++;
                x.Value = value;
            }
            return x;
        }

        public void Delete(string key)
        {
            // TODO: 实现删除操作
            //this._root = this.delete(this._root, key, 0);
        }
        //private Node delete(Node x, string key, int d)
        //{
        //    if (x == null) return null;
        //    if (d == key.Length)
        //    {
        //        if (x.Value != null) this._n--;
        //        x.Value = default(Value);
        //    }
        //    else
        //    {
        //        char c = Convert.ToChar(key.Substring(d, 1));
        //        x = this.delete(x.Next[c], key, d + 1);
        //    }

        //    if (x.Value != null) return x;
        //    for (int c = 0; c < R; c++)
        //        if (x.Next[c] != null)
        //            return x;

        //    return null;
        //}

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
            char c = Convert.ToChar(query.Substring(d, 1));
            if (c < x.C) return this.longestPrefixOf(x.Left, query, d, length);
            else if (c > x.C) return this.longestPrefixOf(x.Right, query, d, length);
            else
            {
                d++;
                if (x.Value != null) length = d;
                return this.longestPrefixOf(x.Mid, query, d, length);
            }
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
            if (x.Value != null) results.Enqueue(prefix.ToString());
            this.collect(x.Mid, new StringBuilder(prefix), results);
            return results;
        }
        private void collect(Node x, StringBuilder prefix, Queue<string> results)
        {
            if (x == null) return;
            if (x.Value != null) results.Enqueue(prefix.ToString() + x.C);

            this.collect(x.Left, prefix, results);

            this.collect(x.Mid, prefix.Append(x.C), results);
            prefix.Remove(prefix.Length - 1, 1);

            this.collect(x.Right, prefix, results);
        }

        public IEnumerable<string> Keys()
        {
            Queue<string> results = new Queue<string>();
            this.collect(this._root, new StringBuilder(string.Empty), results);
            return results;
        }

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
            char c = Convert.ToChar(pattern.Substring(d, 1));
            if (c == '.' || c < x.C) this.collect(x.Left, prefix, pattern, results);
            if (c == '.' || c == x.C)
            {
                if (d == pattern.Length - 1 && x.Value != null) results.Enqueue(prefix.ToString() + x.C);
                if (d < pattern.Length - 1)
                {
                    this.collect(x.Mid, prefix.Append(x.C), pattern, results);
                    prefix.Remove(prefix.Length - 1, 1);
                }
            }
            if (c == '.' || c > x.C) this.collect(x.Right, prefix, pattern, results);
        }
    }
}
