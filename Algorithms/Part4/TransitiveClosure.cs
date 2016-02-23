using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Part4
{
    public class TransitiveClosure
    {
        private DirectedDFS[] _all;

        public TransitiveClosure(Digraph g)
        {
            this._all = new DirectedDFS[g.V];
            for (int v = 0; v < g.V; v++)
                this._all[v] = new DirectedDFS(g, v);
        }

        public bool Reachable(int v, int w) { return this._all[v].Marked[w]; }
    }
}
