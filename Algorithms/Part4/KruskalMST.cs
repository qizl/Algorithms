using Algorithms.Part2;
using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms.Part4
{
    public class KruskalMST
    {
        public double Weight { get; private set; }
        private Queue<Edge> _mst = new Queue<Edge>();

        public KruskalMST(EdgeWeightedGraph g)
        {
            MinPQ<Edge> pq = new MinPQ<Edge>();
            foreach (Edge e in g.Edges())
                pq.Insert(e);

            Part1.QuickUnion uf = new Part1.QuickUnion(g.V());
            while (!pq.IsEmpty() && this._mst.Count < g.V() - 1)
            {
                Edge e = pq.DelMin();
                int v = e.Either();
                int w = e.Other(v);
                if (!uf.IsConnected(v, w))
                {
                    uf.Union(v, w);
                    this._mst.Enqueue(e);
                    this.Weight += e.Weight();
                }
            }
        }

        public IEnumerable<Edge> Edges() { return this._mst; }

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
