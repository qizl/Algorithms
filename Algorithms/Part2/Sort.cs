using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Part2
{
    public class Sort
    {
        #region Elementary Sorts
        public static void BubbleSort(IComparable[] a)
        {
            for (int i = 0; i < a.Length; i++)
                for (int j = 0; j < a.Length - i - 1; j++)
                    if (isLess(a[j + 1], a[j]))
                        exch(a, j, j + 1);
        }

        public static void Selection(IComparable[] a)
        {
            int n = a.Length;
            for (int i = 0; i < n; i++)
            {
                int min = i;
                for (int j = i + 1; j < n; j++)
                    if (isLess(a[j], a[min]))
                        min = j;
                exch(a, i, min);
            }
        }

        public static void Insertion(IComparable[] a)
        {
            int n = a.Length;
            for (int i = 1; i < n; i++)
                for (int j = i; j > 0 && isLess(a[j], a[j - 1]); j--)
                    exch(a, j, j - 1);
        }

        public static void ImprovedInsertion(IComparable[] a)
        {
            int n = a.Length;
            for (int i = 1; i < n; i++)
            {
                int j = i;
                IComparable b = a[j];
                for (; j > 0 && isLess(b, a[j - 1]); j--)
                    a[j] = a[j - 1];
                a[j] = b;
            }
        }

        public static void ShellSort(IComparable[] a)
        {
            int n = a.Length;
            int h = 1;

            while (h < n / 3)
                h = 3 * h + 1;

            // h-sort the array
            while (h >= 1)
            {
                for (int i = h; i < n; i++)
                    for (int j = i; j >= h && isLess(a[j], a[j - h]); j -= h)
                        exch(a, j, j - h);

                h = h / 3;
            }
        }
        #endregion

        #region Quicksort
        public static void Quicksort(IComparable[] a)
        {
            Shuffle(a);
            quicksort(a, 0, a.Length - 1);
        }

        public static void Shuffle(IComparable[] a)
        {
            Random rand = new Random();
            for (int i = 0; i < a.Length; i++)
                exch(a, i, rand.Next(0, a.Length - 1));
        }

        private static void quicksort(IComparable[] a, int lo, int hi)
        {
            if (hi <= lo) return;

            int j = partition(a, lo, hi);
            quicksort(a, lo, j - 1);
            quicksort(a, j + 1, hi);
        }

        #region 改进1：小数组进行插入排序
        private static void insertion(IComparable[] a, int lo, int hi)
        {
            int n = hi - lo + 1;
            for (int i = lo; i < n; i++)
                for (int j = i; j > 0 && isLess(a[j], a[j - 1]); j--)
                    exch(a, j, j - 1);
        }
        private static void insertedQuicksort(IComparable[] a, int lo, int hi)
        {
            int m = 5; // 5~15
            if (hi <= lo + m)
            {
                insertion(a, lo, hi);
                return;
            }

            int j = partition(a, lo, hi);
            insertedQuicksort(a, lo, j - 1);
            insertedQuicksort(a, j + 1, hi);
        }
        #endregion

        #region 改进2：三取样切分
        #endregion

        #region 改进3：三向切分
        private static void quick3WaySort(IComparable[] a, int lo, int hi)
        {
            if (hi <= lo) return;
            int lt = lo, i = lo + 1, gt = hi;

            IComparable v = a[lo];
            while (i <= gt)
            {
                int cmp = a[i].CompareTo(v);
                if (cmp < 0) exch(a, lt++, i++);
                else if (cmp > 0) exch(a, i, gt--);
                else i++;
            }

            quick3WaySort(a, lo, lt - 1);
            quick3WaySort(a, gt + 1, hi);
        }
        #endregion

        private static int partition(IComparable[] a, int lo, int hi)
        {
            int i = lo, j = hi + 1;
            IComparable v = a[lo];

            while (true)
            {
                while (isLess(a[++i], v))
                    if (i == hi) break;
                while (isLess(v, a[--j]))
                    if (j == lo) break;

                if (i >= j)
                    break;

                exch(a, i, j);
            }
            exch(a, lo, j);

            return j;
        }
        #endregion

        #region HeapSort
        public static void HeapSort(IComparable[] pq)
        {
            int N = pq.Length;

            for (int k = N / 2; k >= 1; k--)
                sink(pq, k, N);

            while (N > 1)
            {
                exchHeap(pq, 1, N--);
                sink(pq, 1, N);
            }
        }

        private static void sink(IComparable[] pq, int k, int N)
        {
            while (2 * k <= N)
            {
                int j = 2 * k;
                if (j < N && isLess(pq, j, j + 1)) j++;
                if (!isLess(pq, k, j)) break;
                exchHeap(pq, k, j);
                k = j;
            }
        }

        private static bool isLess(IComparable[] pq, int i, int j)
        {
            return pq[i - 1].CompareTo(pq[j - 1]) < 0;
        }

        private static void exchHeap(IComparable[] pq, int i, int j)
        {
            exch(pq, i - 1, j - 1);
        }
        #endregion

        private static bool isLess(IComparable v, IComparable w)
        {
            return v.CompareTo(w) < 0;
        }

        private static void exch(IComparable[] a, int i, int j)
        {
            IComparable t = a[i];
            a[i] = a[j];
            a[j] = t;
        }

        public static string Show(IComparable[] a)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < a.Length; i++)
                sb.Append(a[i] + " ");

            return sb.ToString();
        }

        public static bool IsSorted(IComparable[] a)
        {
            for (int i = 1; i < a.Length; i++)
                if (isLess(a[i], a[i - 1])) return false;
            return true;
        }
    }
}