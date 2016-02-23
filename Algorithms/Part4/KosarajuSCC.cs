using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Part4
{
    public class KosarajuSCC
    {
        /// <summary>
        /// marked[v] = has vertex v been marked?
        /// </summary>
        public bool[] Marked { get; private set; }
        /// <summary>
        /// id[v] = id of connected component containing v
        /// </summary>
        public int[] ID { get; private set; }
        /// <summary>
        /// number of connected components
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Computes the connected components of the directed graph G.
        /// </summary>
        /// <param name="g"></param>
        public KosarajuSCC(Digraph g)
        {
            this.Marked = new bool[g.V];
            this.ID = new int[g.V];

            DepthFirstOrder order = new DepthFirstOrder(g.Reverse());
            foreach (int v in order.ReversePost)
                if (!this.Marked[v])
                {
                    this.dfs(g, v);
                    this.Count++;
                }
        }

        private void dfs(Digraph g, int v)
        {
            this.Marked[v] = true;
            this.ID[v] = this.Count;
            foreach (int w in g.Adj[v])
                if (!this.Marked[w])
                    this.dfs(g, w);
        }

        public bool StronglyConnected(int v, int w) { return this.ID[v] == this.ID[w]; }
    }
}
