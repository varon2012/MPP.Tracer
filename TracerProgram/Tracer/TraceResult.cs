using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Tracer
{
    public class TraceResult
    {
        public Dictionary<int,ThreadNode> threadList;
        private readonly static object locker = new object();

        public TraceResult()
        {
            threadList = new Dictionary<int,ThreadNode>();
        }

        public void StartTrace(MethodInfo info)
        {
            ThreadNode thread = GetTreeElement();
            thread.AddMethod(info);
        }

        public void StopTrace(long time)
        {
            ThreadNode thread = GetTreeElement();
            thread.StopMethod(time);
        }

        private ThreadNode GetTreeElement()
        {
            int threadId = Thread.CurrentThread.ManagedThreadId;
            ThreadNode threadNode;

            lock (locker)
            {
                if (threadList.ContainsKey(threadId))
                {
                    threadNode = threadList[threadId];
                }
                else
                {
                    threadNode = new ThreadNode(threadId);
                    threadList.Add(threadId, threadNode);
                }
            }
            return threadNode;
        }
         
    }
}
