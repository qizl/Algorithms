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
    }
}
