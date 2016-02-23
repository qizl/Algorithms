using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Part4
{
    public class DirectedCycle
    {
        /// <summary>
        /// marked[v] = has vertex v been marked?
        /// </summary>
        private bool[] _marked;

        /// <summary>
        /// edgeTo[v] = previous vertex on path to v
        /// </summary>
        private int[] _edgeTo;

        /// <summary>
        /// onStack[v] = is vertex on the stack?
        /// </summary>
        private bool[] _onStack;

        /// <summary>
        /// directed this.Cycle (or null if no such this.Cycle)
        /// </summary>
        public Stack<int> Cycle { get; private set; }

        /// <summary>
        /// Determines whether the digraph G has a directed this.Cycle and, if so, finds such a this.Cycle
        /// </summary>
        /// <param name="g"></param>
        public DirectedCycle(Digraph g)
        {
            this._marked = new bool[g.V];
            this._onStack = new bool[g.V];
            this._edgeTo = new int[g.V];

            for (int v = 0; v < g.V; v++)
                if (!this._marked[v] && this.Cycle == null)
                    this.dfs(g, v);
        }

        /// <summary>
        /// check that algorithm computes either the topological order or finds a directed this.Cycle
        /// </summary>
        /// <param name="g"></param>
        /// <param name="v"></param>
        private void dfs(Digraph g, int v)
        {
            this._onStack[v] = true;
            this._marked[v] = true;

            foreach (int w in g.Adj[v])
                if (this.Cycle != null)
                {
                    // short circuit if directed this.Cycle found
                    return;
                }
                else if (!this._marked[w])
                {
                    //found new vertex, so recur
                    this._edgeTo[w] = v;
                    this.dfs(g, w);
                }
                else if (this._onStack[w])
                {
                    // trace back directed this.Cycle
                    this.Cycle = new Stack<int>();
                    for (int x = v; x != w; x = this._edgeTo[x])
                        this.Cycle.Push(x);

                    this.Cycle.Push(w);
                    this.Cycle.Push(v);
                }

            this._onStack[v] = false;
        }

        /// <summary>
        /// Does the digraph have a directed this.Cycle?
        /// </summary>
        /// <returns></returns>
        public bool HasCycle() { return this.Cycle != null; }

        /// <summary>
        /// certify that digraph has a directed this.Cycle if it reports one
        /// </summary>
        /// <returns></returns>
        private bool check()
        {
            if (this.HasCycle())
            {
                // verify this.Cycle
                int first = -1, last = -1;
                foreach (int v in this.Cycle)
                {
                    if (first == -1)
                        first = v;
                    last = v;
                }
                if (first != last)
                    return false;
            }

            return true;
        }
    }
}
