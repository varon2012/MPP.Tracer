using System.Collections.Generic;
using System.Linq;

namespace Tracer
{
    public class ThreadTraceInfo
    {
        private readonly Stack<MethodTraceInfo> callStack;

        public ThreadTraceInfo()
        {
            callStack = new Stack<MethodTraceInfo>();
            MethodsTraceInfo = new List<MethodTraceInfo>();
        }

        public long Duration => MethodsTraceInfo.Sum(methodTraceInfo => methodTraceInfo.Duration);
        public List<MethodTraceInfo> MethodsTraceInfo { get; }

        public void StartMethodTrace(MethodTraceInfo methodTraceInfo)
        {
            if (callStack.Count == 0)
                MethodsTraceInfo.Add(methodTraceInfo);
            else
                callStack.Peek().AddNestedMethod(methodTraceInfo);
            callStack.Push(methodTraceInfo);
        }

        public void StopMethodTrace()
        {
            if (callStack.Count <= 0) return;
            callStack.Peek().StopTrace();
            callStack.Pop();
        }
    }
}