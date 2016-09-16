using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Reflection;
using System.Threading;

namespace Tracer
{
    public sealed class Tracer : ITracer
    {
        private static volatile Tracer instance = null;
        private static readonly object syncRoot = new object();

        private TraceResult traceResult;

        private Tracer()
        {
            this.traceResult = new TraceResult();   
        }

        public void StartTrace()
        {
            StackTrace stackTrace = new StackTrace();
            StackFrame stackFrame = stackTrace.GetFrame(1);
            MethodBase method = stackFrame.GetMethod();

            string methodName = method.Name;
            string className = method.DeclaringType.ToString();
            int paramCount = method.GetParameters().Length;

            int threadId = Thread.CurrentThread.ManagedThreadId;

            lock (syncRoot)
            {
                this.traceResult.StartNode(threadId, methodName, className, paramCount);
            }
        }

        public void StopTrace()
        {
            int threadId = Thread.CurrentThread.ManagedThreadId;

            lock (syncRoot)
            {
                this.traceResult.FinishNode(threadId);
            }
        }

        public TraceResult GetTraceResult()
        {
            return this.traceResult;
        }

        public static Tracer Instance()
        {
            if (instance == null)
            {
                lock (syncRoot)
                {
                    if (instance == null)
                    {
                        instance = new Tracer();
                    }
                }                    
            }               

            return instance;
        }
    }
}
