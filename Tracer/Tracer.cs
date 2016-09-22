using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using Tracer.TraceResultBuild;
using Tracer.TraceResultData;

namespace Tracer
{
    public class Tracer : ITracer
    {
        private static readonly Lazy<Tracer> lazy = new Lazy<Tracer>(() => new Tracer());
        public static Tracer Instance => lazy.Value;

        private readonly ThreadInfoCollection traceResultController;
        private Tracer()
        {
            traceResultController = new ThreadInfoCollection();
        }

        public void StartTrace()
        {
            //get previous method obj
            StackFrame frame = new StackFrame(1);
            MethodBase currentMethod = frame.GetMethod();

            traceResultController.StartMethodTrace(Thread.CurrentThread.ManagedThreadId, currentMethod);
        }

        public void StopTrace()
        {
            //get previous method obj
            StackFrame frame = new StackFrame(1);
            MethodBase currentMethod = frame.GetMethod();

            traceResultController.StopMethodTrace(Thread.CurrentThread.ManagedThreadId, currentMethod);
        }

        public TraceResult GetTraceResult()
        {
            return new TraceResultBuilder(traceResultController.ThreadsInfo).GetResult();
        }
    }
}
