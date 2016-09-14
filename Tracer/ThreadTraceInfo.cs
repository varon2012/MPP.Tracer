using System;
using System.Collections.Generic;

namespace Tracer
{
    internal sealed class ThreadTraceInfo
    {
        private readonly Stack<MethodTraceInfo> fCallStack;
        private readonly List<MethodTraceInfo> fTracedMethods;

        internal ThreadTraceInfo()
        {
            fCallStack = new Stack<MethodTraceInfo>();
            fTracedMethods = new List<MethodTraceInfo>();
        }

        internal void InitMethodTrace(MethodTraceInfo methodTraceInfo)
        {
            if (fCallStack.Count == 0)
            {
                fTracedMethods.Add(methodTraceInfo);
            }
            else
            {
                fCallStack.Peek().AddNestedCall(methodTraceInfo);
            }
            fCallStack.Push(methodTraceInfo);
        }

        internal void FinishMethodTrace()
        {
            if (fCallStack.Count == 0)
            {
                throw new InvalidOperationException("can't finish method trace: empty call stack");
            }
            fCallStack.Peek().FinishMethodTrace();
            fCallStack.Pop();
        }

        internal IEnumerable<MethodTraceInfo> TracedMethods => fTracedMethods;
    }
}