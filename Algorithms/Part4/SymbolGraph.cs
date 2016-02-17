using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Part4
{
    public class SymbolGraph
    {
        /// <summary>
        /// string -> index
        /// </summary>
        private Dictionary<string, int> _dics;

        /// <summary>
        /// index  -> string
        /// </summary>
        private string[] _keys;

        public Graph Graph { get; private set; }

        /// <summary>
        /// Initializes a graph from a string using the specified delimiter.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="delimiter"></param>
        public SymbolGraph(string str, char delimiter)
        {
            this._dics = new Dictionary<string, int>();

            // First pass builds the index by reading strings to associate distinct strings with an index
            string[] strs = str.Split(delimiter);
            for (int i = 0; i < strs.Length; i++)
                if (!string.IsNullOrEmpty(strs[i]))
                    if (!this._dics.ContainsKey(strs[i]))
                        this._dics.Add(strs[i], this._dics.Count);

            // inverted index to get string keys in an aray
            this._keys = new string[this._dics.Count];
            Array.Copy(this._dics.Keys.ToArray(), this._keys, this._dics.Count);

            // second pass builds the graph by connecting first vertex on each line to all others
            this.Graph = new Graph(this._dics.Count);
            for (int i = 0; i < strs.Length; i++)
                if (!string.IsNullOrEmpty(strs[i]))
                    this.Graph.AddEdge(this._dics[strs[i]], this._dics[strs[++i]]);
        }

        /// <summary>
        /// Does the graph contain the vertex named s?
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public bool Contains(string s) { return this._dics.ContainsKey(s); }

        /// <summary>
        /// Returns the integer associated with the vertex named s.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public int Index(string s) { return this._dics[s]; }

        /// <summary>
        /// Returns the name of the vertex associated with the integer v.
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public string Name(int v) { return this._keys[v]; }

        public override string ToString()
        {
            StringBuilder s = new StringBuilder();
            s.Append(this.Graph.V + " vertices, " + this.Graph.E + " edges " + Environment.NewLine);
            for (int v = 0; v < this.Graph.V; v++)
            {
                s.Append(this.Name(v) + "(" + v + "): ");
                foreach (int w in this.Graph.Adj[v])
                    s.Append(this.Name(w) + "(" + w + ") ");
                s.Append(Environment.NewLine);
            }
            return s.ToString();
        }
    }
}
