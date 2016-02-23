using Algorithms.Part1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Part4
{
    public class EdgeWeightedDigraph
    {
        /// <summary>
        /// number of vertices in this digraph
        /// </summary>
        private readonly int _v;
        /// <summary>
        /// number of edges in this digraph
        /// </summary>
        private int _e;
        public Bag<DirectedEdge>[] Adj { get; private set; }

        public EdgeWeightedDigraph(int v)
        {
            if (v < 0) throw new Exception("Number of vertices in a Digraph must be nonnegative");
            this._v = v;
            this._e = 0;
            this.Adj = new Bag<DirectedEdge>[v];
            for (int i = 0; i < v; i++)
                this.Adj[i] = new Bag<DirectedEdge>();
        }

        public EdgeWeightedDigraph(EdgeWeightedDigraph g)
            : this(g.V())
        {
            this._e = g.E();
            for (int v = 0; v < g.V(); v++)
            {
                // reverse so that adjacency list is in same order as original
                Part1.Stack<DirectedEdge> reverse = new Part1.Stack<DirectedEdge>();
                foreach (DirectedEdge e in g.Adj[v])
                    reverse.Push(e);
                foreach (DirectedEdge e in reverse)
                    this.Adj[v].Add(e);
            }
        }

        public int V() { return this._v; }

        public int E() { return this._e; }

        private void validateVertex(int v)
        {
            if (v < 0 || v >= this._v)
                throw new IndexOutOfRangeException("vertex " + v + " is not between 0 and " + (this._v - 1));
        }

        public void AddEdge(DirectedEdge e)
        {
            int v = e.From();
            int w = e.To();
            this.validateVertex(v);
            this.validateVertex(w);
            this.Adj[v].Add(e);
            this._e++;
        }

        public Bag<DirectedEdge> Edges()
        {
            Bag<DirectedEdge> list = new Bag<DirectedEdge>();
            for (int v = 0; v < this._v; v++)
                foreach (DirectedEdge e in this.Adj[v])
                    list.Add(e);
            return list;
        }

        public override string ToString()
        {
            StringBuilder s = new StringBuilder();
            s.Append(this._v + " " + this._e + Environment.NewLine);
            for (int v = 0; v < this._v; v++)
            {
                s.Append(v + ": ");
                foreach (DirectedEdge e in this.Adj[v])
                    s.Append(e + "  ");
                s.Append(Environment.NewLine);
            }
            return s.ToString();
        }
    }
}
