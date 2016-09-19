using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading;

namespace Tracer
{
    public class Tracer : ITracer
    {
        private static readonly Lazy<Tracer> lazy = new Lazy<Tracer>(() => new Tracer());
        public static Tracer Instance => lazy.Value;

        private readonly TraceResult traceResult;
        private Tracer()
        {
            traceResult = new TraceResult();
        }

        public void StartTrace()
        {
            //get previous method obj
            StackFrame frame = new StackFrame(1);
            MethodBase currentMethod = frame.GetMethod();

            traceResult.StartMethodTrace(Thread.CurrentThread.ManagedThreadId, currentMethod);
        }

        public void StopTrace()
        {
            //get previous method obj
            StackFrame frame = new StackFrame(1);
            MethodBase currentMethod = frame.GetMethod();

            traceResult.StopMethodTrace(Thread.CurrentThread.ManagedThreadId, currentMethod);
        }

        public TraceResult GetTraceResult() => traceResult;
    }
}
