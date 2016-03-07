using System;

namespace Algorithms.Part5
{
    public class RabinKarp
    {
        /// <summary>
        /// the pattern
        /// needed only for Las Vegas
        /// </summary>
        private string _pat;
        /// <summary>
        /// pattern hash value
        /// </summary>
        private long _patHash;
        /// <summary>
        /// pattern length
        /// </summary>
        private int _m;
        /// <summary>
        /// a large prime, small enough to avoid long overflow
        /// </summary>
        private long _q;
        /// <summary>
        /// radix
        /// </summary>
        private int _r;
        /// <summary>
        /// R^(M-1) % Q
        /// </summary>
        private long _rm;

        public RabinKarp(string pat)
        {
            this._pat = pat; // save pattern (needed only for Las Vegas)
            this._r = 256;
            this._m = pat.Length;
            this._q = this.longRandomPrime();

            // precompute R^(M-1) % Q for use in removing leading digit
            this._rm = 1;
            for (int i = 1; i <= _m - 1; i++)
                this._rm = (this._r * this._rm) % this._q;
            this._patHash = this.hash(this._pat, this._m);
        }

        /// <summary>
        /// a random 31-bit prime
        /// </summary>
        /// <returns></returns>
        private long longRandomPrime() { return 997; }

        /// <summary>
        /// Monte Carlo version: always return true
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        private bool check(int i) { return true; }

        /// <summary>
        /// Las Vegas version: does pat[] match txt[i..i-M+1] ?
        /// </summary>
        /// <param name="txt"></param>
        /// <param name="i"></param>
        /// <returns></returns>
        private bool check(string txt, int i)
        {
            for (int j = 0; j < this._m; j++)
                if (this._pat.Substring(j, 1) != txt.Substring(i + j, 1))
                    return false;
            return true;
        }

        /// <summary>
        /// Compute hash for key[0..M-1]. 
        /// Horner
        /// </summary>
        /// <param name="key"></param>
        /// <param name="M"></param>
        /// <returns></returns>
        private long hash(string key, int M)
        {
            long h = 0;
            for (int j = 0; j < this._m; j++)
                h = (this._r * h + Convert.ToChar(key.Substring(j, 1))) % this._q;
            return h;
        }

        /// <summary>
        /// Returns the index of the first occurrrence of the pattern string in the text string.
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public int Search(string txt)
        {
            int N = txt.Length;
            if (N < this._m) return N;
            long txtHash = this.hash(txt, this._m);

            if ((this._patHash == txtHash) && this.check(txt, 0))
                return 0;

            for (int i = this._m; i < N; i++)
            {
                // Remove leading digit, add trailing digit, check for match. 
                txtHash = (txtHash + this._q - this._rm * Convert.ToChar(txt.Substring(i - _m, 1)) % this._q) % this._q;
                txtHash = (txtHash * this._r + Convert.ToChar(txt.Substring(i, 1))) % this._q;

                int offset = i - this._m + 1;
                if ((this._patHash == txtHash) && this.check(txt, offset))
                    return offset;
            }

            return N;
        }
    }
}
