using System.Collections.Generic;
using Tracer.Models;

namespace Tracer.Tracers
{
    public class ThreadTracer
    {
        private readonly Stack<MethodTracer> _callStack;
        private readonly ThreadTraceResult _threadTraceResult;

        public ThreadTracer(int threadId)
        {
            _threadTraceResult = new ThreadTraceResult() { Id = threadId };
            _callStack = new Stack<MethodTracer>();
            MethodTracers = new List<MethodTracer>();
        }

        public List<MethodTracer> MethodTracers { get; }

        public void StartMethodTrace(MethodTracer methodTracer)
        {
            if (_callStack.Count == 0)
            {
                MethodTracers.Add(methodTracer);
            }
            else
            {
                _callStack.Peek().AddNestedMethodTracer(methodTracer);
            }
            _callStack.Push(methodTracer);
        }

        public void StopMethodTrace()
        {
            if (_callStack.Count <= 0) return;
            MethodTracer methodTracer = _callStack.Pop();
            methodTracer.StopTrace();
            if (MethodTracers.Contains(methodTracer))
                _threadTraceResult.AddMethodTraceResult(methodTracer.GetTraceResult());
        }

        public ThreadTraceResult GetTraceResult()
        {
            return _threadTraceResult;
        }
    }
}