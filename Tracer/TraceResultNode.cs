using System;
using System.Collections.Generic;

namespace Tracer
{
    internal struct ParameterInfo
    {
        internal ParameterInfo(string name, Type type)
            : this()
        {
            Name = name;
            Type = type;
        }

        internal string Name { get; }
        internal Type Type { get; }
    }

    internal sealed class TraceResultNode
    {
        internal TraceResultNode(string className, string methodName, List<ParameterInfo> parameters)
        {
            ClassName = className;
            MethodName = methodName;
            Parameters = parameters;

            StartTime = DateTime.Now;
            FinishTime = DateTime.Now;

            InternalNodes = new List<TraceResultNode>();
        }

        internal void FinishNode()
        {
            FinishTime = DateTime.Now;
        }

        internal void AddInternalNode(TraceResultNode node)
        {
            InternalNodes.Add(node);
        }

        internal string ClassName { get; }
        internal string MethodName { get; }
        internal List<ParameterInfo> Parameters { get; }
        internal DateTime StartTime { get; }
        internal DateTime FinishTime { get; private set; }

        internal List<TraceResultNode> InternalNodes { get; }

        internal TimeSpan TracingTime => FinishTime - StartTime;
    }
}
