using System.Collections.Concurrent;
using System.Diagnostics;
using System.Reflection;
using System.Threading;
using Tracer.Interfaces;
using Tracer.Tracers;

namespace Tracer
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
            ThreadTracer threadTracer = _threadTracers.GetOrAdd(currentThreadId, new ThreadTracer());
            threadTracer.StartMethodTrace(new MethodTracer(currentMethod));
        }

        public void StopTrace()
        {
            var currentThreadId = Thread.CurrentThread.ManagedThreadId;
            _threadTracers[currentThreadId].StopMethodTrace();
        }

        public TraceResult GetTraceResult()
        {
            return new TraceResult(_threadTracers);
        }
    }
}


