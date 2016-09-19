using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Reflection;

namespace Tracer
{
    public class ThreadInfoList
    {
        private readonly ConcurrentDictionary<long, ThreadInfo> threadsInfo;

        internal ThreadInfoList()
        {
            threadsInfo = new ConcurrentDictionary<long, ThreadInfo>();
        }

        public IEnumerable ThreadsInfo => threadsInfo;

        internal void StartMethodTraceByThread(long threadId, MethodBase methodBaseInfo)
        {
            ThreadInfo threadInfo = threadsInfo.GetOrAdd( threadId, new ThreadInfo());
            threadInfo.StartMethodTrace(new MethodInfo(methodBaseInfo));
        }

        internal void StopMethodTraceByThread(long threadId, MethodBase methodBaseInfo)
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
