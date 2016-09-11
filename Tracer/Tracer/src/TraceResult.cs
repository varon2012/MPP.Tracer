
using System.Collections.Generic;
using Tracer.Tree;

namespace Tracer
{
    public class TraceResult
    {
        private RootNode Root { get; set; }
        public List<int> Threads { get; private set; }

        public TraceResult(RootNode root)
        {
            Root = root;
            Threads = new List<int>(Root.ThreadTable.Keys);
        }

        public List<MethodNode> GetThreadForest(int threadId)
        {
            Dictionary<int, ThreadNode> threadTable = Root.ThreadTable;
            return threadTable[threadId].NestedMethods;
        }


    }
}
