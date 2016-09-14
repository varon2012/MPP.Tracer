using System;
using System.Diagnostics;
using System.Reflection;
using System.Threading;

namespace Tracer
{
    public class Tracer : ITracer
    {
        private readonly TraceResult fTraceResult;

        private static volatile Tracer _tracerInstance;
        private static readonly object SyncRoot = new object();

        public void StartTrace()
        {
            var stackTrace = new StackTrace(1);
            StackFrame currentFrame = stackTrace.GetFrame(0);
            MethodBase currentMethod = currentFrame.GetMethod();

            fTraceResult.InitMethodTrace(Thread.CurrentThread.ManagedThreadId, currentMethod);
        }

        public void StopTrace()
        {
            fTraceResult.FinishMethodTrace(Thread.CurrentThread.ManagedThreadId);
        }

        public TraceResult GetTraceResult()
        {
            return fTraceResult;
        }

        // Static

        public static Tracer GetInstance()
        {
            if (_tracerInstance == null)
            {
                lock (SyncRoot)
                {
                    if (_tracerInstance == null)
                    {
                        _tracerInstance = new Tracer();
                    }
                }
            }

            return _tracerInstance;
        }

        // Internals

        private Tracer()
        {
            fTraceResult = new TraceResult();
        }
    }
}
