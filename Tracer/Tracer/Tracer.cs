using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Reflection;

namespace Tracer
{
    public class Tracer : ITracer
    {
        private static volatile Tracer instance = null;
        private static readonly object syncRoot = new object();

        private TraceResult traceResult;

        private Tracer()
        {
            traceResult = new TraceResult();   
        }

        public void StartTrace()
        {
            StackTrace stackTrace = new StackTrace();
            StackFrame stackFrame = stackTrace.GetFrame(1);
            MethodBase method = stackFrame.GetMethod();

            string methodName = method.Name;
            Type className = method.DeclaringType;
            int paramCount = method.GetParameters().Length;

            lock (syncRoot)
            {
                
            }
        }

        public void StopTrace()
        {
            lock (syncRoot)
            {

            }
        }

        public TraceResult GetTraceResult()
        {
            return traceResult;
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
