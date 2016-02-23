using Algorithms.Part2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Part4
{
    public class LazyPrimMST
    {
        public double Weight { get; private set; }

        public Queue<Edge> MST { get; private set; }

        private bool[] _marked;
        private MinPQ<Edge> _pq;

        public LazyPrimMST(EdgeWeightedGraph g)
        {
            this.MST = new Queue<Edge>();
            this._pq = new MinPQ<Edge>();
            this._marked = new bool[g.V()];
            for (int v = 0; v < g.V(); v++)
                if (!this._marked[v])
                    this.prim(g, v);
        }

        /// <summary>
        /// run Prim's algorithm
        /// </summary>
        /// <param name="g"></param>
        /// <param name="s"></param>
        private void prim(EdgeWeightedGraph g, int s)
        {
            this.scan(g, s);

            while (!this._pq.IsEmpty())
            {
                Edge e = this._pq.DelMin();
                int v = e.Either(), w = e.Other(v);
                if (this._marked[v] && this._marked[w])
                {
                    // lazy, both v and w already scanned
                    continue;
                }

                this.MST.Enqueue(e);
                this.Weight += e.Weight();

                if (!this._marked[v])
                    this.scan(g, v);
                if (!this._marked[w])
                    this.scan(g, w);
            }
        }

        /// <summary>
        /// add all edges e incident to v onto pq if the other endpoint has not yet been scanned
        /// </summary>
        /// <param name="g"></param>
        /// <param name="v"></param>
        private void scan(EdgeWeightedGraph g, int v)
        {
            this._marked[v] = true;
            foreach (Edge e in g.Adj[v])
                if (!this._marked[e.Other(v)])
                    this._pq.Insert(e);
        }

        public IEnumerable<Edge> Edges() { return this.MST; }

        public override string ToString()
        {
            StringBuilder s = new StringBuilder();
            s.Append(this.Weight.ToString("F2") + Environment.NewLine);
            foreach (Edge e in this.Edges())
                s.Append(e + "  " + Environment.NewLine);
            return s.ToString();
        }
    }
}
