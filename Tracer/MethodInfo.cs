using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    public class MethodInfo
    {
        private List<MethodInfo> firstLevelChildren;
        private Stopwatch stopwatch;

        public MethodInfo(MethodBase methodBaseInfo)
        {
            firstLevelChildren = new List<MethodInfo>();

            Name = methodBaseInfo.Name;
            ClassName = methodBaseInfo.DeclaringType.Name;
            ParamsCount = methodBaseInfo.GetParameters().Length;
            stopwatch = Stopwatch.StartNew();
        }

        public string Name { get; private set; }
        public string ClassName { get; private set; }
        public int ParamsCount { get; private set; }
        public long ExecutionTime => stopwatch.ElapsedMilliseconds;

        public void AddChild(MethodInfo methodInfo)
        {
            firstLevelChildren.Add(methodInfo);
        }

        public void StopMethodTrace()
        {
            stopwatch.Stop();
        }
    }
}
