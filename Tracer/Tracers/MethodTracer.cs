using System.Diagnostics;
using System.Reflection;
using Tracer.Models;

namespace Tracer.Tracers
{
    public class MethodTracer
    {
        private readonly MethodTraceResult _methodTraceResult;
        private Stopwatch _timer;

        public MethodTracer(MethodBase methodBase)
        {
            _methodTraceResult = new MethodTraceResult()
            {
                Name = methodBase.Name,
                ClassName = methodBase.DeclaringType?.Name,
                ArgumentsCount = methodBase.GetParameters().Length
            };
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

        public MethodTraceResult GetTraceResult()
        {
            return _methodTraceResult;
        }

        public void AddNestedMethodTracer(MethodTracer methodTracer)
        {
            _methodTraceResult.AddNestedMethodResult(methodTracer.GetTraceResult());
        }
    }
}