using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Part4
{
    public class CC
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
        /// Computes the connected components of the undirected graph G.
        /// </summary>
        /// <param name="G"></param>
        public CC(Graph G)
        {
            this.Marked = new bool[G.V];
            this.ID = new int[G.V];
            for (int v = 0; v < G.V; v++)
                if (!this.Marked[v])
                {
                    this.dfs(G, v);
                    this.Count++;
                }
        }

        private void dfs(Graph G, int v)
        {
            this.Marked[v] = true;
            this.ID[v] = this.Count;
            foreach (int w in G.Adj[v])
                if (!this.Marked[w])
                    this.dfs(G, w);
        }

        public bool Connected(int v, int w)
        {
            return this.ID[v] == this.ID[w];
        }
    }
}
