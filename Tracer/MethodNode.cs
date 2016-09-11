using System;
using System.Collections.Generic;

namespace Tracer
{
    public sealed class MethodNode
    {
        public string MethodName { get; }
        public string ClassName { get; }
        private readonly DateTime _startTime;
        private DateTime _endTime;
        public TimeSpan ExecutionTime => _endTime - _startTime;
        internal int ParameterCount { get; }

        internal List<MethodNode> InnerMethods { get; }

        internal MethodNode(string className, string methodName, int paramCount)
        {
            InnerMethods = new List<MethodNode>();
            MethodName = methodName;
            ClassName = className;
            ParameterCount = paramCount;
            _startTime = DateTime.Now;
            _endTime = DateTime.Now;
        }

        internal void StopTrace()
        {
            _endTime = DateTime.Now;
        }

        internal void AddInnerMethod(string className, string methodName, int paramCount)
        {
            InnerMethods.Add(new MethodNode(className, methodName, paramCount));
        }
    }
}