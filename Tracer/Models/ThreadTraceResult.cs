using System.Collections.Generic;

namespace Tracer.Models
{
    public class ThreadTraceResult
    {
        public ThreadTraceResult()
        {
            MethodsTraceResult = new List<MethodTraceResult>();
        }

        public List<MethodTraceResult> MethodsTraceResult { get; }
        public long Duration { get; private set; }

        public void AddMethodTraceResult(MethodTraceResult methodTraceResult)
        {
            MethodsTraceResult.Add(methodTraceResult);
            Duration += methodTraceResult.Duration;
        }
    }
}