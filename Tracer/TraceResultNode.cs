using System;
using System.Collections.Generic;

namespace Tracer
{
    public struct ParameterInfo
    {
        public ParameterInfo(string name, Type type)
            : this()
        {
            Name = name;
            Type = type;
        }

        public string Name { get; }
        public Type Type { get; }
    }

    public class TraceResultNode
    {
        public TraceResultNode(string className, string methodName, List<ParameterInfo> parameters)
        {
            ClassName = className;
            MethodName = methodName;
            Parameters = parameters;

            StartTime = DateTime.Now;
            FinishTime = DateTime.Now;

            InternalNodes = new List<TraceResultNode>();
        }

        public void FinishNode()
        {
            FinishTime = DateTime.Now;
        }

        public void AddInternalNode(TraceResultNode node)
        {
            InternalNodes.Add(node);
        }

        public string ClassName { get; }
        public string MethodName { get; }
        public List<ParameterInfo> Parameters { get; }
        public DateTime StartTime { get; }
        public DateTime FinishTime { get; private set; }

        public List<TraceResultNode> InternalNodes { get; }

        public TimeSpan TracingTime => FinishTime - StartTime;
    }
}
