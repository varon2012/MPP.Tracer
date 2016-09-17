using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace Trace.Classes.TraceInfo
{
    public class MethodTrace
    {
        public List<MethodTrace> NestedMethods { get; }
        public MethodMetadata Metadata;
        private Stopwatch _stopwatch;

        public MethodTrace(MethodBase methodBase)
        {
            CreateStopwatch();
            Metadata = new MethodMetadata(methodBase);
            NestedMethods = new List<MethodTrace>();
        }

        public void AddNestedMethod(MethodTrace nestedMethod)
        {
            NestedMethods.Add(nestedMethod);
        }

        public void StopMeteringTime()  
        {
            _stopwatch.Stop();
        }

        public long GetExecutionTime()
        {
            return _stopwatch.ElapsedMilliseconds;
        }

        private void CreateStopwatch()
        {
            _stopwatch = new Stopwatch();
            _stopwatch.Start();
        }

    }
}
