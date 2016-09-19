using System.Collections.Concurrent;
using System.Collections.Generic;
using Tracer.Models;
using Tracer.Tracers;

namespace Tracer
{
    public class TraceResult
    {
        public TraceResult(ConcurrentDictionary<int, ThreadTracer> threadTracers)
        {
            ThreadsTraceResult = new Dictionary<int, ThreadTraceResult>();
            foreach (KeyValuePair<int, ThreadTracer> threadTracer in threadTracers)
            {
                ThreadsTraceResult.Add(threadTracer.Key, threadTracer.Value.GetTraceResult());
            }
        }

        public Dictionary<int, ThreadTraceResult> ThreadsTraceResult { get; }
    }
}