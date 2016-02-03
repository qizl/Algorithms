using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Algorithms.Part3
{
    public class BST<Key, Value>
        where Key : IComparable
    {
        private Node _root;

        private class Node
        {
            public Key Key { get; set; }
            public Value Value { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }
            public int N { get; set; }

            public Node(Key key, Value val, int N)
            {
                this.Key = key;
                this.Value = val;
                this.N = N;
            }
        }

        public bool IsEmpty()
        {
            return this.Size() == 0;
        }

        public void Put(Key key, Value val)
        {
            this._root = this.put(this._root, key, val);
        }
        private Node put(Node x, Key key, Value val)
        {
            if (x == null)
                return new Node(key, val, 1);

            int cmp = key.CompareTo(x.Key);
            if (cmp < 0)
                x.Left = this.put(x.Left, key, val);
            else if (cmp > 0)
                x.Right = this.put(x.Right, key, val);
            else
                x.Value = val;
            x.N = 1 + this.size(x.Left) + this.size(x.Right);

            return x;
        }

        public Value Get(Key key)
        {
            return this.get(this._root, key);
        }
        private Value get(Node x, Key key)
        {
            if (x == null)
                throw new NullReferenceException("called get() with empty symbol table");

            int cmp = key.CompareTo(x.Key);
            if (cmp < 0)
                return this.get(x.Left, key);
            else if (cmp > 0)
                return this.get(x.Right, key);
            else
                return x.Value;
        }

        public int Size(Key lo, Key hi)
        {
            if (lo.CompareTo(hi) > 0)
                return 0;
            if (this.Contains(hi))
                return this.Rank(hi) - this.Rank(lo) + 1;
            else
                return this.Rank(hi) - this.Rank(lo);
        }
        public int Size()
        {
            return this.size(this._root);
        }
        private int size(Node x)
        {
            if (x == null)
                return 0;
            else
                return x.N;
        }

        public bool Contains(Key key)
        {
            return this.Get(key) != null;
        }

        public Key Min()
        {
            if (this.IsEmpty())
                throw new NullReferenceException("called min() with empty symbol table");

            return this.min(this._root).Key;
        }
        private Node min(Node x)
        {
            if (x.Left == null)
                return x;
            else
                return this.min(x.Left);
        }

        public Key Max()
        {
            if (this.IsEmpty())
                throw new NullReferenceException("called max() with empty symbol table");

            return this.max(this._root).Key;
        }
        private Node max(Node x)
        {
            if (x.Right == null)
                return x;
            else
                return this.max(x.Right);
        }

        /// <summary>
        /// Returns the largest key in the symbol table less than or equal to key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Key Floor(Key key)
        {
            if (this.IsEmpty())
                throw new NullReferenceException("called floor() with empty symbol table");

            Node x = this.floor(this._root, key);
            return x.Key;
        }
        private Node floor(Node x, Key key)
        {
            if (x == null)
                return null;

            int cmp = key.CompareTo(x.Key);
            if (cmp == 0)
                return x;
            if (cmp < 0)
                return this.floor(x.Left, key);

            Node t = this.floor(x.Right, key);
            if (t != null)
                return t;
            else
                return x;
        }

        /// <summary>
        /// Returns the smallest key in the symbol table greater than or equal to key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Key Ceiling(Key key)
        {
            if (this.IsEmpty())
                throw new NullReferenceException("called ceiling() with empty symbol table");

            Node x = this.ceiling(this._root, key);
            return x.Key;
        }
        private Node ceiling(Node x, Key key)
        {
            if (x == null)
                return null;

            int cmp = key.CompareTo(x.Key);
            if (cmp == 0)
                return x;
            if (cmp < 0)
            {
                Node t = this.ceiling(x.Left, key);
                if (t != null)
                    return t;
                else
                    return x;
            }
            return this.ceiling(x.Right, key);
        }

        /// <summary>
        /// Return the number of keys in the symbol table strictly less than key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public int Rank(Key key)
        {
            return this.rank(key, this._root);
        }
        private int rank(Key key, Node x)
        {
            if (x == null)
                throw new NullReferenceException("called rank() with empty symbol table");

            int cmp = key.CompareTo(x.Key);
            if (cmp < 0)
                return rank(key, x.Left);
            else if (cmp > 0)
                return 1 + size(x.Left) + this.rank(key, x.Right);
            else
                return this.size(x.Left);
        }

        /// <summary>
        /// Return the kth smallest key in the symbol table.
        /// </summary>
        /// <param name="k">the order statistic</param>
        /// <returns></returns>
        public Key Select(int k)
        {
            if (k < 0 || k >= Size())
                throw new KeyNotFoundException();

            Node x = this.select(this._root, k);
            return x.Key;
        }
        private Node select(Node x, int k)
        {
            if (x == null)
                return null;

            int t = this.size(x.Left);
            if (t > k)
                return this.select(x.Left, k);
            else if (t < k)
                return this.select(x.Right, k - t - 1);
            else
                return x;
        }

        public void DeleteMin()
        {
            if (this.IsEmpty())
                throw new NullReferenceException("Symbol table underflow");

            this._root = this.deleteMin(this._root);
        }
        private Node deleteMin(Node x)
        {
            if (x.Left == null)
                return x.Right;

            x.Left = this.deleteMin(x.Left);
            x.N = this.size(x.Left) + this.size(x.Right) + 1;

            return x;
        }

        public void DeleteMax()
        {
            if (this.IsEmpty())
                throw new NullReferenceException("Symbol table underflow");

            this._root = this.deleteMax(this._root);
        }
        private Node deleteMax(Node x)
        {
            if (x.Right == null)
                return x.Left;

            x.Right = this.deleteMax(x.Right);
            x.N = this.size(x.Left) + this.size(x.Right) + 1;

            return x;
        }

        public void Delete(Key key)
        {
            this._root = this.delete(this._root, key);
        }
        private Node delete(Node x, Key key)
        {
            if (x == null)
                return null;

            int cmp = key.CompareTo(x.Key);
            if (cmp < 0)
                x.Left = this.delete(x.Left, key);
            else if (cmp > 0)
                x.Right = this.delete(x.Right, key);
            else
            {
                if (x.Right == null)
                    return x.Left;
                if (x.Left == null)
                    return x.Right;

                Node t = x;
                x = this.min(t.Right);
                x.Right = this.deleteMin(t.Right);
                x.Left = t.Left;
            }
            x.N = this.size(x.Left) + this.size(x.Right) + 1;

            return x;
        }

        /// <summary>
        /// Returns all keys in the symbol table as an Iterable.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Key> Keys()
        {
            return this.Keys(this.Min(), this.Max());
        }
        public IEnumerable<Key> Keys(Key lo, Key hi)
        {
            Queue<Key> queue = new Queue<Key>();
            this.keys(this._root, queue, lo, hi);
            return queue;
        }
        private void keys(Node x, Queue<Key> queue, Key lo, Key hi)
        {
            if (x == null)
                return;

            int cmplo = lo.CompareTo(x.Key);
            int cmphi = hi.CompareTo(x.Key);
            if (cmplo < 0)
                this.keys(x.Left, queue, lo, hi);
            if (cmplo <= 0 && cmphi >= 0)
                queue.Enqueue(x.Key);
            if (cmphi > 0)
                this.keys(x.Right, queue, lo, hi);
        }

        /// <summary>
        /// Returns the height of the BST (for debugging).
        /// </summary>
        /// <returns></returns>
        public int Height()
        {
            return this.height(this._root);
        }
        private int height(Node x)
        {
            if (x == null)
                return -1;
            return 1 + Math.Max(this.height(x.Left), this.height(x.Right));
        }

        /// <summary>
        /// Returns the keys in the BST in level order (for debugging).
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Key> LevelOrder()
        {
            Queue<Key> keys = new Queue<Key>();
            Queue<Node> queue = new Queue<Node>();
            queue.Enqueue(this._root);
            while (queue.Count() != 0)
            {
                Node x = queue.Dequeue();
                if (x == null) continue;
                keys.Enqueue(x.Key);
                queue.Enqueue(x.Left);
                queue.Enqueue(x.Right);
            }
            return keys;
        }

        /// <summary>
        /// Check integrity of BST data structure.
        /// </summary>
        /// <returns></returns>
        private bool check()
        {
            if (!isBST())
                Debug.WriteLine("Not in symmetric order");
            if (!isSizeConsistent())
                Debug.WriteLine("Subtree counts not consistent");
            if (!isRankConsistent())
                Debug.WriteLine("Ranks not consistent");

            return this.isBST() && this.isSizeConsistent() && this.isRankConsistent();
        }

        /// <summary>
        /// does this binary tree satisfy symmetric order?
        /// Note: this test also ensures that data structure is a binary tree since order is strict 
        /// </summary>
        /// <returns></returns>
        private bool isBST()
        {
            return this.isBST(this._root, default(Key), default(Key));
        }
        /// <summary>
        /// is the tree rooted at x a BST with all keys strictly between min and max
        /// (if min or max is null, treat as empty constraint)
        /// </summary>
        /// <param name="x"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        private bool isBST(Node x, Key min, Key max)
        {
            if (x == null)
                return true;
            if (min != null && x.Key.CompareTo(min) <= 0)
                return false;
            if (max != null && x.Key.CompareTo(max) >= 0)
                return false;

            return this.isBST(x.Left, min, x.Key) && this.isBST(x.Right, x.Key, max);
        }

        /// <summary>
        /// are the size fields correct?
        /// </summary>
        /// <returns></returns>
        private bool isSizeConsistent()
        {
            return this.isSizeConsistent(this._root);
        }
        private bool isSizeConsistent(Node x)
        {
            if (x == null)
                return true;

            if (x.N != this.size(x.Left) + this.size(x.Right) + 1)
                return false;
            return
                this.isSizeConsistent(x.Left) && this.isSizeConsistent(x.Right);
        }

        /// <summary>
        /// check that ranks are consistent
        /// </summary>
        /// <returns></returns>
        private bool isRankConsistent()
        {
            for (int i = 0; i < this.Size(); i++)
                if (i != this.Rank(this.Select(i)))
                    return false;
            foreach (Key key in this.Keys())
                if (key.CompareTo(this.Select(this.Rank(key))) != 0)
                    return false;

            return true;
        }
    }
}
