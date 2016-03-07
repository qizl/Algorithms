using System;

namespace Algorithms.Part5
{
    public class BoyerMoore
    {
        private int[] _right;
        private string _pat;

        public BoyerMoore(string pat)
        {
            this._pat = pat;
            int m = pat.Length;
            int r = 256;
            this._right = new int[r];
            for (int c = 0; c < r; c++)
                this._right[c] = -1;
            for (int j = 0; j < m; j++)
                this._right[Convert.ToChar(pat.Substring(j, 1))] = j;
        }

        public int Search(string txt)
        {
            int n = txt.Length;
            int m = this._pat.Length;
            int skip = 0;
            for (int i = 0; i <= n - m; i += skip)
            {
                skip = 0;
                for (int j = m - 1; j >= 0; j--)
                    if (this._pat.Substring(j, 1) != txt.Substring(i + j, 1))
                    {
                        skip = j - this._right[Convert.ToChar(txt.Substring(i + j, 1))];
                        if (skip < 1) skip = 1;
                        break;
                    }
                if (skip == 0) return i;
            }
            return n;
        }
    }
}
