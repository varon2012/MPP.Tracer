using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using Tracer.Interfaces;
using Tracer.Models;

namespace Tracer.Tracers
{
    public class Tracer : ITracer
    {
        private readonly ConcurrentDictionary<int, ThreadTracer> _threadTracers;

        private Tracer()
        {
           _threadTracers = new ConcurrentDictionary<int, ThreadTracer>();
        }

        public static Tracer Instance { get; } = new Tracer();

        public void StartTrace()
        {
            int currentThreadId = Thread.CurrentThread.ManagedThreadId;
            MethodBase currentMethod = new StackTrace().GetFrame(1).GetMethod();
            ThreadTracer threadTracer = _threadTracers.GetOrAdd(currentThreadId, new ThreadTracer(currentThreadId));
            threadTracer.StartMethodTrace(new MethodTracer(currentMethod));
        }

        public void StopTrace()
        {
            var currentThreadId = Thread.CurrentThread.ManagedThreadId;
            _threadTracers[currentThreadId].StopMethodTrace();
        }

        public TraceResult GetTraceResult()
        {
            var traceResult = new TraceResult();           
            foreach (KeyValuePair<int, ThreadTracer> threadTracerInfo in _threadTracers)
            {
                traceResult.ThreadsTraceResult.Add(threadTracerInfo.Value.GetTraceResult());
            }
            return traceResult;
        }
    }
}


