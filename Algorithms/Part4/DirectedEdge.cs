using System;

namespace Algorithms.Part4
{
    public class DirectedEdge
    {
        private readonly int _v;
        private readonly int _w;
        private readonly double _weight;

        public DirectedEdge(int v, int w, double weight)
        {
            if (v < 0 || w < 0) throw new IndexOutOfRangeException("Vertex names must be nonnegative integers");
            if (double.IsNaN(weight)) throw new Exception("Weight is NaN");

            this._v = v;
            this._w = w;
            this._weight = weight;
        }

        public int From() { return this._v; }

        public int To() { return this._w; }

        public double Weight() { return this._weight; }

        public override string ToString() { return this._v + "->" + this._w + " " + this._weight.ToString("F2"); }
    }
}
