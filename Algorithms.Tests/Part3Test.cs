using Algorithms.Part3;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace Algorithms.Tests
{
    [TestClass]
    public class Part3Test
    {
        [TestMethod]
        public void TestSequentialSearchST()
        {
            SequentialSearchST<string, int> st = new SequentialSearchST<string, int>();

            int amounts = 20;
            string[] strs = new string[amounts];
            Random rand = new Random();
            for (int i = 0; i < amounts; i++)
            {
                strs[i] = Convert.ToChar(rand.Next(97, 122)).ToString();
                st.Put(strs[i], i);
            }
            StringBuilder sb0 = new StringBuilder();
            foreach (var str in strs)
                sb0.Append(str + " ");
            Debug.WriteLine(sb0.ToString());

            StringBuilder sb = new StringBuilder();
            foreach (var item in st.Keys())
                sb.Append(st.Get(item) + " ");
            Debug.WriteLine(sb.ToString());

            Assert.AreEqual(strs.Distinct().Count(), st.Keys().Count());
        }
    }
}
