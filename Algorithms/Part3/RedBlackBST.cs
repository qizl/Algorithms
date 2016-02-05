
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Algorithms.Part3
{
    public class RedBlackBST<Key, Value>
        where Key : IComparable
    {
        #region Members
        private const bool RED = true;
        private const bool BLACK = false;

        private Node _root;
        #endregion

        #region BST helper node data type
        private class Node
        {
            public Key Key { get; set; }
            public Value Value { get; set; }
            public Node Left { get; set; }
            public Node Right { get; set; }
            public bool Color { get; set; }
            public int N { get; set; }

            public Node(Key key, Value val, bool color, int N)
            {
                this.Key = key;
                this.Value = val;
                this.Color = color;
                this.N = N;
            }
        }
        #endregion

        #region Node helper methods
        /// <summary>
        /// is node x red; false if x is null ?
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private bool isRed(Node x)
        {
            if (x == null)
                return false;

            return x.Color == RED;
        }

        /// <summary>
        /// Returns the number of key-value pairs in this symbol table.
        /// </summary>
        /// <returns></returns>
        public int Size()
        {
            return this.size(this._root);
        }
        /// <summary>
        /// number of node in subtree rooted at x; 0 if x is null
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        private int size(Node x)
        {
            if (x == null)
                return 0;

            return x.N;
        }

        /// <summary>
        /// Is this symbol table empty?
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            return this._root == null;
        }
        #endregion

        #region Standard BST search
        /// <summary>
        /// Returns the value associated with the given key. 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public Value Get(Key key)
        {
            return this.get(this._root, key);
        }
        private Value get(Node x, Key key)
        {
            while (x != null)
            {
                int cmp = key.CompareTo(x.Key);

                if (cmp < 0)
                    x = x.Left;
                else if (cmp > 0)
                    x = x.Right;
                else
                    return x.Value;
            }

            throw new NullReferenceException("called get() with empty symbol table");
        }

        public bool Contains(Key key)
        {
            return this.Get(key) != null;
        }
        #endregion

        #region Red-black tree insertion
        /// <summary>
        /// Inserts the specified key-value pair into the symbol table, overwriting the old 
        /// value with the new value if the symbol table already contains the specified key.
        /// Deletes the specified key (and its associated value) from this symbol table
        /// if the specified value is null.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Put(Key key, Value value)
        {
            if (value == null)
            {
                this.Delete(key);
                return;
            }

            this._root = this.put(this._root, key, value);
            this._root.Color = BLACK;
        }
        private Node put(Node h, Key key, Value val)
        {
            if (h == null)
                return new Node(key, val, RED, 1);

            int cmp = key.CompareTo(h.Key);
            if (cmp < 0)
                h.Left = this.put(h.Left, key, val);
            else if (cmp > 0)
                h.Right = this.put(h.Right, key, val);
            else
                h.Value = val;

            // fix-up any right-leaning links
            if (this.isRed(h.Right) && !this.isRed(h.Left))
                h = this.rotateLeft(h);
            if (this.isRed(h.Left) && this.isRed(h.Left.Left))
                h = this.rotateRight(h);
            if (this.isRed(h.Left) && this.isRed(h.Right))
                this.flipColors(h);
            h.N = this.size(h.Left) + this.size(h.Right) + 1;

            return h;
        }
        #endregion

        #region Red-black tree deletion
        public void DeleteMin()
        {
            if (this.IsEmpty())
                throw new KeyNotFoundException("BST underflow");

            // if both children of root are black, set root to red
            if (!this.isRed(this._root.Left) && !this.isRed(this._root.Right))
                this._root.Color = RED;

            this._root = this.deleteMin(this._root);
            if (!this.IsEmpty())
                this._root.Color = BLACK;
        }
        private Node deleteMin(Node h)
        {
            if (h.Left == null)
                return null;

            if (!isRed(h.Left) && !isRed(h.Left.Left))
                h = moveRedLeft(h);

            h.Left = deleteMin(h.Left);
            return balance(h);
        }

        public void DeleteMax()
        {
            if (IsEmpty()) throw new KeyNotFoundException("BST underflow");

            // if both children of root are black, set root to red
            if (!isRed(this._root.Left) && !isRed(this._root.Right))
                this._root.Color = RED;

            this._root = deleteMax(this._root);
            if (!IsEmpty()) this._root.Color = BLACK;
            // assert check();
        }
        private Node deleteMax(Node h)
        {
            if (isRed(h.Left))
                h = rotateRight(h);

            if (h.Right == null)
                return null;

            if (!isRed(h.Right) && !isRed(h.Right.Left))
                h = moveRedRight(h);

            h.Right = deleteMax(h.Right);

            return balance(h);
        }

        public void Delete(Key key)
        {
            if (key == null) throw new KeyNotFoundException("argument to delete() is null");
            if (!Contains(key)) return;

            // if both children of root are black, set root to red
            if (!isRed(this._root.Left) && !isRed(this._root.Right))
                this._root.Color = RED;

            this._root = delete(this._root, key);
            if (!IsEmpty()) this._root.Color = BLACK;
            // assert check();
        }
        private Node delete(Node h, Key key)
        {
            // assert get(h, key) != null;

            if (key.CompareTo(h.Key) < 0)
            {
                if (!isRed(h.Left) && !isRed(h.Left.Left))
                    h = moveRedLeft(h);
                h.Left = delete(h.Left, key);
            }
            else {
                if (isRed(h.Left))
                    h = rotateRight(h);
                if (key.CompareTo(h.Key) == 0 && (h.Right == null))
                    return null;
                if (!isRed(h.Right) && !isRed(h.Right.Left))
                    h = moveRedRight(h);
                if (key.CompareTo(h.Key) == 0)
                {
                    Node x = min(h.Right);
                    h.Key = x.Key;
                    h.Value = x.Value;
                    // h.val = get(h.right, min(h.right).key);
                    // h.key = min(h.right).key;
                    h.Right = deleteMin(h.Right);
                }
                else h.Right = delete(h.Right, key);
            }
            return balance(h);
        }
        #endregion

        #region  Red-black tree helper functions
        /// <summary>
        /// make a left-leaning link lean to the right
        /// </summary>
        /// <param name="h"></param>
        /// <returns></returns>
        private Node rotateRight(Node h)
        {
            Node x = h.Left;
            h.Left = x.Right;
            x.Right = h;
            x.Color = x.Right.Color;
            x.Right.Color = RED;
            x.N = h.N;
            h.N = this.size(h.Left) + this.size(h.Right) + 1;
            return x;
        }

        /// <summary>
        /// make a right-leaning link lean to the left
        /// </summary>
        /// <param name="h"></param>
        /// <returns></returns>
        private Node rotateLeft(Node h)
        {
            Node x = h.Right;
            h.Right = x.Left;
            x.Left = h;
            x.Color = x.Left.Color;
            x.Left.Color = RED;
            x.N = h.N;
            h.N = this.size(h.Left) + this.size(h.Right) + 1;
            return x;
        }

        /// <summary>
        /// flip the colors of a node and its two children
        /// </summary>
        /// <param name="h"></param>
        private void flipColors(Node h)
        {
            // h must have opposite color of its two children
            h.Color = !h.Color;
            h.Left.Color = !h.Left.Color;
            h.Right.Color = !h.Right.Color;
        }

        /// <summary>
        /// Assuming that h is red and both h.left and h.left.left
        /// are black, make h.left or one of its children red.
        /// </summary>
        /// <param name="h"></param>
        /// <returns></returns>
        private Node moveRedLeft(Node h)
        {
            this.flipColors(h);
            if (this.isRed(h.Right.Left))
            {
                h.Right = this.rotateRight(h.Right);
                h = this.rotateLeft(h);
                this.flipColors(h);
            }
            return h;
        }

        /// <summary>
        /// Assuming that h is red and both h.right and h.right.left
        /// are black, make h.right or one of its children red.
        /// </summary>
        /// <param name="h"></param>
        /// <returns></returns>
        private Node moveRedRight(Node h)
        {
            this.flipColors(h);
            if (this.isRed(h.Left.Left))
            {
                h = this.rotateRight(h);
                this.flipColors(h);
            }
            return h;
        }

        /// <summary>
        /// restore red-black tree invariant
        /// </summary>
        /// <param name="h"></param>
        /// <returns></returns>
        private Node balance(Node h)
        {
            if (this.isRed(h.Right))
                h = this.rotateLeft(h);
            if (this.isRed(h.Left) && this.isRed(h.Left.Left))
                h = this.rotateRight(h);
            if (this.isRed(h.Left) && this.isRed(h.Right))
                this.flipColors(h);

            h.N = this.size(h.Left) + this.size(h.Right) + 1;
            return h;
        }
        #endregion

        #region Utility functions
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
        #endregion

        #region Ordered symbol table methods
        public Key Min()
        {
            if (this.IsEmpty())
                throw new KeyNotFoundException("called min() with empty symbol table");

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
                throw new KeyNotFoundException("called max() with empty symbol table");
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
                throw new KeyNotFoundException("called floor() with empty symbol table");
            Node x = this.floor(this._root, key);
            if (x == null)
                throw new NullReferenceException("called floor() with empty symbol table");
            else
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
                throw new KeyNotFoundException("called ceiling() with empty symbol table");
            Node x = this.ceiling(this._root, key);
            if (x == null)
                throw new NullReferenceException("called ceiling() with empty symbol table");
            else
                return x.Key;
        }
        private Node ceiling(Node x, Key key)
        {
            if (x == null)
                return null;

            int cmp = key.CompareTo(x.Key);
            if (cmp == 0)
                return x;
            if (cmp > 0)
                return this.ceiling(x.Right, key);
            Node t = this.ceiling(x.Left, key);
            if (t != null)
                return t;
            else
                return x;
        }

        /// <summary>
        /// Return the kth smallest key in the symbol table.
        /// </summary>
        /// <param name="k"></param>
        /// <returns></returns>
        public Key Select(int k)
        {
            if (k < 0 || k >= this.Size())
                throw new Exception();
            Node x = this.select(this._root, k);
            return x.Key;
        }
        private Node select(Node x, int k)
        {
            int t = this.size(x.Left);
            if (t > k)
                return this.select(x.Left, k);
            else if (t < k)
                return this.select(x.Right, k - t - 1);
            else
                return x;
        }

        /// <summary>
        /// Return the number of keys in the symbol table strictly less than key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public int Rank(Key key)
        {
            if (key == null)
                throw new NullReferenceException("argument to rank() is null");
            return this.rank(key, this._root);
        }
        private int rank(Key key, Node x)
        {
            if (x == null)
                return 0;

            int cmp = key.CompareTo(x.Key);
            if (cmp < 0)
                return this.rank(key, x.Left);
            else if (cmp > 0)
                return 1 + this.size(x.Left) + this.rank(key, x.Right);
            else
                return this.size(x.Left);
        }
        #endregion

        #region Range count and range search
        /// <summary>
        /// Returns all keys in the symbol table as an Iterable.
        /// To iterate over all of the keys in the symbol table named st,
        /// use the foreach notation: for (Key key : st.keys()).
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Key> Keys()
        {
            if (this.IsEmpty())
                return new Queue<Key>();
            return this.Keys(this.Min(), this.Max());
        }
        public IEnumerable<Key> Keys(Key lo, Key hi)
        {
            if (lo == null)
                throw new NullReferenceException("first argument to keys() is null");
            if (hi == null)
                throw new NullReferenceException("second argument to keys() is null");

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
        /// Returns the number of keys in the symbol table in the given range.
        /// </summary>
        /// <param name="lo"></param>
        /// <param name="hi"></param>
        /// <returns></returns>
        public int Size(Key lo, Key hi)
        {
            if (lo == null)
                throw new NullReferenceException("first argument to size() is null");
            if (hi == null)
                throw new NullReferenceException("second argument to size() is null");

            if (lo.CompareTo(hi) > 0)
                return 0;
            if (this.Contains(hi))
                return this.Rank(hi) - this.Rank(lo) + 1;
            else return this.Rank(hi) - this.Rank(lo);
        }
        #endregion

        #region Check integrity of red-black tree data structure
        private bool check()
        {
            if (!this.isBST()) Debug.WriteLine("Not in symmetric order");
            if (!this.isSizeConsistent()) Debug.WriteLine("Subtree counts not consistent");
            if (!this.isRankConsistent()) Debug.WriteLine("Ranks not consistent");
            if (!this.is23()) Debug.WriteLine("Not a 2-3 tree");
            if (!this.isBalanced()) Debug.WriteLine("Not balanced");
            return this.isBST() && this.isSizeConsistent() && this.isRankConsistent() && this.is23() && this.isBalanced();
        }

        // does this binary tree satisfy symmetric order?
        // Note: this test also ensures that data structure is a binary tree since order is strict
        private bool isBST()
        {
            return isBST(this._root, default(Key), default(Key));
        }

        // is the tree rooted at x a BST with all keys strictly between min and max
        // (if min or max is null, treat as empty constraint)
        // Credit: Bob Dondero's elegant solution
        private bool isBST(Node x, Key min, Key max)
        {
            if (x == null) return true;
            if (min != null && x.Key.CompareTo(min) <= 0) return false;
            if (max != null && x.Key.CompareTo(max) >= 0) return false;
            return isBST(x.Left, min, x.Key) && isBST(x.Right, x.Key, max);
        }

        // are the size fields correct?
        private bool isSizeConsistent() { return isSizeConsistent(this._root); }
        private bool isSizeConsistent(Node x)
        {
            if (x == null) return true;
            if (x.N != size(x.Left) + size(x.Right) + 1) return false;
            return isSizeConsistent(x.Left) && isSizeConsistent(x.Right);
        }

        // check that ranks are consistent
        private bool isRankConsistent()
        {
            for (int i = 0; i < Size(); i++)
                if (i != this.Rank(Select(i)))

                    return false;
            foreach (var key in this.Keys())
                if (key.CompareTo(Select(this.Rank(key))) != 0) return false;
            return true;
        }

        // Does the tree have no red right links, and at most one (left)
        // red links in a row on any path?
        private bool is23() { return is23(this._root); }
        private bool is23(Node x)
        {
            if (x == null) return true;
            if (isRed(x.Right)) return false;
            if (x != this._root && isRed(x) && isRed(x.Left))
                return false;
            return is23(x.Left) && is23(x.Right);
        }

        // do all paths from root to leaf have same number of black edges?
        private bool isBalanced()
        {
            int black = 0;     // number of black links on path from root to min
            Node x = this._root;
            while (x != null)
            {
                if (!isRed(x)) black++;
                x = x.Left;
            }
            return isBalanced(this._root, black);
        }

        // does every path from the root to a leaf have the given number of black links?
        private bool isBalanced(Node x, int black)
        {
            if (x == null) return black == 0;
            if (!isRed(x)) black--;
            return isBalanced(x.Left, black) && isBalanced(x.Right, black);
        }
        #endregion
    }
}
