using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace Tracer
{
    internal class MethodInfo
    {
        private readonly List<MethodInfo> firstLevelChildren;
        private readonly Stopwatch stopWatch;
        private readonly MethodBase methodBase;

        internal MethodInfo(MethodBase methodBaseInfo)
        {
            firstLevelChildren = new List<MethodInfo>();
            methodBase = methodBaseInfo;
            stopWatch = Stopwatch.StartNew();
        }

        internal string Name => methodBase.Name;
        internal string ClassName => methodBase.DeclaringType.Name;
        internal int ParamsCount => methodBase.GetParameters().Length;
        internal long ExecutionTime => stopWatch.ElapsedMilliseconds;
        internal IEnumerable<MethodInfo> MethodsInfo => firstLevelChildren;

        internal void AddChild(MethodInfo methodInfo)
        {
            firstLevelChildren.Add(methodInfo);
        }

        internal void StopMethodTrace()
        {
            stopWatch.Stop();
        }

        internal bool IsEquals(MethodInfo methodInfo)
        {
            return (methodBase == methodInfo.methodBase);
        }
    }
}
