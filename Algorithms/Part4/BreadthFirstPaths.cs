using System.Collections.Generic;

namespace Algorithms.Part4
{
    public class BreadthFirstPaths
    {
        /// <summary>
        /// marked[v] = is there an s-v path?
        /// </summary>
        public bool[] Marked { get; private set; }
        /// <summary>
        /// edgeTo[v] = last edge on s-v path
        /// </summary>
        private int[] _edgeTo;
        /// <summary>
        /// source vertex
        /// </summary>
        private readonly int _s;

        /// <summary>
        /// Computes the shortest path between any one of the source vertices in sources
        /// </summary>
        /// <param name="g"></param>
        /// <param name="s"></param>
        public BreadthFirstPaths(Graph g, int s)
        {
            this.Marked = new bool[g.V];
            this._edgeTo = new int[g.V];
            this._s = s;

            this.bfs(g, s);
        }

        private void bfs(Graph g, int s)
        {
            this.Marked[s] = true;
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(s);

            while (queue.Count != 0)
            {
                int v = queue.Dequeue();
                foreach (int w in g.Adj[s])
                    if (!this.Marked[w])
                    {
                        this._edgeTo[w] = s;
                        this.Marked[w] = true;
                        queue.Enqueue(w);
                    }
            }
        }

        public bool HasPathTo(int v) { return this.Marked[v]; }

        public IEnumerable<int> PathTo(int v)
        {
            if (!this.HasPathTo(v))
                return null;

            Stack<int> path = new Stack<int>();
            for (int x = v; x != this._s; x = this._edgeTo[x])
                path.Push(x);
            path.Push(this._s);

            return path;
        }
    }
}
