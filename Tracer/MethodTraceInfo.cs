using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace Tracer
{
    public class MethodTraceInfo
    {
        private List<MethodTraceInfo> nestedMethodsTraceInfo;
        private Stopwatch timer;
        private MethodBase method;

        public MethodTraceInfo(MethodBase method)
        {
            this.method = method;
            nestedMethodsTraceInfo = new List<MethodTraceInfo>();
            StartTrace();
        }

        public string Name => method.Name;
        public string ClassName => method.DeclaringType.Name;
        public long Duration => timer.ElapsedMilliseconds;
        public int ArgumentsCount => method.GetParameters().Length;

        public void StopTrace()
        {
            timer.Stop();
        }

        public void AddNestedMethod(MethodTraceInfo methodTraceInfo)
        {
            nestedMethodsTraceInfo.Add(methodTraceInfo);
        }

        private void StartTrace()
        {
            timer = new Stopwatch();
            timer.Start();
        }
    }
}