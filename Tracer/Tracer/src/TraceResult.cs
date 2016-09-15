
using System.Collections.Generic;
using MPPTracer.Tree;
using System.Collections;
using System;

namespace MPPTracer
{
    public class TraceResult : IEnumerable<KeyValuePair<int, ThreadNode>>
    {
        private RootNode Root { get; }

        public TraceResult(RootNode root)
        {
            Root = root;
        }

        public IEnumerator<KeyValuePair<int, ThreadNode>> GetEnumerator()
        {
            return Root.GetThreadEnumerator();
            
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
