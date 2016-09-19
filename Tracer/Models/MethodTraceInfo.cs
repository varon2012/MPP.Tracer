using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace Tracer.Models
{
    public class MethodTraceInfo
    {
        private Stopwatch _timer;
        private readonly MethodBase _method;

        public MethodTraceInfo(MethodBase method)
        {
            _method = method;
            NestedMethodsTraceInfo = new List<MethodTraceInfo>();
            StartTrace();
        }

        public string Name => _method.Name;
        public string ClassName => _method.DeclaringType.Name;
        public long Duration => _timer.ElapsedMilliseconds;
        public int ArgumentsCount => _method.GetParameters().Length;
        public List<MethodTraceInfo> NestedMethodsTraceInfo { get; }

        public void StopTrace()
        {
            _timer.Stop();
        }

        public void AddNestedMethod(MethodTraceInfo methodTraceInfo)
        {
            NestedMethodsTraceInfo.Add(methodTraceInfo);
        }

        private void StartTrace()
        {
            _timer = new Stopwatch();
            _timer.Start();
        }
    }
}