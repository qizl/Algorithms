using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Part2
{
    public class MaxPQ
    {
        /// <summary>
        /// heap-ordered complete binary tree
        /// </summary>
        public IComparable[] PQs { get; private set; }

        /// <summary>
        /// in pq[1..N] with pq[0] unused
        /// </summary>
        private int _n = 0;

        public MaxPQ(int maxN)
        {
            this.PQs = new IComparable[maxN + 1];
        }

        public bool IsEmpty()
        {
            return this._n == 0;
        }

        public int Size()
        {
            return this._n;
        }

        public void Insert(IComparable v)
        {
            this.PQs[++this._n] = v;
            this.swim(this._n);
        }

        public IComparable DelMax()
        {
            IComparable max = this.PQs[1];

            this.exch(1, this._n--);
            this.PQs[this._n + 1] = null;
            this.sink(1);

            return max;
        }

        private bool isLess(int i, int j)
        {
            return this.PQs[i].CompareTo(this.PQs[j]) < 0;
        }

        private void exch(int i, int j)
        {
            IComparable t = this.PQs[i];
            this.PQs[i] = this.PQs[j];
            this.PQs[j] = t;
        }

        /// <summary>
        /// 上浮
        ///     新元素插入尾部，执行上浮
        /// </summary>
        /// <param name="k"></param>
        private void swim(int k)
        {
            while (k > 1 && this.isLess(k / 2, k))
            {
                this.exch(k / 2, k);
                k = k / 2;
            }
        }

        /// <summary>
        /// 下沉
        ///     删除顶部元素后，将尾部元素移到顶部，执行下沉
        /// </summary>
        /// <param name="k"></param>
        private void sink(int k)
        {
            while (2 * k <= this._n)
            {
                int j = 2 * k;

                if (j < this._n && this.isLess(j, j + 1))
                    j++;

                if (!this.isLess(k, j))
                    break;

                this.exch(k, j);
                k = j;
            }
        }
    }

    public class MinPQ<Key> : IComparable<Key>
    {
        /// <summary>
        /// heap-ordered complete binary tree
        /// </summary>
        public Key[] PQs { get; private set; }

        /// <summary>
        /// in pq[1..N] with pq[0] unused
        /// </summary>
        private int _n = 0;

        public MinPQ()
        {
            this.PQs = new Key[1];
        }

        public bool IsEmpty()
        {
            return this._n == 0;
        }

        public int Size()
        {
            return this._n;
        }

        public Key Min()
        {
            if (this.IsEmpty())
                throw new KeyNotFoundException("Priority queue underflow");
            return this.PQs[1];
        }

        /// <summary>
        /// helper function to double the size of the heap array
        /// </summary>
        /// <param name="capacity"></param>
        private void resize(int capacity)
        {
            Key[] temp = new Key[capacity];
            for (int i = 1; i <= this._n; i++)
                temp[i] = PQs[i];
            this.PQs = temp;
        }

        public void Insert(Key v)
        {
            if (this._n == this.PQs.Length - 1)
                this.resize(2 * this.PQs.Length);

            this.PQs[++this._n] = v;
            this.swim(this._n);
        }

        public Key DelMin()
        {
            if (this.IsEmpty())
                throw new KeyNotFoundException("Priority queue underflow");

            this.exch(1, this._n);

            Key min = this.PQs[this._n--];

            this.sink(1);
            this.PQs[this._n + 1] = default(Key);
            if ((this._n > 0) && (this._n == (this.PQs.Length - 1) / 4))
                this.resize(this.PQs.Length / 2);

            return min;
        }

        private bool greater(int i, int j) { return ((IComparable<Key>)this.PQs[i]).CompareTo(this.PQs[j]) > 0; }

        private void exch(int i, int j)
        {
            Key t = this.PQs[i];
            this.PQs[i] = this.PQs[j];
            this.PQs[j] = t;
        }

        /// <summary>
        /// 上浮
        ///     新元素插入尾部，执行上浮
        /// </summary>
        /// <param name="k"></param>
        private void swim(int k)
        {
            while (k > 1 && this.greater(k / 2, k))
            {
                this.exch(k, k / 2);
                k = k / 2;
            }
        }

        /// <summary>
        /// 下沉
        ///     删除顶部元素后，将尾部元素移到顶部，执行下沉
        /// </summary>
        /// <param name="k"></param>
        private void sink(int k)
        {
            while (2 * k <= this._n)
            {
                int j = 2 * k;

                if (j < this._n && this.greater(j, j + 1))
                    j++;

                if (!this.greater(k, j))
                    break;

                this.exch(k, j);
                k = j;
            }
        }

        public int CompareTo(Key other)
        {
            throw new NotImplementedException();
        }
    }

    public class IndexMinPQ<Key> : IComparable<Key>
    {
        private int maxN;        // maximum number of elements on PQ
        private int N;           // number of elements on PQ
        private int[] pq;        // binary heap using 1-based indexing
        private int[] qp;        // inverse of pq - qp[pq[i]] = pq[qp[i]] = i
        private Key[] keys;      // keys[i] = priority of i

        /**
         * Initializes an empty indexed priority queue with indices between 0
         * and maxN - 1.
         * @param  maxN the keys on this priority queue are index from 0
         *         maxN - 1
         * @throws IllegalArgumentException if maxN &lt; 0
         */
        public IndexMinPQ(int maxN)
        {
            if (maxN < 0)
                throw new Exception();
            this.maxN = maxN;
            keys = new Key[maxN + 1];    // make this of length maxN??
            pq = new int[maxN + 1];
            qp = new int[maxN + 1];                   // make this of length maxN??
            for (int i = 0; i <= maxN; i++)
                qp[i] = -1;
        }

        public bool IsEmpty() { return N == 0; }

        /**
         * Is i an index on this priority queue?
         *
         * @param  i an index
         * @return true if i is an index on this priority queue;
         *         false otherwise
         * @throws IndexOutOfBoundsException unless 0 &le; i &lt; maxN
         */
        public bool Contains(int i)
        {
            if (i < 0 || i >= maxN)
                throw new IndexOutOfRangeException();
            return qp[i] != -1;
        }

        /**
         * Returns the number of keys on this priority queue.
         *
         * @return the number of keys on this priority queue
         */
        public int Size() { return N; }

        /**
         * Associates key with index i.
         *
         * @param  i an index
         * @param  key the key to associate with index i
         * @throws IndexOutOfBoundsException unless 0 &le; i &lt; maxN
         * @throws IllegalArgumentException if there already is an item associated
         *         with index i
         */
        public void Insert(int i, Key key)
        {
            if (i < 0 || i >= maxN) throw new IndexOutOfRangeException();
            if (Contains(i)) throw new Exception("index is already in the priority queue");
            N++;
            qp[i] = N;
            pq[N] = i;
            keys[i] = key;
            swim(N);
        }

        /**
         * Returns an index associated with a minimum key.
         *
         * @return an index associated with a minimum key
         * @throws NoSuchElementException if this priority queue is empty
         */
        public int MinIndex()
        {
            if (N == 0) throw new KeyNotFoundException("Priority queue underflow");
            return pq[1];
        }

        /**
         * Returns a minimum key.
         *
         * @return a minimum key
         * @throws NoSuchElementException if this priority queue is empty
         */
        public Key MinKey()
        {
            if (N == 0) throw new KeyNotFoundException("Priority queue underflow");
            return keys[pq[1]];
        }

        /**
         * Removes a minimum key and returns its associated index.
         * @return an index associated with a minimum key
         * @throws NoSuchElementException if this priority queue is empty
         */
        public int DelMin()
        {
            if (N == 0) throw new KeyNotFoundException("Priority queue underflow");
            int min = pq[1];
            exch(1, N--);
            sink(1);

            qp[min] = -1;        // delete
            keys[min] = default(Key);    // to help with garbage collection
            pq[N + 1] = -1;        // not needed
            return min;
        }

        /**
         * Returns the key associated with index i.
         *
         * @param  i the index of the key to return
         * @return the key associated with index i
         * @throws IndexOutOfBoundsException unless 0 &le; i &lt; maxN
         * @throws NoSuchElementException no key is associated with index i
         */
        public Key KeyOf(int i)
        {
            if (i < 0 || i >= maxN) throw new IndexOutOfRangeException();
            if (!Contains(i)) throw new KeyNotFoundException("index is not in the priority queue");
            else return keys[i];
        }

        /**
         * Change the key associated with index i to the specified value.
         *
         * @param  i the index of the key to change
         * @param  key change the key assocated with index i to this key
         * @throws IndexOutOfBoundsException unless 0 &le; i &lt; maxN
         * @throws NoSuchElementException no key is associated with index i
         */
        public void ChangeKey(int i, Key key)
        {
            if (i < 0 || i >= maxN) throw new IndexOutOfRangeException();
            if (!Contains(i)) throw new KeyNotFoundException("index is not in the priority queue");
            keys[i] = key;
            swim(qp[i]);
            sink(qp[i]);
        }

        /**
         * Change the key associated with index i to the specified value.
         *
         * @param  i the index of the key to change
         * @param  key change the key assocated with index i to this key
         * @throws IndexOutOfBoundsException unless 0 &le; i &lt; maxN
         * @deprecated Replaced by {@link #changeKey(int, Key)}.
         */
        public void Change(int i, Key key) { ChangeKey(i, key); }

        /// <summary>
        ///  Decrease the key associated with index i to the specified value.
        /// </summary>
        /// <param name="i"></param>
        /// <param name="key"></param>
        public void DecreaseKey(int i, Key key)
        {
            if (i < 0 || i >= maxN)
                throw new IndexOutOfRangeException();
            if (!Contains(i))
                throw new KeyNotFoundException("index is not in the priority queue");
            if (((IComparable)keys[i]).CompareTo(key) <= 0)
                throw new Exception("Calling decreaseKey() with given argument would not strictly decrease the key");
            keys[i] = key;
            swim(qp[i]);
        }

        /**
         * Increase the key associated with index i to the specified value.
         *
         * @param  i the index of the key to increase
         * @param  key increase the key assocated with index i to this key
         * @throws IndexOutOfBoundsException unless 0 &le; i &lt; maxN
         * @throws IllegalArgumentException if key &le; key associated with index i
         * @throws NoSuchElementException no key is associated with index i
         */
        public void IncreaseKey(int i, Key key)
        {
            if (i < 0 || i >= maxN) throw new IndexOutOfRangeException();
            if (!Contains(i)) throw new KeyNotFoundException("index is not in the priority queue");
            if (((IComparable)keys[i]).CompareTo(key) >= 0)
                throw new Exception("Calling increaseKey() with given argument would not strictly increase the key");
            keys[i] = key;
            sink(qp[i]);
        }

        /**
         * Remove the key associated with index i.
         *
         * @param  i the index of the key to remove
         * @throws IndexOutOfBoundsException unless 0 &le; i &lt; maxN
         * @throws NoSuchElementException no key is associated with index <t>i
         */
        public void Delete(int i)
        {
            if (i < 0 || i >= maxN) throw new IndexOutOfRangeException();
            if (!Contains(i)) throw new KeyNotFoundException("index is not in the priority queue");
            int index = qp[i];
            exch(index, N--);
            swim(index);
            sink(index);
            keys[i] = default(Key);
            qp[i] = -1;
        }

        /***************************************************************************
         * General helper functions.
         ***************************************************************************/
        private bool greater(int i, int j)
        {
            return ((IComparable)keys[pq[i]]).CompareTo(keys[pq[j]]) > 0;
        }

        private void exch(int i, int j)
        {
            int swap = pq[i];
            pq[i] = pq[j];
            pq[j] = swap;
            qp[pq[i]] = i;
            qp[pq[j]] = j;
        }

        /***************************************************************************
         * Heap helper functions.
         ***************************************************************************/
        private void swim(int k)
        {
            while (k > 1 && greater(k / 2, k))
            {
                exch(k, k / 2);
                k = k / 2;
            }
        }

        private void sink(int k)
        {
            while (2 * k <= N)
            {
                int j = 2 * k;
                if (j < N && greater(j, j + 1)) j++;
                if (!greater(k, j)) break;
                exch(k, j);
                k = j;
            }
        }

        public int CompareTo(Key other)
        {
            throw new NotImplementedException();
        }
    }
}
