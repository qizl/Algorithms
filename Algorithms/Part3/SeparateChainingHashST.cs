using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Part3
{
    public class SeparateChainingHashST<Key, Value>
    {
        private const int INIT_CAPACITY = 997;

        /// <summary>
        /// number of key-value pairs
        /// </summary>
        private int _n;

        /// <summary>
        /// hash table size
        /// </summary>
        private int _m;
        private SequentialSearchST<Key, Value>[] _st;

        public SeparateChainingHashST()
            : this(INIT_CAPACITY)
        { }

        public SeparateChainingHashST(int m)
        {
            this._m = m;
            this._st = new SequentialSearchST<Key, Value>[m];
            for (int i = 0; i < m; i++)
                this._st[i] = new SequentialSearchST<Key, Value>();
        }

        /// <summary>
        /// resize the hash table to have the given number of chains b rehashing all of the keys
        /// </summary>
        /// <param name="chains"></param>
        private void resize(int chains)
        {
            SeparateChainingHashST<Key, Value> temp = new SeparateChainingHashST<Key, Value>(chains);
            for (int i = 0; i < this._m; i++)
                foreach (Key key in this._st[i].Keys())
                    temp.Put(key, this._st[i].Get(key));

            this._m = temp._m;
            this._n = temp._n;
            this._st = temp._st;
        }

        /// <summary>
        /// hash value between 0 and M-1
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        private int hash(Key key)
        {
            return (key.GetHashCode() & 0x7fffffff) % this._m;
        }

        public Value Get(Key key)
        {
            int i = this.hash(key);
            return this._st[i].Get(key);
        }

        public void Put(Key key, Value val)
        {
            // double table size if average length of list >= 10
            if (this._n >= 10 * this._m)
                this.resize(2 * this._m);

            int i = this.hash(key);
            if (!this._st[i].Contains(key))
                this._n++;
            this._st[i].Put(key, val);
        }

        public void Delete(Key key)
        {
            int i = this.hash(key);
            if (this._st[i].Contains(key))
                this._n--;
            this._st[i].Delete(key);

            // halve table size if average length of list <= 2
            if (this._m > INIT_CAPACITY && this._n <= 2 * this._m)
                this.resize(this._m / 2);
        }

        public bool Contains(Key key)
        {
            return this.Get(key) != null;
        }

        public int Size()
        {
            return this._n;
        }

        public bool IsEmpty()
        {
            return this.Size() == 0;
        }

        public IEnumerable<Key> Keys()
        {
            Queue<Key> queue = new Queue<Key>();
            for (int i = 0; i < this._m; i++)
                foreach (Key key in this._st[i].Keys())
                    queue.Enqueue(key);

            return queue;
        }
    }
}
