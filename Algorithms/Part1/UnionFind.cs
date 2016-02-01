using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Part1
{
    public class QuickFind
    {
        public int[] ID { get; private set; }
        private int _count;

        public QuickFind(int n)
        {
            this._count = n;
            this.ID = new int[n];
            for (int i = 0; i < n; i++)
                this.ID[i] = i;
        }

        public int Count() { return this._count; }

        public bool IsConnected(int p, int q) { return this.Find(p) == this.Find(q); }

        public int Find(int p) { return this.ID[p]; }

        public void Union(int p, int q)
        {
            int pID = this.Find(p);
            int qID = this.Find(q);

            if (qID == pID)
                return;

            for (int i = 0; i < this.ID.Length; i++)
                if (this.ID[i] == pID)
                    this.ID[i] = qID;

            this._count--;
        }
    }

    public class QuickUnion
    {
        public int[] ID { get; private set; }
        private int _count;

        public QuickUnion(int n)
        {
            this._count = n;
            this.ID = new int[n];
            for (int i = 0; i < n; i++)
                this.ID[i] = i;
        }

        public int Count() { return this._count; }

        public bool IsConnected(int p, int q) { return this.Find(p) == this.Find(q); }

        public int Find(int p)
        {
            while (p != this.ID[p])
                p = this.ID[p];

            return p;
        }

        public void Union(int p, int q)
        {
            int pRoot = this.Find(p);
            int qRoot = this.Find(q);

            if (qRoot == pRoot)
                return;

            this.ID[pRoot] = qRoot;

            this._count--;
        }
    }

    public class WeightedQuickUnion
    {
        public int[] ID { get; private set; }
        private int[] _size;
        private int _count;

        public WeightedQuickUnion(int n)
        {
            this._count = n;

            this.ID = new int[n];
            for (int i = 0; i < n; i++)
                this.ID[i] = i;

            this._size = new int[n];
            for (int i = 0; i < n; i++)
                this._size[i] = i;
        }

        public int Count() { return this._count; }

        public bool IsConnected(int p, int q) { return this.Find(p) == this.Find(q); }

        public int Find(int p)
        {
            while (p != this.ID[p])
                p = this.ID[p];

            return p;
        }

        public void Union(int p, int q)
        {
            int i = this.Find(p);
            int j = this.Find(q);

            if (j == i)
                return;

            // 将小树的根节点连接到大树的根节点
            if (this._size[i] < this._size[j])
            {
                this.ID[i] = j;
                this._size[j] += this._size[i];
            }
            else
            {
                this.ID[j] = i;
                this._size[i] += this._size[j];
            }

            this._count--;
        }
    }

    public class WeightedQuickUnionPathCompression
    {
        public int[] ID { get; private set; }
        private int[] _size;
        private int _count;

        public WeightedQuickUnionPathCompression(int n)
        {
            this._count = n;

            this.ID = new int[n];
            for (int i = 0; i < n; i++)
                this.ID[i] = i;

            this._size = new int[n];
            for (int i = 0; i < n; i++)
                this._size[i] = i;
        }

        public int Count() { return this._count; }

        public bool IsConnected(int p, int q) { return this.Find(p) == this.Find(q); }

        public int Find(int p)
        {
            int root = p;

            while (root != this.ID[root])
                root = this.ID[root];

            while (p != root)
            {
                int newP = this.ID[p];
                this.ID[p] = root;
                p = newP;
            }

            return root;
        }

        public void Union(int p, int q)
        {
            int i = this.Find(p);
            int j = this.Find(q);

            if (j == i)
                return;

            // 将小树的根节点连接到大树的根节点
            if (this._size[i] < this._size[j])
            {
                this.ID[i] = j;
                this._size[j] += this._size[i];
            }
            else
            {
                this.ID[j] = i;
                this._size[i] += this._size[j];
            }

            this._count--;
        }
    }
}