using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Part3
{
    public class BinarySearchST<Key, Value>
        where Key : IComparable
    {
        private static readonly int INIT_CAPACITY = 2;
        private Key[] _keys;
        private Value[] _values;
        private int _n = 0;

        public BinarySearchST()
            : this(INIT_CAPACITY)
        {
        }

        public BinarySearchST(int capacity)
        {
            this._keys = new Key[capacity];
            this._values = new Value[capacity];
        }

        private void resize(int capacity)
        {
            //assert capacity >= _n;
            Key[] tempk = new Key[capacity];
            Value[] tempv = new Value[capacity];
            for (int i = 0; i < _n; i++)
            {
                tempk[i] = this._keys[i];
                tempv[i] = this._values[i];
            }
            this._values = tempv;
            this._keys = tempk;
        }

        public int Size()
        {
            return this._n;
        }

        public bool IsEmpty()
        {
            return this.Size() == 0;
        }

        public bool Contains(Key key)
        {
            //if (key == null)
            //    throw new NullPointerException("argument to contains() is null");

            return this.Get(key) != null;
        }

        public Value Get(Key key)
        {
            //if (key == null) throw new NullPointerException("argument to get() is null");
            if (IsEmpty()) return default(Value);

            int i = Rank(key);
            if (i < _n && this._keys[i].CompareTo(key) == 0)
                return _values[i];

            return default(Value);
        }

        public int Rank(Key key)
        {
            //if (key == null) throw new NullPointerException("argument to rank() is null");

            int lo = 0, hi = _n - 1;
            while (lo <= hi)
            {
                int mid = lo + (hi - lo) / 2;
                int cmp = key.CompareTo(this._keys[mid]);
                if (cmp < 0) hi = mid - 1;
                else if (cmp > 0) lo = mid + 1;
                else return mid;
            }
            return lo;
        }


        public void Put(Key key, Value val)
        {
            //if (key == null) throw new NullPointerException("first argument to put() is null");

            if (val == null)
            {
                Delete(key);
                return;
            }

            int i = Rank(key);

            // key is already in table
            if (i < _n && this._keys[i].CompareTo(key) == 0)
            {
                _values[i] = val;
                return;
            }

            // insert new key-value pair
            if (_n == this._keys.Length)
                this.resize(2 * this._keys.Length);

            for (int j = _n; j > i; j--)
            {
                this._keys[j] = this._keys[j - 1];
                this._values[j] = this._values[j - 1];
            }
            this._keys[i] = key;
            this._values[i] = val;
            this._n++;

            //assert check();
        }

        public void Delete(Key key)
        {
            //if (key == null) throw new NullPointerException("argument to delete() is null");
            if (IsEmpty()) return;

            // compute rank
            int i = Rank(key);

            // key not in table
            if (i == _n || this._keys[i].CompareTo(key) != 0)
            {
                return;
            }

            for (int j = i; j < _n - 1; j++)
            {
                this._keys[j] = this._keys[j + 1];
                _values[j] = _values[j + 1];
            }

            _n--;
            this._keys[_n] = null;  // to avoid loitering
            _values[_n] = null;

            // resize if 1/4 full
            if (_n > 0 && _n == this._keys.Length / 4) resize(this._keys.Length / 2);

            //assert check();
        }

        public void deleteMin()
        {
            if (IsEmpty()) throw new NoSuchElementException("Symbol table underflow error");
            Delete(min());
        }

        public void deleteMax()
        {
            if (IsEmpty()) throw new NoSuchElementException("Symbol table underflow error");
            Delete(max());
        }


        public Key min()
        {
            if (IsEmpty()) return null;
            return this._keys[0];
        }

        public Key max()
        {
            if (IsEmpty()) return null;
            return this._keys[_n - 1];
        }

        public Key select(int k)
        {
            if (k < 0 || k >= _n) return null;
            return this._keys[k];
        }

        public Key floor(Key key)
        {
            if (key == null) throw new NullPointerException("argument to floor() is null");
            int i = Rank(key);
            if (i < _n && key.CompareTo(this._keys[i]) == 0) return this._keys[i];
            if (i == 0) return null;
            else return this._keys[i - 1];
        }

        public Key ceiling(Key key)
        {
            if (key == null) throw new NullPointerException("argument to ceiling() is null");
            int i = Rank(key);
            if (i == _n) return null;
            else return this._keys[i];
        }

        public int size(Key lo, Key hi)
        {
            if (lo == null) throw new NullPointerException("first argument to size() is null");
            if (hi == null) throw new NullPointerException("second argument to size() is null");

            if (lo.CompareTo(hi) > 0) return 0;
            if (Contains(hi)) return Rank(hi) - Rank(lo) + 1;
            else return Rank(hi) - Rank(lo);
        }

        public Iterable<Key> this._keys()
        {
            return this._keys(min(), max());
        }

        public Iterable<Key> this._keys(Key lo, Key hi)
        {
            if (lo == null) throw new NullPointerException("first argument to size() is null");
            if (hi == null) throw new NullPointerException("second argument to size() is null");

            Queue<Key> queue = new Queue<Key>();
            // if (lo == null && hi == null) return queue;
            if (lo == null) throw new NullPointerException("lo is null in this._keys()");
            if (hi == null) throw new NullPointerException("hi is null in this._keys()");
            if (lo.CompareTo(hi) > 0) return queue;
            for (int i = Rank(lo); i < Rank(hi); i++)
                queue.enqueue(this._keys[i]);
            if (Contains(hi)) queue.enqueue(this._keys[Rank(hi)]);
            return queue;
        }


        private bool check()
        {
            return isSorted() && rankCheck();
        }

        private bool isSorted()
        {
            for (int i = 1; i < Size(); i++)
                if (this._keys[i].CompareTo(this._keys[i - 1]) < 0) return false;
            return true;
        }

        private bool rankCheck()
        {
            for (int i = 0; i < Size(); i++)
                if (i != Rank(select(i))) return false;
            for (int i = 0; i < Size(); i++)
                if (this._keys[i].CompareTo(select(rank(this._keys[i]))) != 0) return false;
            return true;
        }
    }
}
