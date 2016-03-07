using Algorithms.Part5;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace Algorithms.Tests
{
    [TestClass]
    public class Part5Test
    {
        [TestMethod]
        public void TestLSD()
        {
            string[] a = new string[] { "kjn", "dsv", "iwo", "upe", "fvh", "kna", "fkv", "jhs", "dkh", "qio", "wpe", };
            //LSD.Sort(a, 3);
            MSD.Sort(a);

            string[] s = a.OrderBy(m => m).ToArray();
            for (int i = 0; i < a.Length; i++)
                Assert.AreEqual(s[i], a[i]);
        }

        [TestMethod]
        public void TestTrieST()
        {
            // build symbol table
            TrieST<string> st = new TrieST<string>();
            string[] a = new string[] { "by", "sea", "sells", "she", "shells", "shore", "the", };
            for (int i = 0; i < a.Length; i++) st.Put(a[i], i.ToString());

            // print results
            if (st.Size() < 100)
            {
                Debug.WriteLine("keys(\"\"):");
                foreach (string key in st.Keys())
                    Debug.WriteLine(key + " " + st.Get(key));
                Debug.WriteLine(Environment.NewLine);
            }

            Debug.WriteLine("longestPrefixOf(\"shellsort\"):");
            Debug.WriteLine(st.LongestPrefixOf("shellsort"));
            Debug.WriteLine(Environment.NewLine);

            Debug.WriteLine("longestPrefixOf(\"quicksort\"):");
            Debug.WriteLine(st.LongestPrefixOf("quicksort"));
            Debug.WriteLine(Environment.NewLine);

            Debug.WriteLine("keysWithPrefix(\"shor\"):");
            foreach (string s in st.KeysWithPrefix("shor"))
                Debug.WriteLine(s);
            Debug.WriteLine(Environment.NewLine);

            Debug.WriteLine("keysThatMatch(\".he.l.\"):");
            foreach (string s in st.KeysThatMatch(".he.l."))
                Debug.WriteLine(s);
        }

        [TestMethod]
        public void TestTST()
        {
            // build symbol table
            TST<string> st = new TST<string>();
            string[] a = new string[] { "by", "sea", "sells", "she", "shells", "shore", "the", };
            for (int i = 0; i < a.Length; i++) st.Put(a[i], i.ToString());

            // print results
            if (st.Size() < 100)
            {
                Debug.WriteLine("keys(\"\"):");
                foreach (string key in st.Keys())
                    Debug.WriteLine(key + " " + st.Get(key));
                Debug.WriteLine(Environment.NewLine);
            }

            Debug.WriteLine("longestPrefixOf(\"shellsort\"):");
            Debug.WriteLine(st.LongestPrefixOf("shellsort"));
            Debug.WriteLine(Environment.NewLine);

            Debug.WriteLine("longestPrefixOf(\"quicksort\"):");
            Debug.WriteLine(st.LongestPrefixOf("quicksort"));
            Debug.WriteLine(Environment.NewLine);

            Debug.WriteLine("keysWithPrefix(\"shor\"):");
            foreach (string s in st.KeysWithPrefix("shor"))
                Debug.WriteLine(s);
            Debug.WriteLine(Environment.NewLine);

            Debug.WriteLine("keysThatMatch(\".he.l.\"):");
            foreach (string s in st.KeysThatMatch(".he.l."))
                Debug.WriteLine(s);
        }

        [TestMethod]
        public void TestSubstringSearch()
        {
            string txt = "does the java system sort use one of these methods for searching with string keys?";
            string pat = "string";
            int indexBruteForce = BFSSearch.BruteForce(pat, txt);
            Assert.AreEqual(pat, txt.Substring(indexBruteForce, pat.Length));

            int indexExplicitBackup = BFSSearch.ExplicitBackup(pat, txt);
            Assert.AreEqual(pat, txt.Substring(indexExplicitBackup, pat.Length));
        }

        [TestMethod]
        public void TestKMP()
        {
            string txt = "abacadabrabracabracadabrabrabracad";
            string pat = "rab";
            int index = new KMP(pat).Search(txt);
            Assert.AreEqual(pat, txt.Substring(index, pat.Length));
        }

        [TestMethod]
        public void TestBoyerMoore()
        {
            string txt = "abacadabrabracabracadabrabrabracad";
            string pat = "rab";
            int index = new BoyerMoore(pat).Search(txt);
            Assert.AreEqual(pat, txt.Substring(index, pat.Length));
        }

        [TestMethod]
        public void TestRabinKarp()
        {
            string txt = "abacadabrabracabracadabrabrabracad";
            string pat = "rab";
            int index = new RabinKarp(pat).Search(txt);
            Assert.AreEqual(pat, txt.Substring(index, pat.Length));
        }

        [TestMethod]
        public void TestRegex()
        {
            Assert.IsTrue(Regex.IsMatch("1100011", "^(0|1(01*0)*1)*$"));
            Assert.IsTrue(Regex.IsMatch("q@qizl.cn", @"^(\w)+(\.\w+)*@(\w)+((\.\w+)+)$"));
        }
    }
}
