namespace Algorithms.Part4
{
    public class DepthFirstSearch
    {
        /// <summary>
        /// marked[v] = is there an s-v path?
        /// </summary>
        public bool[] Marked { get; private set; }
        /// <summary>
        /// number of vertices connected to s
        /// </summary>
        public int Count { get; private set; }

        /// <summary>
        /// Computes the vertices in graph G that are connected to the source vertex s.
        /// </summary>
        /// <param name="g"></param>
        /// <param name="s"></param>
        public DepthFirstSearch(Graph g, int s)
        {
            this.Marked = new bool[g.V];
            this.dfs(g, s);
        }

        private void dfs(Graph g, int v)
        {
            this.Count++;
            this.Marked[v] = true;

            foreach (int w in g.Adj[v])
                if (!this.Marked[w])
                    this.dfs(g, w);
        }
    }
}
