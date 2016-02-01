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
}
