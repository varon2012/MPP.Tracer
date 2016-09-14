using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Reflection;

namespace Tracer
{
    public class TraceResult
    {
        private readonly ConcurrentDictionary<int, ThreadTraceInfo> fThreadsTraceInfo;

        internal TraceResult()
        {
            fThreadsTraceInfo = new ConcurrentDictionary<int, ThreadTraceInfo>();
        }

        internal void InitMethodTrace(int threadId, MethodBase method)
        {
            ThreadTraceInfo threadsTraceInfo = fThreadsTraceInfo.GetOrAdd(threadId, new ThreadTraceInfo());
            threadsTraceInfo.InitMethodTrace(new MethodTraceInfo(method));
        }

        internal void FinishMethodTrace(int threadId)
        {
            ThreadTraceInfo threadsTraceInfo;
            if (!fThreadsTraceInfo.TryGetValue(threadId, out threadsTraceInfo))
            {
                throw new ArgumentException("invalid thread id");
            }
            threadsTraceInfo.FinishMethodTrace();
        }

        internal IEnumerable<KeyValuePair<int, ThreadTraceInfo>> ThreadsTraceInfo => fThreadsTraceInfo;
    }
}
