﻿using System;
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
        /// <param name="g"></param>
        public CC(Graph g)
        {
            this.Marked = new bool[g.V];
            this.ID = new int[g.V];
            for (int v = 0; v < g.V; v++)
                if (!this.Marked[v])
                {
                    this.dfs(g, v);
                    this.Count++;
                }
        }

        private void dfs(Graph g, int v)
        {
            this.Marked[v] = true;
            this.ID[v] = this.Count;
            foreach (int w in g.Adj[v])
                if (!this.Marked[w])
                    this.dfs(g, w);
        }

        public bool Connected(int v, int w)
        {
            return this.ID[v] == this.ID[w];
        }
    }
}
