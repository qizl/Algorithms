using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Algorithms.Part3
{
    public class LinearProbingHashST<Key, Value>
    {
        private const int INIT_CAPACITY = 4;

        /// <summary>
        /// number of key-value pairs in the symbol table
        /// </summary>
        private int _n;

        /// <summary>
        /// size of linear probing table
        /// </summary>
        private int _m;
        private Key[] _keys;
        private Value[] _values;

        public LinearProbingHashST()
            : this(INIT_CAPACITY)
        { }

        public LinearProbingHashST(int capacity)
        {
            this._m = capacity;
            this._keys = new Key[this._m];
            this._values = new Value[this._m];
        }

        /// <summary>
        /// hash function for keys - returns value between 0 and M-1
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private int hash(Key key)
        {
            return (key.GetHashCode() & 0x7fffffff) % _m;
        }

        /// <summary>
        /// resizes the hash table to the given capacity by re-hashing all of the keys
        /// </summary>
        /// <param name="capacity"></param>
        private void resize(int capacity)
        {
            LinearProbingHashST<Key, Value> temp = new LinearProbingHashST<Key, Value>(capacity);
            for (int i = 0; i < this._m; i++)
                if (this._keys[i] != null)
                    temp.Put(this._keys[i], this._values[i]);

            this._keys = temp._keys;
            this._values = temp._values;
            this._m = temp._m;
        }

        public void Put(Key key, Value val)
        {
            // double table size if 50% full
            if (this._n >= this._m / 2)
                this.resize(2 * this._m);

            int i;
            for (i = this.hash(key); this._keys[i] != null; i = (i + 1) % this._m)
                if (this._keys[i].Equals(key))
                {
                    this._values[i] = val;
                    return;
                }
            this._keys[i] = key;
            this._values[i] = val;
            this._n++;
        }

        public Value Get(Key key)
        {
            for (int i = hash(key); this._keys[i] != null; i = (i + 1) % this._m)
                if (this._keys[i].Equals(key))
                    return this._values[i];

            return default(Value);
        }

        public void Delete(Key key)
        {
            if (!this.Contains(key))
                return;

            // find position i of key
            int i = this.hash(key);
            while (!key.Equals(this._keys[i]))
                i = (i + 1) % this._m;

            // delete key and associated value
            this._keys[i] = default(Key);
            this._values[i] = default(Value);

            // rehash all keys in same cluster
            i = (i + 1) % this._m;
            while (this._keys[i] != null)
            {
                // delete keys[i] an vals[i] and reinsert
                Key keyToRehash = this._keys[i];
                Value valToRehash = this._values[i];
                this._keys[i] = default(Key);
                this._values[i] = default(Value);
                this._n--;
                this.Put(keyToRehash, valToRehash);
                i = (i + 1) % this._m;
            }

            this._n--;

            // halves size of array if it's 12.5% full or less
            if (this._n > 0 && this._n <= this._m / 8)
                this.resize(this._m / 2);
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
            return this.Get(key) != null;
        }

        public IEnumerable<Key> Keys()
        {
            Queue<Key> queue = new Queue<Key>();
            for (int i = 0; i < this._m; i++)
                if (this._keys[i] != null) queue.Enqueue(this._keys[i]);
            return queue;
        }

        // integrity check - don't check after each put() because
        // integrity not maintained during a delete()
        private bool check()
        {
            // check that hash table is at most 50% full
            if (this._m < 2 * this._n)
            {
                Debug.WriteLine("Hash table size M = " + this._m + "; array size N = " + this._n);
                return false;
            }

            // check that each key in table can be found by get()
            for (int i = 0; i < this._m; i++)
            {
                if (this._keys[i] == null)
                    continue;
                else if (!this.Get(this._keys[i]).Equals(this._values[i]))
                {
                    Debug.WriteLine("get[" + this._keys[i] + "] = " + this.Get(this._keys[i]) + "; vals[i] = " + this._values[i]);
                    return false;
                }
            }
            return true;
        }
    }
}
