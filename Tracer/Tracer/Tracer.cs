using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Tracer
{
    public class Tracer : ITracer
    {
        private TraceResult result { get; set; }
        private Stopwatch timer { get; set; }
        private StackTrace stackTrace { get; set; }
        private StackFrame stackFrame { get; set; }

        public void StartTrace()
        {
            stackTrace = new StackTrace();
            stackFrame = stackTrace.GetFrame(1);
            
            timer = Stopwatch.StartNew();  
        }

        public void StopTrace()
        {
            // something
            timer.Stop();
        }

        public TraceResult GetTraceResult()
        {
            // something
            var method = stackFrame.GetMethod();
            var parametersAmount = method.GetParameters().Length;
            var className = method.ReflectedType.Name;
            result = new TraceResult(timer.ElapsedMilliseconds,  className, method.Name, parametersAmount);
            return result;
        }

    }
}
