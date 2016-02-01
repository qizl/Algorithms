using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Algorithms.Part1;
using System.Text;
using System.Diagnostics;

namespace Algorithms.Tests
{
    [TestClass]
    public class Part1Test
    {
        [TestMethod]
        public void TestNode()
        {
            Node first = new Node();
            Node second = new Node();
            Node third = new Node();

            first.Item = "to";
            second.Item = "be";
            third.Item = "or";

            first.Next = second;
            second.Next = third;

            StringBuilder sb = new StringBuilder();
            for (Node x = first; x != null; x = x.Next)
                sb.Append(x.Item + " ");
        }

        [TestMethod]
        public void TestStack()
        {
            Stack<string> s = new Stack<string>();
            s.Push("to");
            s.Push("be");
            s.Push("or");

            StringBuilder sb = new StringBuilder();
            while (!s.IsEmpty)
                sb.Append(s.Pop() + " "); // {or be to }
        }

        [TestMethod]
        public void TestQueue()
        {
            Queue<string> s = new Queue<string>();
            s.Enqueue("to");
            s.Enqueue("be");
            s.Enqueue("or");

            StringBuilder sb = new StringBuilder();
            while (!s.IsEmpty)
                sb.Append(s.Dequeue() + " "); // {to be or }
        }

        [TestMethod]
        public void TestTArray()
        {
            System.Collections.Generic.List<string>[] strs = new System.Collections.Generic.List<string>[10];
            object[] oa = (object[])strs;
            System.Collections.Generic.List<int> ints = new System.Collections.Generic.List<int>();
            ints.Add(123);
            oa[0] = ints;
            string s = strs[0][0];
        }

        [TestMethod]
        public void TestQuickFind()
        {
            Random rand = new Random();
            int n = rand.Next(1000000, 2000000);
            Debug.WriteLine("总点数：" + n);
            QuickFind uf = new QuickFind(n);

            int pairCounts = n;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < pairCounts; i++)
            {
                int p = rand.Next(0, n - 1);
                int q = rand.Next(0, n - 1);
                if (uf.IsConnected(p, q))
                    continue;
                uf.Union(p, q);
                sb.Append(p + "-" + q + ",");
            }
            Debug.WriteLine("连接对：" + sb.ToString());

            sb.Clear();
            for (int i = 0; i < uf.ID.Length; i++)
                sb.Append(uf.ID[i].ToString().PadLeft(n.ToString().Length, '0') + "," + (i % 10 == 9 ? "\r" : ""));
            Debug.WriteLine("连接图：");
            Debug.WriteLine(sb.ToString());

            Debug.WriteLine("连接数：" + uf.Count());
        }

        [TestMethod]
        public void TestQuickUnion()
        {
            Random rand = new Random();
            int n = rand.Next(100, 200);
            Debug.WriteLine("总点数：" + n);
            QuickUnion qu = new QuickUnion(n);

            int pairCounts = n;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < pairCounts; i++)
            {
                int p = rand.Next(0, n - 1);
                int q = rand.Next(0, n - 1);
                if (qu.IsConnected(p, q))
                    continue;
                qu.Union(p, q);
                sb.Append(p + "-" + q + ",");
            }
            Debug.WriteLine("连接对：" + sb.ToString());

            sb.Clear();
            for (int i = 0; i < qu.ID.Length; i++)
                sb.Append(qu.ID[i].ToString().PadLeft(n.ToString().Length, '0') + "," + (i % 10 == 9 ? "\r" : ""));
            Debug.WriteLine("连接图：");
            Debug.WriteLine(sb.ToString());

            Debug.WriteLine("连接数：" + qu.Count());
        }

        [TestMethod]
        public void TestWeightedQuickUnion()
        {
            Random rand = new Random();
            int n = rand.Next(100, 200);
            Debug.WriteLine("总点数：" + n);
            WeightedQuickUnion wqu = new WeightedQuickUnion(n);

            int pairCounts = n;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < pairCounts; i++)
            {
                int p = rand.Next(0, n - 1);
                int q = rand.Next(0, n - 1);
                if (wqu.IsConnected(p, q))
                    continue;
                wqu.Union(p, q);
                sb.Append(p + "-" + q + ",");
            }
            Debug.WriteLine("连接对：" + sb.ToString());

            sb.Clear();
            for (int i = 0; i < wqu.ID.Length; i++)
                sb.Append(wqu.ID[i].ToString().PadLeft(n.ToString().Length, '0') + "," + (i % 10 == 9 ? "\r" : ""));
            Debug.WriteLine("连接图：");
            Debug.WriteLine(sb.ToString());

            Debug.WriteLine("连接数：" + wqu.Count());
        }

        [TestMethod]
        public void TestWeightedQuickUnionPathCompression()
        {
            Random rand = new Random();
            int n = rand.Next(100, 200);
            Debug.WriteLine("总点数：" + n);
            WeightedQuickUnionPathCompression wqu = new WeightedQuickUnionPathCompression(n);

            int pairCounts = n;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < pairCounts; i++)
            {
                int p = rand.Next(0, n - 1);
                int q = rand.Next(0, n - 1);
                if (wqu.IsConnected(p, q))
                    continue;
                wqu.Union(p, q);
                sb.Append(p + "-" + q + ",");
            }
            Debug.WriteLine("连接对：" + sb.ToString());

            sb.Clear();
            for (int i = 0; i < wqu.ID.Length; i++)
                sb.Append(wqu.ID[i].ToString().PadLeft(n.ToString().Length, '0') + "," + (i % 10 == 9 ? "\r" : ""));
            Debug.WriteLine("连接图：");
            Debug.WriteLine(sb.ToString());

            Debug.WriteLine("连接数：" + wqu.Count());
        }
    }
}
