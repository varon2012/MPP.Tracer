using System.Collections.Concurrent;
using System.Reflection;
using Tracer.Models;

namespace Tracer
{
    public class TraceResult
    {
        public TraceResult()
        {
            ThreadsTraceInfo = new ConcurrentDictionary<int, ThreadTraceInfo>();
        }

        public ConcurrentDictionary<int, ThreadTraceInfo> ThreadsTraceInfo { get; }

        public void StartMethodTrace(int threadId, MethodBase method)
        {
            ThreadsTraceInfo.GetOrAdd(threadId, new ThreadTraceInfo()).StartMethodTrace(new MethodTraceInfo(method));
        }

        public void StopMethodTrace(int threadId)
        {
            ThreadTraceInfo threadTraceInfo;
            ThreadsTraceInfo.TryGetValue(threadId, out threadTraceInfo);
            threadTraceInfo?.StopMethodTrace();
        }
    }
}