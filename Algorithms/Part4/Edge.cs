using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Part4
{
    public class Edge : IComparable<Edge>
    {
        private readonly int _v;
        private readonly int _w;
        private readonly double _weight;

        public Edge(int v, int w, double weight)
        {
            if (v < 0 || w < 0)
                throw new IndexOutOfRangeException("Vertex name must be a nonnegative integer");

            this._v = v;
            this._w = w;
            this._weight = weight;
        }

        public double Weight() { return this._weight; }
        public int Either() { return this._v; }
        public int Other(int vertex)
        {
            if (vertex == this._v)
                return this._w;
            else if (vertex == this._w)
                return this._v;
            else
                throw new Exception("Illegal endpoint");
        }

        public int CompareTo(Edge that)
        {
            if (this.Weight() < that.Weight())
                return -1;
            else if (this.Weight() > that.Weight())
                return 1;
            else
                return 0;
        }

        public override string ToString() { return String.Format("{0}-{1} {2:N2}", this._v, this._w, this._weight); }
    }
}
