using System;
using System.Collections.Concurrent;
using System.Reflection;

namespace Tracer
{
    internal class ThreadInfoCollection
    {
        private readonly ConcurrentDictionary<long, ThreadInfo> threadsInfo;

        internal ThreadInfoCollection()
        {
            threadsInfo = new ConcurrentDictionary<long, ThreadInfo>();
        }

        internal ConcurrentDictionary<long, ThreadInfo> ThreadsInfo => threadsInfo;

        internal void StartMethodTrace(long threadId, MethodBase methodBaseInfo)
        {
            ThreadInfo threadInfo = threadsInfo.GetOrAdd(threadId, new ThreadInfo());
            threadInfo.StartMethodTrace(new MethodInfo(methodBaseInfo));
        }

        internal void StopMethodTrace(long threadId, MethodBase methodBaseInfo)
        {
            ThreadInfo threadInfo;
            if (!threadsInfo.TryGetValue(threadId, out threadInfo))
            {
                throw new ArgumentException("There is no thread with id = " + threadId);
            }
            threadInfo.StopMethodTrace(new MethodInfo(methodBaseInfo));
        }
    }
}
