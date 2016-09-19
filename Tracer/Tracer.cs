using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Tracer
{
    public class Tracer : ITracer
    {
        private static volatile Tracer instance;
        private static object syncRoot = new object();

        private TraceResult traceResult;
        private Tracer()
        {
            traceResult = new TraceResult();
        }

        public static Tracer Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new Tracer();
                    }
                }

                return instance;
            }
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

        public TraceResult GetTraceResult()
        {
            return traceResult;
        }


    }
}
