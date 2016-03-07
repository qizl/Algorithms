using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Part5
{
    public class Quick3String
    {
        /// <summary>
        /// 小数组的切换阈值
        /// </summary>
        private const int M = 15;

        private Quick3String() { }

        public static void Sort(string[] a)
        {
            shuffle(a);
            sort(a, 0, a.Length - 1, 0);
        }

        private static void shuffle(string[] a)
        {
            Random rand = new Random();
            for (int i = 0; i < a.Length; i++)
                exch(a, i, rand.Next(0, a.Length - 1));
        }

        private static int charAt(string s, int d)
        {
            if (d < s.Length)
                return Convert.ToChar(s.Substring(d, 1));
            else
                return -1;
        }

        private static void sort(string[] a, int lo, int hi, int d)
        {
            if (hi <= lo + M)
            {
                insertion(a, lo, hi, d);
                return;
            }

            if (hi <= lo) return;
            int lt = lo, gt = hi;
            int v = charAt(a[lo], d);
            int i = lo + 1;
            while (i <= gt)
            {
                int t = charAt(a[i], d);
                if (t < v) exch(a, lt++, i++);
                else if (t > v) exch(a, i, gt--);
                else i++;
            }

            // a[lo..lt-1] < v = a[lt..gt] < a[gt+1..hi]. 
            sort(a, lo, lt - 1, d);
            if (v >= 0) sort(a, lt, gt, d + 1);
            sort(a, gt + 1, hi, d);
        }

        #region Insertion Sort
        /// <summary>
        /// insertion sort a[lo..hi], starting at dth character
        /// </summary>
        /// <param name="a"></param>
        /// <param name="lo"></param>
        /// <param name="hi"></param>
        /// <param name="d"></param>
        private static void insertion(string[] a, int lo, int hi, int d)
        {
            for (int i = lo; i <= hi; i++)
                for (int j = i; j > lo && less(a[j], a[j - 1], d); j--)
                    exch(a, j, j - 1);
        }

        /// <summary>
        /// exchange a[i] and a[j]
        /// </summary>
        /// <param name="a"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        private static void exch(string[] a, int i, int j)
        {
            string temp = a[i];
            a[i] = a[j];
            a[j] = temp;
        }

        /// <summary>
        /// is v less than w, starting at character d
        /// </summary>
        /// <param name="v"></param>
        /// <param name="w"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        private static bool less(string v, string w, int d)
        {
            for (int i = d; i < Math.Min(v.Length, w.Length); i++)
            {
                if (Convert.ToChar(v.Substring(i, 1)) < Convert.ToChar(w.Substring(i, 1))) return true;
                if (Convert.ToChar(v.Substring(i, 1)) > Convert.ToChar(w.Substring(i, 1))) return false;
            }
            return v.Length < w.Length;
        }
        #endregion
    }
}
