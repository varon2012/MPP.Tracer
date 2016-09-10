using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Tracer.Classes.Tree
{
    internal class RootNode : AbstractNode<ThreadNode>
    {
        private Dictionary<int, ThreadNode> threadTable;

        internal RootNode()
        {
            threadTable = new Dictionary<int, ThreadNode>();
        }

        internal override void FixateCountStart(long startTime, CallerDescriptor caller)
        {
            
            ThreadNode thread = GetCurrentThread();
            thread.FixateCountStart(startTime, caller);
        }

        internal override void FixateCountEnd(long endTime)
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
