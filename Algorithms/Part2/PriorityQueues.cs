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
}
