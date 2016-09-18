using System.Collections.Concurrent;

namespace Tracer
{
    public class TraceResult
    {
        private ConcurrentDictionary<int, ThreadTraceInfo> threadsTraceInfo;

        public TraceResult()
        {
            threadsTraceInfo = new ConcurrentDictionary<int, ThreadTraceInfo>();
        }

        public ConcurrentDictionary<int, ThreadTraceInfo> ThreadsTraceInfo => threadsTraceInfo;
    }
}