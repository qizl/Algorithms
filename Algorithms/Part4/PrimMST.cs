using Algorithms.Part2;
using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms.Part4
{
    public class PrimMST
    {
        /// <summary>
        /// edgeTo[v] = shortest edge from tree vertex to non-tree vertex
        /// </summary>
        private Edge[] _edgeTo;
        /// <summary>
        /// distTo[v] = weight of shortest such edge
        /// </summary>
        private double[] _distTo;
        /// <summary>
        /// marked[v] = true if v on tree, false otherwise
        /// </summary>
        private bool[] _marked;
        private IndexMinPQ<double> _pq;

        public PrimMST(EdgeWeightedGraph g)
        {
            this._edgeTo = new Edge[g.V()];
            this._distTo = new double[g.V()];
            this._marked = new bool[g.V()];
            this._pq = new IndexMinPQ<double>(g.V());
            for (int v = 0; v < g.V(); v++)
                this._distTo[v] = double.PositiveInfinity;

            for (int v = 0; v < g.V(); v++)
                if (!this._marked[v])
                    this.prim(g, v);
        }

        private void prim(EdgeWeightedGraph g, int s)
        {
            this._distTo[s] = 0.0d;
            this._pq.Insert(s, this._distTo[s]);
            while (!this._pq.IsEmpty())
            {
                int v = this._pq.DelMin();
                this.scan(g, v);
            }
        }

        private void scan(EdgeWeightedGraph g, int v)
        {
            this._marked[v] = true;
            foreach (Edge e in g.Adj[v])
            {
                int w = e.Other(v);
                if (this._marked[w])
                    continue;

                if (e.Weight() < this._distTo[w])
                {
                    this._distTo[w] = e.Weight();
                    this._edgeTo[w] = e;
                    if (this._pq.Contains(w))
                        this._pq.DecreaseKey(w, this._distTo[w]);
                    else
                        this._pq.Insert(w, this._distTo[w]);
                }
            }
        }

        public IEnumerable<Edge> Edges()
        {
            Queue<Edge> mst = new Queue<Edge>();
            for (int v = 0; v < this._edgeTo.Length; v++)
            {
                Edge e = this._edgeTo[v];
                if (e != null)
                    mst.Enqueue(e);
            }
            return mst;
        }

        public double Weight()
        {
            double weight = 0.0d;
            foreach (Edge e in this.Edges())
                weight += e.Weight();
            return weight;
        }

        public override string ToString()
        {
            StringBuilder s = new StringBuilder();
            s.Append(this.Weight().ToString("F2") + Environment.NewLine);
            foreach (Edge e in this.Edges())
                s.Append(e + "  " + Environment.NewLine);
            return s.ToString();
        }
    }
}
