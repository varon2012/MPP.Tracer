using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace MPPTracer.Tree
{
    public class RootNode : INode, IEnumerable<ThreadNode>
    {
        private Dictionary<int, ThreadNode> ThreadTable { get; }
        private readonly static object syncRoot = new object();

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
            ThreadNode thread;
            bool threadExists;

            lock (syncRoot)
            {
                threadExists = ThreadTable.ContainsKey(threadId);
            }

            if (threadExists)
            {
                thread = ThreadTable[threadId];
            }
            else
            {
                thread = new ThreadNode(threadId);
                lock (syncRoot)
                {
                    ThreadTable.Add(threadId, thread);
                }
            }
           
            return thread;
        }

        public IEnumerator<ThreadNode> GetEnumerator()
        {
            IEnumerator<ThreadNode> enumerator;
            lock (syncRoot)
            {
                List<ThreadNode> oldList = new List<ThreadNode>(ThreadTable.Values);
                List<ThreadNode> newList = oldList.GetRange(0, oldList.Count);
                enumerator = newList.GetEnumerator();
            }
            return enumerator;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
