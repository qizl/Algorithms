using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Part4
{
    public class AcyclicSP
    {
        private EdgeWeightedDigraph _g;
        private double _s;
        /// <summary>
        /// distTo[v] = distance of shortest s->v path
        /// </summary>
        private DirectedEdge[] _edgeTo;

        /// <summary>
        /// edgeTo[v] = last edge on shortest s->v path
        /// </summary>
        public double[] DistTo { get; private set; }

        public AcyclicSP(EdgeWeightedDigraph g, int s)
        {
            this._g = g;
            this._s = s;
            this.DistTo = new double[g.V()];
            this._edgeTo = new DirectedEdge[g.V()];

            for (int v = 0; v < g.V(); v++)
                this.DistTo[v] = double.PositiveInfinity;
            this.DistTo[s] = 0d;

            Topological top = new Topological(g);
            foreach (int v in top.Order)
                foreach (DirectedEdge e in g.Adj[v])
                    this.relax(e);
        }

        private void relax(DirectedEdge e)
        {
            int v = e.From(), w = e.To();
            if (this.DistTo[w] > this.DistTo[v] + e.Weight())
            {
                this.DistTo[w] = this.DistTo[v] + e.Weight();
                this._edgeTo[w] = e;
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
}