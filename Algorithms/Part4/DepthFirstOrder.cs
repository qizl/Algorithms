using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Algorithms.Part4
{
    public class DepthFirstOrder
    {
        private bool[] _marked;

        /// <summary>
        /// 所有顶点的前序排序
        /// </summary>
        public Queue<int> Pre { get; private set; }
        /// <summary>
        /// 所有顶点的后序排序
        /// </summary>
        public Queue<int> Post { get; private set; }
        /// <summary>
        /// 所有顶点的逆后序排序
        /// </summary>
        public Stack<int> ReversePost { get; private set; }

        public DepthFirstOrder(Digraph g)
        {
            this.Pre = new Queue<int>();
            this.Post = new Queue<int>();
            this.ReversePost = new Stack<int>();
            this._marked = new bool[g.V];

            for (int v = 0; v < g.V; v++)
                if (!this._marked[v])
                    this.dfs(g, v);
        }

        private void dfs(Digraph g, int v)
        {
            this.Pre.Enqueue(v);

            this._marked[v] = true;
            foreach (int w in g.Adj[v])
                if (!this._marked[w])
                    this.dfs(g, w);

            this.Post.Enqueue(v);
            this.ReversePost.Push(v);
        }
    }
}
