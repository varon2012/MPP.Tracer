
using System.Collections.Generic;
using MPPTracer.Tree;

namespace MPPTracer
{
    public class TraceResult
    {
        private RootNode Root { get; set; }
        public int ThreadsCount
        {
            get
            {
                Dictionary<int, ThreadNode> threads = Root.ThreadTable;
                return threads.Count;
            }
        }
        private List<int> threadsId;

        public TraceResult(RootNode root)
        {
            Root = root;
            threadsId = new List<int>(Root.ThreadTable.Keys);
        }

        public int GetThreadId(int index)
        {
            return threadsId[index];
        }

        public List<MethodNode> GetThreadForest(int threadId)
        {
            Dictionary<int, ThreadNode> threadTable = Root.ThreadTable;
            return threadTable[threadId].NestedMethods;
        }

        
    }
}
