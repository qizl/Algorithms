using Algorithms.Part2;
using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms.Part4
{
    public class DijkstraSP
    {
        private EdgeWeightedDigraph _g;
        private double _s;
        /// <summary>
        /// distTo[v] = distance of shortest s->v path
        /// </summary>
        private DirectedEdge[] _edgeTo;
        private IndexMinPQ<double> _pq;

        /// <summary>
        /// edgeTo[v] = last edge on shortest s->v path
        /// </summary>
        public double[] DistTo { get; private set; }

        public DijkstraSP(EdgeWeightedDigraph g, int s)
        {
            this._g = g;
            this._s = s;
            this.DistTo = new double[g.V()];
            this._edgeTo = new DirectedEdge[g.V()];
            this._pq = new IndexMinPQ<double>(g.V());

            for (int v = 0; v < g.V(); v++)
                this.DistTo[v] = double.PositiveInfinity;
            this.DistTo[s] = 0d;

            this._pq.Insert(s, 0d);
            while (!this._pq.IsEmpty())
            {
                int v = this._pq.DelMin();
                foreach (DirectedEdge e in g.Adj[v])
                    this.relax(e);
            }
        }

        private void relax(DirectedEdge e)
        {
            int v = e.From(), w = e.To();
            if (this.DistTo[w] > this.DistTo[v] + e.Weight())
            {
                this.DistTo[w] = this.DistTo[v] + e.Weight();
                this._edgeTo[w] = e;

                if (this._pq.Contains(w))
                    this._pq.DecreaseKey(w, this.DistTo[w]);
                else
                    this._pq.Insert(w, this.DistTo[w]);
            }
        }

        public bool HasPathTo(int v) { return this.DistTo[v] < double.PositiveInfinity; }

        public IEnumerable<DirectedEdge> PathTo(int v)
        {
            if (!this.HasPathTo(v)) return null;

            Stack<DirectedEdge> path = new Stack<DirectedEdge>();
            for (DirectedEdge e = this._edgeTo[v]; e != null; e = this._edgeTo[e.From()])
                path.Push(e);

            return path;
        }

        public override string ToString()
        {
            StringBuilder s = new StringBuilder();
            s.Append("source vertex: " + this._s + Environment.NewLine);
            for (int v = 0; v < this._g.V(); v++)
                if (this.HasPathTo(v))
                {
                    s.Append(this._s + " to " + v + " (" + this.DistTo[v].ToString("F2") + ")   ");
                    foreach (DirectedEdge e in this.PathTo(v))
                        s.Append(e + "  ");
                    s.Append(Environment.NewLine);
                }
            return s.ToString();
        }
    }

    public class DijkstraAllPairsSP
    {
        private DijkstraSP[] _all;

        public DijkstraAllPairsSP(EdgeWeightedDigraph g)
        {
            this._all = new DijkstraSP[g.V()];
            for (int v = 0; v < g.V(); v++)
                this._all[v] = new DijkstraSP(g, v);
        }

        public IEnumerable<DirectedEdge> Path(int s, int t) { return this._all[s].PathTo(t); }

        public double Dist(int s, int t) { return this._all[s].DistTo[t]; }

        public bool HasPath(int s, int t) { return this.Dist(s, t) < double.PositiveInfinity; }
    }
}
