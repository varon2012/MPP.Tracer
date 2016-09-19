using System;
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
        private readonly MethodBase methodBase;

        internal MethodInfo(MethodBase methodBaseInfo)
        {
            firstLevelChildren = new List<MethodInfo>();
            methodBase = methodBaseInfo;
            stopWatch = Stopwatch.StartNew();
        }

        public string Name => methodBase.Name;
        public string ClassName => methodBase.DeclaringType.Name;
        public int ParamsCount => methodBase.GetParameters().Length;
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

        internal bool IsEquals(MethodInfo methodInfo)
        {
            return (methodBase == methodInfo.methodBase);
        }
    }
}
