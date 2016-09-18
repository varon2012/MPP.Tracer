using System.Collections.Generic;
using MPPTracer.Tree;
using System.Collections;

namespace MPPTracer
{
    public class TraceResult : IEnumerable<ThreadNode>
    {
        private RootNode Root { get; }

        public TraceResult(RootNode root)
        {
            Root = root;
        }

        public IEnumerator<ThreadNode> GetEnumerator()
        {
            return Root.GetEnumerator();
            
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
