using Algorithms.Part1;
using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithms.Part4
{
    public class EdgeWeightedGraph
    {
        private readonly int _v;
        private int _e;
        public Bag<Edge>[] Adj { get; private set; }

        public EdgeWeightedGraph(int v)
        {
            if (v < 0)
                throw new Exception("Number of vertices must be nonnegative");

            this._v = v;
            this._e = 0;
            this.Adj = new Bag<Edge>[v];
            for (int i = 0; i < v; i++)
                this.Adj[i] = new Bag<Edge>();
        }

        /// <summary>
        /// Initializes a new edge-weighted graph that is a deep copy of G.
        /// </summary>
        /// <param name="G"></param>
        public EdgeWeightedGraph(EdgeWeightedGraph G)
            : this(G.V())
        {
            this._e = G.E();
            for (int v = 0; v < G.V(); v++)
            {
                // reverse so that adjacency list is in same order as original
                Part1.Stack<Edge> reverse = new Part1.Stack<Edge>();
                foreach (Edge e in G.Adj[v])
                    reverse.Push(e);
                foreach (Edge e in reverse)
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

        public void AddEdge(Edge e)
        {
            int v = e.Either();
            int w = e.Other(v);
            validateVertex(v);
            validateVertex(w);
            this.Adj[v].Add(e);
            this.Adj[w].Add(e);
            this._e++;
        }

        /// <summary>
        /// Returns all edges in this edge-weighted graph.
        /// </summary>
        /// <returns></returns>
        public Bag<Edge> Edges()
        {
            Bag<Edge> list = new Bag<Edge>();
            for (int v = 0; v < this._v; v++)
            {
                int selfLoops = 0;
                foreach (Edge e in this.Adj[v])
                    if (e.Other(v) > v)
                        list.Add(e);
                    else if (e.Other(v) == v)
                    {
                        // only add one copy of each self loop (self loops will be consecutive)
                        if (selfLoops % 2 == 0)
                            list.Add(e);
                        selfLoops++;
                    }
            }
            return list;
        }

        public override string ToString()
        {
            StringBuilder s = new StringBuilder();
            s.Append(this._v + " " + this._e + Environment.NewLine);
            for (int v = 0; v < this._v; v++)
            {
                s.Append(v + ": ");
                foreach (Edge e in this.Adj[v])
                    s.Append(e + "  ");
                s.Append(Environment.NewLine);
            }
            return s.ToString();
        }
    }
}
