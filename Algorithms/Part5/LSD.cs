using System;

namespace Algorithms.Part5
{
    public class LSD
    {
        private LSD() { }

        /// <summary>
        /// Rearranges the array of W-character strings in ascending order.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="w"></param>
        public static void Sort(string[] a, int w)
        {
            int n = a.Length;
            int r = 256;
            string[] aux = new string[n];

            for (int d = w - 1; d >= 0; d--)
            {
                // 根据第d个字符用键索引计数法排序

                int[] count = new int[r + 1];

                // 计算出现频率
                for (int i = 0; i < n; i++)
                    count[Convert.ToChar(a[i].Substring(d, 1)) + 1]++;

                // 将频率转换为索引
                for (int i = 0; i < r; i++)
                    count[i + 1] += count[i];

                // 将元素分类
                for (int i = 0; i < n; i++)
                    aux[count[Convert.ToChar(a[i].Substring(d, 1))]++] = a[i];

                // 回写
                for (int i = 0; i < n; i++)
                    a[i] = aux[i];
            }
        }
    }
}
