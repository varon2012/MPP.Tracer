using System.Collections.Generic;
using System.Threading;

namespace Tracer.Tree
{
    public class RootNode : INode
    {
        public Dictionary<int, ThreadNode> ThreadTable { get; private set; }

        internal RootNode()
        {
            ThreadTable = new Dictionary<int, ThreadNode>();
        }

        public void AddNestedTrace(long startTime, MethodDescriptor descriptor)
        {
            
            ThreadNode thread = CurrentThreadNode();
            thread.AddNestedTrace(startTime, descriptor);
        }

        public void StopLastTrace(long endTime)
        {
            ThreadNode thread = CurrentThreadNode();
            thread.StopLastTrace(endTime);
        }

        private ThreadNode CurrentThreadNode()
        {
            int threadId = Thread.CurrentThread.ManagedThreadId;
            if (ThreadTable.ContainsKey(threadId))
            {
                return ThreadTable[threadId];
            }

            ThreadNode thread = new ThreadNode(threadId);
            ThreadTable.Add(threadId, thread);
           
            return thread;
        }

    }
}
