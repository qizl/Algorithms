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
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < e0; i++)
            {
                int v1 = rand.Next(0, v);
                int v2 = rand.Next(0, v);
                if (v1 == v2)
                {
                    i--;
                    continue;
                }

                g.AddEdge(v1, v2);
                sb.Append(v1 + " - " + v2 + ",");
            }

            Debug.WriteLine("input e: " + sb.ToString());
            Debug.WriteLine(g.ToString());

            Graph g1 = new Graph(g);
            Debug.WriteLine("g1: ");
            Debug.WriteLine(g1.ToString());
        }
    }
}
