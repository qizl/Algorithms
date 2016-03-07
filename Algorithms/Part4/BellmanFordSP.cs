using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms.Part4
{
    public class BellmanFordSP
    {
        private EdgeWeightedDigraph _g;
        private double _s;
        private double[] _distTo;
        private DirectedEdge[] _edgeTo;
        private bool[] _onQueue;
        private Queue<int> _queue;
        private int _cost;
        private IEnumerable<DirectedEdge> _cycle;

        public BellmanFordSP(EdgeWeightedDigraph g, int s)
        {
            this._g = g;
            this._s = s;
            this._distTo = new double[g.V()];
            this._edgeTo = new DirectedEdge[g.V()];
            this._onQueue = new bool[g.V()];
            this._queue = new Queue<int>();

            for (int v = 0; v < g.V(); v++)
                this._distTo[v] = double.PositiveInfinity;
            this._distTo[s] = 0d;

            this._queue.Enqueue(s);

            this._onQueue[s] = true;

            while (this._queue.Count != 0 && !this.hasNegativeCycle())
            {
                int v = this._queue.Dequeue();
                this._onQueue[v] = false;
                this.relax(g, v);
            }
        }

        private void relax(EdgeWeightedDigraph g, int v)
        {
            foreach (DirectedEdge e in g.Adj[v])
            {
                int w = e.To();
                if (this._distTo[w] > this._distTo[v] + e.Weight())
                {
                    this._distTo[w] = this._distTo[v] + e.Weight();
                    this._edgeTo[w] = e;
                    if (!this._onQueue[w])
                    {
                        this._queue.Enqueue(w);
                        this._onQueue[w] = true;
                    }
                }

                if (this._cost++ % g.V() == 0)
                {
                    this.findNegativeCycle();
                    if (this.hasNegativeCycle())
                        return;
                }
            }
        }

        private bool hasNegativeCycle() { return this._cycle != null; }

        public IEnumerable<DirectedEdge> NegativeCycle() { return this._cycle; }

        private void findNegativeCycle()
        {
            int V = this._edgeTo.Length;
            EdgeWeightedDigraph spt = new EdgeWeightedDigraph(V);
            for (int v = 0; v < V; v++)
                if (this._edgeTo[v] != null)
                    spt.AddEdge(this._edgeTo[v]);

            EdgeWeightedDirectedCycle finder = new EdgeWeightedDirectedCycle(spt);
            this._cycle = finder.Cycle;
        }

        public double DistTo(int v)
        {
            if (this.hasNegativeCycle())
                throw new NotSupportedException("Negative cost cycle exists");
            return this._distTo[v];
        }

        public bool HasPathTo(int v) { return this._distTo[v] < double.PositiveInfinity; }

        public IEnumerable<DirectedEdge> PathTo(int v)
        {
            if (this.hasNegativeCycle())
                throw new NotSupportedException("Negative cost cycle exists");

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
                    s.Append(this._s + " to " + v + " (" + this._distTo[v].ToString("F2") + ")   ");
                    foreach (DirectedEdge e in this.PathTo(v))
                        s.Append(e + "  ");
                    s.Append(Environment.NewLine);
                }
            return s.ToString();
        }
    }
}
