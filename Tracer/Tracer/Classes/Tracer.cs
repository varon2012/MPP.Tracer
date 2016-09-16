using System.Diagnostics;
using System.Threading;
using Trace.Interfaces;

namespace Trace.Classes
{
    public class Tracer : ITracer
    {
        private readonly TraceResult _traceResult;

        public Tracer()
        {
            _traceResult = new TraceResult();
        }

        public void StartTrace()
        {
            var method = new StackTrace(1).GetFrame(0).GetMethod();
            var idThread = Thread.CurrentThread.ManagedThreadId;

            _traceResult.StartListenThread(idThread, method);
        }

        public void StopTrace()
        {
            var idThread = Thread.CurrentThread.ManagedThreadId;
            _traceResult.StopListenThread(idThread);
        }

        public TraceResult GetTraceResult()
        {
            return _traceResult;
        }
    }
}
