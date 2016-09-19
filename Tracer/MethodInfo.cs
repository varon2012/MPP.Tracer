using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace Tracer
{
    public class MethodInfo
    {
        private readonly List<MethodInfo> firstLevelChildren;
        private readonly Stopwatch stopWatch;

        public MethodInfo(MethodBase methodBaseInfo)
        {
            firstLevelChildren = new List<MethodInfo>();

            Name = methodBaseInfo.Name;
            ClassName = methodBaseInfo.DeclaringType.Name;
            ParamsCount = methodBaseInfo.GetParameters().Length;
            stopWatch = Stopwatch.StartNew();
        }

        public string Name { get; private set; }
        public string ClassName { get; private set; }
        public int ParamsCount { get; private set; }
        public long ExecutionTime => stopWatch.ElapsedMilliseconds;

        public IEnumerable MethodsInfo => firstLevelChildren;

        internal void AddChild(MethodInfo methodInfo)
        {
            firstLevelChildren.Add(methodInfo);
        }

        internal void StopMethodTrace()
        {
            stopWatch.Stop();
        }
    }
}
