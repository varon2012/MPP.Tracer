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

        public void AddNestedTrace(long startTime, CallerDescriptor caller)
        {
            
            ThreadNode thread = CurrentThreadNode();
            thread.AddNestedTrace(startTime, caller);
        }

        public void StopLastTrace(long endTime)
        {
            ThreadNode thread = CurrentThreadNode();
            thread.StopLastTrace(endTime);
        }

        private ThreadNode CurrentThreadNode()
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
