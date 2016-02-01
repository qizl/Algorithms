using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Part1
{
    public class ThreeSum
    {
        /// <summary>
        /// Count triples that sum to 0
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static int Count(int[] a)
        {
            int n = a.Length;
            int cnt = 0;
            for (int i = 0; i < n; i++)
                for (int j = i + 1; j < n; j++)
                    for (int k = j + 1; k < n; k++)
                        if (a[i] + a[j] + a[k] == 0)
                            cnt++;
            return cnt;
        }
    }

    public class ThreeSumFast
    {
        /// <summary>
        /// Count triples that sum to 0
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        //public static int Count(int[] a)
        //{
        //    Arrays.Sort(a);
        //    int n = a.Length;
        //    int cnt = 0;
        //    for (int i = 0; i < n; i++)
        //        for (int j = i + 1; j < n; j++)
        //            if (BinarySearch.Rank(-a[i] - a[j], a) > j)
        //                cnt++;
        //    return cnt;
        //}
    }
}
