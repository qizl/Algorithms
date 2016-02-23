using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Part4
{
    public class DirectedDFS
    {
        /// <summary>
        /// marked[v] = true if v is reachable from source (or sources)
        /// </summary>
        public bool[] Marked { get; private set; }
        /// <summary>
        /// number of vertices reachable from s
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Computes the vertices in digraph G that are reachable from the source vertex s.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="s"></param>
        public DirectedDFS(Digraph g, int s)
        {
            this.Marked = new bool[g.V];
            this.dfs(g, s);
        }

        /// <summary>
        /// Computes the vertices in digraph G that are to any of the source vertices sources.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="sources"></param>
        public DirectedDFS(Digraph g, IEnumerable<int> sources)
        {
            this.Marked = new bool[g.V];
            foreach (int v in sources)
                if (!this.Marked[v])
                    this.dfs(g, v);
        }

        private void dfs(Digraph g, int v)
        {
            this.Count++;
            this.Marked[v] = true;

            foreach (int w in g.Adj[v])
                if (!this.Marked[w])
                    this.dfs(g, w);
        }
    }
}
