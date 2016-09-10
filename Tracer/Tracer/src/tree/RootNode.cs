using System.Collections.Generic;
using System.Threading;

namespace Tracer.Tree
{
    public class RootNode : INode
    {
        private Dictionary<int, ThreadNode> threadTable;

        internal RootNode()
        {
            threadTable = new Dictionary<int, ThreadNode>();
        }

        public void FixateCountStart(long startTime, CallerDescriptor caller)
        {
            
            ThreadNode thread = GetCurrentThread();
            thread.FixateCountStart(startTime, caller);
        }

        public void FixateCountEnd(long endTime)
        {
            ThreadNode thread = GetCurrentThread();
            thread.FixateCountEnd(endTime);
        }

        private ThreadNode GetCurrentThread()
        {
            int threadId = Thread.CurrentThread.ManagedThreadId;
            if (threadTable.ContainsKey(threadId))
            {
                return threadTable[threadId];
            }

            ThreadNode thread = new ThreadNode(threadId);
            threadTable.Add(threadId, thread);
           
            return thread;
        }

    }
}
