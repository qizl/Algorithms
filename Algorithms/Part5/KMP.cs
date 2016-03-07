using System;

namespace Algorithms.Part5
{
    public class KMP
    {
        private string _pat;
        private int[,] _dfa;

        public KMP(string pat)
        {
            this._pat = pat;
            int m = pat.Length;
            int r = 256;
            this._dfa = new int[r, m];
            this._dfa[Convert.ToChar(pat.Substring(0, 1)), 0] = 1;
            for (int x = 0, j = 1; j < m; j++)
            {
                for (int c = 0; c < r; c++)
                    this._dfa[c, j] = this._dfa[c, x];
                this._dfa[Convert.ToChar(pat.Substring(j, 1)), j] = j + 1;
                x = this._dfa[Convert.ToChar(pat.Substring(j, 1)), x];
            }
        }

        public int Search(string txt)
        {
            int m = this._pat.Length;
            int n = txt.Length;
            int i = 0, j = 0;
            for (; i < n && j < m; i++)
                j = this._dfa[Convert.ToChar(txt.Substring(i, 1)), j];
            if (j == m) return i - m;
            else return n;
        }
    }
}
