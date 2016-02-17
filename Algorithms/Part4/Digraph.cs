using Algorithms.Part1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Part4
{
    public class Digraph
    {
        public readonly int V;
        public int E { get; private set; }

        public Bag<int>[] Adj { get; private set; }

        public Digraph(int v)
        {
            if (v < 0)
                throw new Exception("Number of vertices must be nonnegative");

            this.V = v;
            this.E = 0;
            this.Adj = new Bag<int>[v];
            for (int i = 0; i < v; i++)
                this.Adj[i] = new Bag<int>();
        }

        public Digraph(Digraph g)
            : this(g.V)
        {
            this.E = g.E;
            for (int v = 0; v < g.V; v++)
            {
                Part1.Stack<int> reverse = new Part1.Stack<int>();
                foreach (int w in g.Adj[v])
                    reverse.Push(w);
                foreach (int w in reverse)
                    this.Adj[v].Add(w);
            }
        }

        private void validateVertex(int v)
        {
            if (v < 0 || v >= V)
                throw new IndexOutOfRangeException("vertex " + v + " is not between 0 and " + (V - 1));
        }

        public void AddEdge(int v, int w)
        {
            this.validateVertex(v);
            this.validateVertex(w);
            this.E++;
            this.Adj[v].Add(w);
        }

        public int OutDegree(int v)
        {
            this.validateVertex(v);
            return this.Adj[v].Size;
        }

        public Digraph Reverse()
        {
            Digraph r = new Digraph(V);
            for (int v = 0; v < V; v++)
                foreach (int w in this.Adj[v])
                    r.AddEdge(w, v);
            return r;
        }

        public override string ToString()
        {
            StringBuilder s = new StringBuilder();
            s.Append(V + " vertices, " + E + " edges " + Environment.NewLine);
            for (int v = 0; v < this.V; v++)
            {
                s.Append(v + ": ");
                foreach (int w in Adj[v])
                    s.Append(w + " ");
                s.Append(Environment.NewLine);
            }
            return s.ToString();
        }
    }
}
