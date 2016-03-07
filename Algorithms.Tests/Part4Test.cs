using Algorithms.Part4;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Algorithms.Tests
{
    [TestClass]
    public class Part4Test
    {
        [TestMethod]
        public void TestGraph()
        {
            Random rand = new Random();
            int v = rand.Next(10, 30);

            Graph g = new Graph(v);
            int e0 = rand.Next(10, 30);
            string strs = string.Empty;
            for (int i = 0; i < e0; i++)
            {
                int v1 = rand.Next(0, v);
                int v2 = rand.Next(0, v);
                string str = v1 + " - " + v2 + ",";
                string str1 = v2 + " - " + v1 + ",";

                if (v1 == v2 || strs.Contains(str) || strs.Contains(str1))
                {
                    i--;
                    continue;
                }

                g.AddEdge(v1, v2);
                strs += str;
            }

            Debug.WriteLine("input e: " + strs);
            Debug.WriteLine(g.ToString());

            Graph g1 = new Graph(g);
            Debug.WriteLine("g1: ");
            Debug.WriteLine(g1.ToString());
        }

        [TestMethod]
        public void TestSymbolGraph()
        {
            string str = "JFK,MCO,ORD,DEN,ORD,HOU,FW,PHX,JFK,ATL,ORD,DFW,ORD,PHX,ATL,HOU,DEN,PHX,PHX,LAX,JFK,ORD,DEN,LAS,DFW,HOU,ORD,ATL,LAS,LAX,ATL,MCO,HOU,MCO,LAS,PHX,";
            char delimiter = ',';
            SymbolGraph sg = new SymbolGraph(str, delimiter);

            Debug.WriteLine(str);
            Debug.WriteLine(sg.ToString());
        }

        [TestMethod]
        public void TestDigraph()
        {
            Random rand = new Random();
            int v = rand.Next(10, 30);

            Digraph g = new Digraph(v);
            int e0 = rand.Next(10, 30);
            string strs = string.Empty;
            for (int i = 0; i < e0; i++)
            {
                int v1 = rand.Next(0, v);
                int v2 = rand.Next(0, v);
                string str = v1 + " - " + v2 + ",";
                string str1 = v2 + " - " + v1 + ",";

                if (v1 == v2 || strs.Contains(str) || strs.Contains(str1))
                {
                    i--;
                    continue;
                }

                g.AddEdge(v1, v2);
                strs += str;
            }

            Debug.WriteLine("input e: " + strs);
            Debug.WriteLine(g.ToString());

            Digraph g1 = new Digraph(g);
            Debug.WriteLine("g1: ");
            Debug.WriteLine(g1.ToString());
        }

        [TestMethod]
        public void TestDirectedCycle()
        {
            Digraph g = new Digraph(6);
            g.AddEdge(0, 5);
            g.AddEdge(5, 4);
            g.AddEdge(4, 3);
            g.AddEdge(3, 5);
            g.AddEdge(0, 1);
            g.AddEdge(1, 2);
            g.AddEdge(2, 3);
            Debug.WriteLine(g.ToString());

            DirectedCycle finder = new DirectedCycle(g);
            if (finder.HasCycle())
            {
                Debug.WriteLine("Directed cycle:");
                foreach (var v in finder.Cycle)
                    Debug.Write(" => " + v);
            }
            else
                Debug.WriteLine("No directed cycle!");
        }

        [TestMethod]
        public void TestKosarajuSCCHasCycle()
        {
            Digraph g = new Digraph(13);
            g.AddEdge(0, 5);
            g.AddEdge(0, 1);
            g.AddEdge(2, 0);
            g.AddEdge(2, 3);
            g.AddEdge(3, 2);
            g.AddEdge(3, 5);
            g.AddEdge(4, 2);
            g.AddEdge(4, 3);
            g.AddEdge(5, 4);
            g.AddEdge(6, 0);
            g.AddEdge(6, 4);
            g.AddEdge(6, 9);
            g.AddEdge(7, 6);
            g.AddEdge(7, 8);
            g.AddEdge(8, 7);
            g.AddEdge(8, 9);
            g.AddEdge(9, 10);
            g.AddEdge(9, 11);
            g.AddEdge(10, 12);
            g.AddEdge(11, 4);
            g.AddEdge(11, 12);
            g.AddEdge(12, 9);
            Debug.WriteLine(g.ToString());

            DepthFirstOrder dfs = new DepthFirstOrder(g);
            StringBuilder sb = new StringBuilder();
            foreach (var item in dfs.ReversePost)
                sb.Append(item + " ");
            Debug.WriteLine(sb.ToString());

            KosarajuSCC scc = new KosarajuSCC(g);
            sb.Clear();
            for (int i = 0; i < scc.ID.Length; i++)
                sb.Append(i + " - " + scc.ID[i] + Environment.NewLine);
            Debug.WriteLine(sb.ToString());
        }

        [TestMethod]
        public void TestKosarajuSCCHasNotCycle()
        {
            Digraph g = new Digraph(6);
            g.AddEdge(0, 1);
            g.AddEdge(1, 2);
            g.AddEdge(2, 3);
            g.AddEdge(0, 4);
            g.AddEdge(4, 5);
            Debug.WriteLine(g.ToString());

            DepthFirstOrder dfs = new DepthFirstOrder(g);
            StringBuilder sb = new StringBuilder();
            foreach (var item in dfs.ReversePost)
                sb.Append(item + " ");
            Debug.WriteLine(sb.ToString());

            KosarajuSCC scc = new KosarajuSCC(g);
            sb.Clear();
            for (int i = 0; i < scc.ID.Length; i++)
                sb.Append(i + " - " + scc.ID[i] + Environment.NewLine);
            Debug.WriteLine(sb.ToString());
        }

        [TestMethod]
        public void TestEdge()
        {
            Edge e = new Edge(12, 34, 5.67);
            Debug.WriteLine(e);
        }

        [TestMethod]
        public void TestEdgeWeightedGraph()
        {
            EdgeWeightedGraph g = new EdgeWeightedGraph(8);
            g.AddEdge(new Edge(0, 2, 0.26d));
            g.AddEdge(new Edge(0, 4, 0.38d));
            g.AddEdge(new Edge(0, 7, 0.16d));
            g.AddEdge(new Edge(1, 3, 0.29d));
            g.AddEdge(new Edge(1, 2, 0.36d));
            g.AddEdge(new Edge(1, 5, 0.32d));
            g.AddEdge(new Edge(1, 7, 0.19d));
            g.AddEdge(new Edge(2, 3, 0.17d));
            g.AddEdge(new Edge(2, 7, 0.34d));
            g.AddEdge(new Edge(3, 6, 0.52d));
            g.AddEdge(new Edge(4, 5, 0.35d));
            g.AddEdge(new Edge(4, 7, 0.37d));
            g.AddEdge(new Edge(5, 7, 0.28d));
            g.AddEdge(new Edge(6, 0, 0.58d));
            g.AddEdge(new Edge(6, 2, 0.40d));
            g.AddEdge(new Edge(6, 4, 0.93d));
            Debug.WriteLine(g);
        }

        [TestMethod]
        public void TestLazyPrimMST()
        {
            EdgeWeightedGraph g = new EdgeWeightedGraph(8);
            g.AddEdge(new Edge(0, 2, 0.26d));
            g.AddEdge(new Edge(0, 4, 0.38d));
            g.AddEdge(new Edge(0, 7, 0.16d));
            g.AddEdge(new Edge(1, 3, 0.29d));
            g.AddEdge(new Edge(1, 2, 0.36d));
            g.AddEdge(new Edge(1, 5, 0.32d));
            g.AddEdge(new Edge(1, 7, 0.19d));
            g.AddEdge(new Edge(2, 3, 0.17d));
            g.AddEdge(new Edge(2, 7, 0.34d));
            g.AddEdge(new Edge(3, 6, 0.52d));
            g.AddEdge(new Edge(4, 5, 0.35d));
            g.AddEdge(new Edge(4, 7, 0.37d));
            g.AddEdge(new Edge(5, 7, 0.28d));
            g.AddEdge(new Edge(6, 0, 0.58d));
            g.AddEdge(new Edge(6, 2, 0.40d));
            g.AddEdge(new Edge(6, 4, 0.93d));
            Debug.WriteLine(g);

            LazyPrimMST mst = new LazyPrimMST(g);
            Debug.WriteLine(mst);
        }

        [TestMethod]
        public void TestPrimMST()
        {
            EdgeWeightedGraph g = new EdgeWeightedGraph(8);
            g.AddEdge(new Edge(0, 2, 0.26d));
            g.AddEdge(new Edge(0, 4, 0.38d));
            g.AddEdge(new Edge(0, 7, 0.16d));
            g.AddEdge(new Edge(1, 3, 0.29d));
            g.AddEdge(new Edge(1, 2, 0.36d));
            g.AddEdge(new Edge(1, 5, 0.32d));
            g.AddEdge(new Edge(1, 7, 0.19d));
            g.AddEdge(new Edge(2, 3, 0.17d));
            g.AddEdge(new Edge(2, 7, 0.34d));
            g.AddEdge(new Edge(3, 6, 0.52d));
            g.AddEdge(new Edge(4, 5, 0.35d));
            g.AddEdge(new Edge(4, 7, 0.37d));
            g.AddEdge(new Edge(5, 7, 0.28d));
            g.AddEdge(new Edge(6, 0, 0.58d));
            g.AddEdge(new Edge(6, 2, 0.40d));
            g.AddEdge(new Edge(6, 4, 0.93d));
            Debug.WriteLine(g);

            PrimMST mst = new PrimMST(g);
            Debug.WriteLine(mst);
        }

        [TestMethod]
        public void TestKruskalMST()
        {
            EdgeWeightedGraph g = new EdgeWeightedGraph(8);
            g.AddEdge(new Edge(0, 2, 0.26d));
            g.AddEdge(new Edge(0, 4, 0.38d));
            g.AddEdge(new Edge(0, 7, 0.16d));
            g.AddEdge(new Edge(1, 3, 0.29d));
            g.AddEdge(new Edge(1, 2, 0.36d));
            g.AddEdge(new Edge(1, 5, 0.32d));
            g.AddEdge(new Edge(1, 7, 0.19d));
            g.AddEdge(new Edge(2, 3, 0.17d));
            g.AddEdge(new Edge(2, 7, 0.34d));
            g.AddEdge(new Edge(3, 6, 0.52d));
            g.AddEdge(new Edge(4, 5, 0.35d));
            g.AddEdge(new Edge(4, 7, 0.37d));
            g.AddEdge(new Edge(5, 7, 0.28d));
            g.AddEdge(new Edge(6, 0, 0.58d));
            g.AddEdge(new Edge(6, 2, 0.40d));
            g.AddEdge(new Edge(6, 4, 0.93d));
            Debug.WriteLine(g);

            KruskalMST mst = new KruskalMST(g);
            Debug.WriteLine(mst);
        }

        [TestMethod]
        public void TestDirectedEdge()
        {
            DirectedEdge e = new DirectedEdge(12, 34, 5.67d);
            Debug.WriteLine(e);
        }

        [TestMethod]
        public void TestEdgeWeightedDigraph()
        {
            EdgeWeightedDigraph g = new EdgeWeightedDigraph(9);
            g.AddEdge(new DirectedEdge(0, 2, 0.26d));
            g.AddEdge(new DirectedEdge(0, 4, 0.38d));
            g.AddEdge(new DirectedEdge(2, 7, 0.34d));
            g.AddEdge(new DirectedEdge(3, 6, 0.52d));
            g.AddEdge(new DirectedEdge(4, 5, 0.35d));
            g.AddEdge(new DirectedEdge(5, 1, 0.32d));
            g.AddEdge(new DirectedEdge(7, 3, 0.39d));
            Debug.WriteLine(g);
        }

        [TestMethod]
        public void TestDijkstraSP()
        {
            EdgeWeightedDigraph g = new EdgeWeightedDigraph(8);
            g.AddEdge(new DirectedEdge(5, 4, 0.35d));
            g.AddEdge(new DirectedEdge(4, 7, 0.37d));
            g.AddEdge(new DirectedEdge(5, 7, 0.28d));
            g.AddEdge(new DirectedEdge(5, 1, 0.32d));
            g.AddEdge(new DirectedEdge(4, 0, 0.38d));
            g.AddEdge(new DirectedEdge(0, 2, 0.26d));
            g.AddEdge(new DirectedEdge(3, 7, 0.39d));
            g.AddEdge(new DirectedEdge(1, 3, 0.29d));
            g.AddEdge(new DirectedEdge(7, 2, 0.34d));
            g.AddEdge(new DirectedEdge(6, 2, 0.40d));
            g.AddEdge(new DirectedEdge(3, 6, 0.52d));
            g.AddEdge(new DirectedEdge(6, 0, 0.58d));
            g.AddEdge(new DirectedEdge(6, 4, 0.93d));
            Debug.WriteLine(g);

            //DijkstraSP mst = new DijkstraSP(g, 5);
            //Debug.WriteLine(mst);
            //AcyclicSP mst1 = new AcyclicSP(g, 5);
            //Debug.WriteLine(mst1);
            //AcyclicLP mst2 = new AcyclicLP(g, 5);
            //Debug.WriteLine(mst2);
            BellmanFordSP mst3 = new BellmanFordSP(g, 5);
            Debug.WriteLine(mst3);
        }
    }
}