using System;
using System.Diagnostics;
using System.Threading;

namespace Trace.Classes
{
    public class Tracer : ITracer
    {
        private TraceResult traceResult;
        private object threadLock;

        public Tracer()
        {
            traceResult = new TraceResult();
            threadLock = new object();
        }

        public void StartTrace()
        {
            var stackTrace = new StackTrace(1);
            var stackFrame = stackTrace.GetFrame(0);
            var method = stackFrame.GetMethod();
            var idThread = Thread.CurrentThread.ManagedThreadId;
        }

        public void StopTrace()
        {
            throw new NotImplementedException();
        }

        public TraceResult GetTraceResult()
        {
            throw new NotImplementedException();
        }
    }
}
