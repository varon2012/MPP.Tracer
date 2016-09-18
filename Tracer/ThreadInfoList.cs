using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    public class ThreadInfoList
    {
        private ConcurrentDictionary<long, ThreadInfo> threadsInfo;

        public ThreadInfoList()
        {
            threadsInfo = new ConcurrentDictionary<long, ThreadInfo>();
        }

        public void StartMethodTraceByThread(long threadId, MethodBase methodBaseInfo)
        {
            ThreadInfo threadInfo = threadsInfo.GetOrAdd( threadId, new ThreadInfo());
            threadInfo.StartMethodTrace(new MethodInfo(methodBaseInfo));
        }

        public void StopMethodTraceByThread(long threadId, MethodBase methodBaseInfo)
        {
            ThreadInfo threadInfo;
            if (!threadsInfo.TryGetValue(threadId, out threadInfo))
            {
                throw new Exception("There is no thread with id = " + threadId);
            }

            threadInfo.StopMethodTrace(new MethodInfo(methodBaseInfo));
        }
    }
}
