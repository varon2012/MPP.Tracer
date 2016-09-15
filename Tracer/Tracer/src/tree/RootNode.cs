using System.Collections.Generic;
using System.Threading;


namespace MPPTracer.Tree
{
    public class RootNode : INode
    {
        private Dictionary<int, ThreadNode> ThreadTable { get; }

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

        public IEnumerator<KeyValuePair<int,ThreadNode>> GetThreadEnumerator()
        {
            return ThreadTable.GetEnumerator();
        }


        private ThreadNode CurrentThreadNode()
        {
            int threadId = Thread.CurrentThread.ManagedThreadId;
            if (ThreadTable.ContainsKey(threadId))
            {
                return ThreadTable[threadId];
            }

            ThreadNode thread = new ThreadNode();
            ThreadTable.Add(threadId, thread);
           
            return thread;
        }

    }
}
