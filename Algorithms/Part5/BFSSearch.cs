namespace Algorithms.Part5
{
    /// <summary>
    /// 暴力子字符串查找
    /// </summary>
    public class BFSSearch
    {
        public static int BruteForce(string pat, string txt)
        {
            int m = pat.Length;
            int n = txt.Length;
            for (int i = 0; i <= n - m; i++)
            {
                int j = 0;
                for (; j < m; j++)
                    if (txt.Substring(i + j, 1) != pat.Substring(j, 1))
                        break;
                if (j == m) return i;
            }
            return n;
        }

        /// <summary>
        /// 显式回退
        /// </summary>
        /// <param name="pat"></param>
        /// <param name="txt"></param>
        /// <returns></returns>
        public static int ExplicitBackup(string pat, string txt)
        {
            int i, j;
            int m = pat.Length;
            int n = txt.Length;
            for (i = 0, j = 0; i < n && j < m; i++)
            {
                if (txt.Substring(i, 1) == pat.Substring(j, 1)) j++;
                else { i -= j; j = 0; }
            }
            if (j == m) return i - m;
            else return n;
        }
    }
}
