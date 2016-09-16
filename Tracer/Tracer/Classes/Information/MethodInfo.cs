using System.Collections.Generic;
using System.Diagnostics;

namespace Trace.Classes.Information
{
    internal class MethodInfo
    {
        private List<MethodInfo> _nestedMethods;
        private Stopwatch _stopwatch;
        public string Name { get; }
        public string ClassName { get; }
        public int CountParameters { get; }

        public MethodInfo(string name, string className, int countParameters)
        {
            CreateStopWatch();
            Name = name;
            ClassName = className;
            CountParameters = countParameters;
            _nestedMethods = new List<MethodInfo>();
        }

        private void CreateStopWatch()
        {
            _stopwatch = new Stopwatch();
            _stopwatch.Start();
        }
    }
}
