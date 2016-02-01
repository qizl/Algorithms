using Algorithms.Part2;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Algorithms.Tests
{
    [TestClass]
    public class Part2Test
    {
        [TestMethod]
        public void TestElementarySelection()
        {
            string[] a = new string[100];

            Random rand = new Random();
            for (int i = 0; i < 100; i++)
                a[i] = rand.Next(0, 1000).ToString();

            Sort.BubbleSort(a);
            //Sort.Selection(a);
            //Sort.Insertion(a);
            //Sort.ImprovedInsertion(a);
            //Sort.ShellSort(a);
            Assert.IsTrue(Sort.IsSorted(a));

            Debug.WriteLine(Sort.Show(a));
        }

        [TestMethod]
        public void TestShuffle()
        {
            string[] a = new string[100];

            for (int i = 0; i < 100; i++)
                a[i] = i.ToString();

            Sort.Shuffle(a);

            Assert.IsFalse(Sort.IsSorted(a));
        }

        [TestMethod]
        public void TestQuickSort()
        {
            string[] a = new string[100];

            Random rand = new Random();
            for (int i = 0; i < 100; i++)
                a[i] = rand.Next(0, 1000).ToString();

            Sort.Quicksort(a);
            Assert.IsTrue(Sort.IsSorted(a));

            Debug.WriteLine(Sort.Show(a));
        }

        [TestMethod]
        public void TestMaxPQ()
        {
            int amounts = 10;

            MaxPQ pq = new MaxPQ(amounts);

            Random rand = new Random();
            for (int i = 0; i < amounts; i++)
                pq.Insert(rand.Next(0, 1000).ToString());

            Debug.WriteLine(Sort.Show(pq.PQs));
            pq.DelMax();
            Debug.WriteLine(Sort.Show(pq.PQs));
        }

        [TestMethod]
        public void TestHeapSort()
        {
            string[] a = new string[100];

            Random rand = new Random();
            for (int i = 0; i < 100; i++)
                a[i] = rand.Next(0, 1000).ToString();

            Sort.HeapSort(a);
            Assert.IsTrue(Sort.IsSorted(a));

            Debug.WriteLine(Sort.Show(a));
        }
    }
}
