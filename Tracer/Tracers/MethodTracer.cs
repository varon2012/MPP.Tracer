using System.ComponentModel.Design;
using System.Diagnostics;
using System.Reflection;
using Tracer.Interfaces;
using Tracer.Models;

namespace Tracer.Tracers
{
    public class MethodTracer : ITracer
    {

        private readonly MethodTraceResult _methodTraceResult;
        private Stopwatch _timer;

        public MethodTracer(MethodBase methodBase)
        {
            _methodTraceResult = new MethodTraceResult(methodBase);
            StartTrace();
        }

        public void StartTrace()
        {
            _timer = new Stopwatch();
            _timer.Start();
        }

        public void StopTrace()
        {
            _timer.Stop();
            _methodTraceResult.Duration = _timer.ElapsedMilliseconds;
        }

        public TraceResult GetTraceResult()
        {
            return _methodTraceResult;
        }

        public void AddNestedMethodResult(MethodTraceResult methodTraceResult)
        {
            _methodTraceResult.AddNestedMethodResult(methodTraceResult);
        }
    }
}