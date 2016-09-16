using System.Diagnostics;
using System.Threading;
using Trace.Interfaces;

namespace Trace.Classes
{
    public class Tracer : ITracer
    {
        private readonly TraceResult _traceResult;
        private readonly object _threadLock;

        public Tracer()
        {
            _traceResult = new TraceResult();
            _threadLock = new object();
        }

        public void StartTrace()
        {
            var stackTrace = new StackTrace(1);
            var stackFrame = stackTrace.GetFrame(0);
            var idThread = Thread.CurrentThread.ManagedThreadId;


        }

        public void StopTrace()
        {
            
        }

        public TraceResult GetTraceResult()
        {
            return _traceResult;
        }
    }
}
