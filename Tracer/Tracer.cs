using System.Diagnostics;
using System.Threading;
using Tracer.Interfaces;

namespace Tracer
{
    public class Tracer : ITracer
    {
        private static readonly Tracer instance = new Tracer();
        private readonly TraceResult traceResult;

        private Tracer()
        {
           traceResult = new TraceResult();
        }

        public static Tracer Instance => instance;

        public void StartTrace()
        {
            var currentThreadId = Thread.CurrentThread.ManagedThreadId;
            var currentMethod = new StackTrace().GetFrame(1).GetMethod();
            traceResult.StartMethodTrace(currentThreadId, currentMethod);
        }

        public void StopTrace()
        {
            var currentThreadId = Thread.CurrentThread.ManagedThreadId;
            traceResult.StopMethodTrace(currentThreadId);
        }

        public TraceResult GetTraceResult()
        {
            return traceResult;
        }
    }
}


