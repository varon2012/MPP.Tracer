using System.Collections.Concurrent;
using System.Reflection;

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

        }

        public void StopMethodTrace(int threadId)
        {

        }
    }
}