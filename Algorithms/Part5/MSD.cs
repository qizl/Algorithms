using System;

namespace Algorithms.Part5
{
    public class MSD
    {
        /// <summary>
        /// 基数
        /// </summary>
        private static int R = 256;
        /// <summary>
        /// 小数组的切换阈值
        /// </summary>
        private const int M = 15;
        /// <summary>
        /// 数组分类的辅助数组
        /// </summary>
        private static string[] Aux;

        private MSD() { }

        private static int charAt(string s, int d)
        {
            if (d < s.Length)
                return Convert.ToChar(s.Substring(d, 1));
            else
                return -1;
        }

        public static void Sort(string[] a)
        {
            int n = a.Length;
            Aux = new string[n];
            sort(a, 0, n - 1, 0);
        }

        /// <summary>
        /// 以第d个字符为键将a[lo]至a[hi]排序
        /// </summary>
        /// <param name="a"></param>
        /// <param name="lo"></param>
        /// <param name="hi"></param>
        /// <param name="d"></param>
        private static void sort(string[] a, int lo, int hi, int d)
        {
            if (hi <= lo + M)
            {
                insertion(a, lo, hi, d);
                return;
            }

            // 计算频率
            int[] count = new int[R + 2];
            for (int i = lo; i <= hi; i++)
                count[charAt(a[i], d) + 2]++;

            // 将频率转换为索引
            for (int r = 0; r < R + 1; r++)
                count[r + 1] += count[r];

            // 数据分类
            for (int i = lo; i <= hi; i++)
                Aux[count[charAt(a[i], d) + 1]++] = a[i];

            // 回写
            for (int i = lo; i <= hi; i++)
                a[i] = Aux[i - lo];

            // 递归地以每个字符为键进行排序
            for (int r = 0; r < R; r++)
                sort(a, lo + count[r], lo + count[r + 1] - 1, d + 1);
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
