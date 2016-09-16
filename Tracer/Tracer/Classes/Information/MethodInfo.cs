using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace Trace.Classes.Information
{
    public class MethodInfo
    {
        public string Name { get; }
        public string ClassName { get; }
        public int CountParameters { get; }
        public List<MethodInfo> NestedMethods { get; }
        private Stopwatch _stopwatch;

        public MethodInfo(MethodBase methodBase)
        {
            CreateStopwatch();
            Name = methodBase.Name;
            ClassName = methodBase.DeclaringType?.ToString();
            CountParameters = methodBase.GetParameters().Length;
            NestedMethods = new List<MethodInfo>();
        }

        public void AddNestedMethod(MethodInfo nestedMethod)
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
