using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Part4
{
    public class DepthFirstPaths
    {
        /// <summary>
        /// marked[v] = is there an s-v path?
        /// </summary>
        public bool[] Marked { get; private set; }
        /// <summary>
        /// edgeTo[v] = last edge on s-v path
        /// </summary>
        private int[] _edgeTo;
        /// <summary>
        /// source vertex
        /// </summary>
        private readonly int _s;

        /// <summary>
        /// Computes a path between s and every other vertex in graph G.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="s"></param>
        public DepthFirstPaths(Graph g, int s)
        {
            this.Marked = new bool[g.V];
            this._edgeTo = new int[g.V];
            this._s = s;

            this.dfs(g, s);
        }

        private void dfs(Graph g, int v)
        {
            this.Marked[v] = true;

            foreach (int w in g.Adj[v])
                if (!this.Marked[w])
                {
                    this._edgeTo[w] = v;
                    this.dfs(g, w);
                }
        }

        public bool HasPathTo(int v) { return this.Marked[v]; }

        public IEnumerable<int> PathTo(int v)
        {
            if (!this.HasPathTo(v))
                return null;

            Stack<int> path = new Stack<int>();
            for (int x = v; x != this._s; x = this._edgeTo[x])
                path.Push(x);
            path.Push(this._s);

            return path;
        }
    }
}
