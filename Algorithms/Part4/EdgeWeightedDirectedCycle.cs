using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Part4
{
    public class EdgeWeightedDirectedCycle
    {
        /// <summary>
        /// marked[v] = has vertex v been marked?
        /// </summary>
        private bool[] _marked;

        /// <summary>
        /// edgeTo[v] = previous vertex on path to v
        /// </summary>
        private DirectedEdge[] _edgeTo;

        /// <summary>
        /// onStack[v] = is vertex on the stack?
        /// </summary>
        private bool[] _onStack;

        /// <summary>
        /// directed this.Cycle (or null if no such this.Cycle)
        /// </summary>
        public Stack<DirectedEdge> Cycle { get; private set; }

        /// <summary>
        /// Determines whether the edge-weighted digraph G has a directed this.Cycle and, if so, finds such a this.Cycle
        /// </summary>
        /// <param name="g"></param>
        public EdgeWeightedDirectedCycle(EdgeWeightedDigraph g)
        {
            this._marked = new bool[g.V()];
            this._onStack = new bool[g.V()];
            this._edgeTo = new DirectedEdge[g.V()];

            for (int v = 0; v < g.V(); v++)
                if (!this._marked[v])
                    this.dfs(g, v);
        }

        /// <summary>
        /// check that algorithm computes either the topological order or finds a directed cycle
        /// </summary>
        /// <param name="g"></param>
        /// <param name="v"></param>
        private void dfs(EdgeWeightedDigraph g, int v)
        {
            this._onStack[v] = true;
            this._marked[v] = true;
            foreach (DirectedEdge e in g.Adj[v])
            {
                int w = e.To();

                if (this.Cycle != null)
                {
                    // short circuit if directed cycle found
                    return;
                }
                else if (!this._marked[w])
                {
                    //found new vertex, so recur
                    this._edgeTo[w] = e;
                    this.dfs(g, w);
                }
                else if (this._onStack[w])
                {
                    // trace back directed cycle
                    this.Cycle = new Stack<DirectedEdge>();
                    while (e.From() != w)
                    {
                        this.Cycle.Push(e);
                        //e = this._edgeTo[e.From()];
                    }
                    this.Cycle.Push(e);
                    return;
                }
            }

            this._onStack[v] = false;
        }

        /// <summary>
        /// Does the edge-weighted digraph have a directed this.Cycle?
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
                DirectedEdge first = null, last = null;
                foreach (DirectedEdge e in this.Cycle)
                {
                    if (first == null) first = e;
                    if (last != null)
                        if (last.To() != e.From())
                            return false;
                    last = e;
                }
                if (first.To() != last.From())
                    return false;
            }

            return true;
        }
    }
}
