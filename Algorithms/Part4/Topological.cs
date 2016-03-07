using System.Collections.Generic;

namespace Algorithms.Part4
{
    public class Topological
    {
        public IEnumerable<int> Order { get; private set; }

        public Topological(Digraph g)
        {
            DirectedCycle cycleFinder = new DirectedCycle(g);
            if (!cycleFinder.HasCycle())
            {
                DepthFirstOrder dfs = new DepthFirstOrder(g);
                this.Order = dfs.ReversePost;
            }
        }

        public Topological(EdgeWeightedDigraph g)
        {
            //EdgeWeightedDirectedCycle cycleFinder = new EdgeWeightedDirectedCycle(g);
            //if (!cycleFinder.HasCycle())
            //{
            DepthFirstOrder dfs = new DepthFirstOrder(g);
            this.Order = dfs.ReversePost;
            //}
        }

        public bool IsDAG() { return this.Order != null; }
    }
}
