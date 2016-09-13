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

        public static Tracer Instance { get { return lazy.Value; } }
        private static readonly Lazy<Tracer> lazy = new Lazy<Tracer>(() => new Tracer());
        private Tracer()
        {
        }

        private TraceResult result { get; set; } = new TraceResult();
        private StackTrace stackTrace { get; set; }
        private StackFrame stackFrame { get; set; }

        public void StartTrace()
        {
            AddTraceItem();            
            
        }

        public void StopTrace()
        {
            stackTrace = new StackTrace();
            stackFrame = stackTrace.GetFrame(1);
            string currentMethodName = stackFrame.GetMethod().Name;
            TraceResultItem analyzedItem = result.Find(currentMethodName);
            analyzedItem.StopRuntimeMeasuring();
        }

        public TraceResult GetTraceResult()
        {
            return result;
        }

        private void AddTraceItem()
        {
            stackTrace = new StackTrace();
            stackFrame = stackTrace.GetFrame(2);

            var method = stackFrame.GetMethod();
            var parametersAmount = method.GetParameters().Length;
            var className = method.ReflectedType.Name;

            var newAnalyzedItem = new TraceResultItem(new Stopwatch(), className, method.Name, parametersAmount);
            result.Add(newAnalyzedItem);
            
        }

    }
}
