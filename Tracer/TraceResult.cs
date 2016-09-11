using System.Collections.Generic;

namespace Tracer
{
    public sealed class TraceResult
    {
        internal Dictionary<int, ThreadNode> ThreadNodes { get; }

        internal TraceResult()
        {
            ThreadNodes = new Dictionary<int, ThreadNode>();
        }

        internal void StartTraceMethod(int id, string className, string methodName, int paramCount)
        {
            ThreadNode node;
            if (!ThreadNodes.TryGetValue(id, out node))
            {
                node = new ThreadNode();
                ThreadNodes.Add(id, node);
            }
            node.AddMethod(className, methodName, paramCount);
        }

        internal void StopTraceMethod(int id)
        {
            ThreadNode node;
            ThreadNodes.TryGetValue(id, out node);
            node?.StopTraceMethod();
        }
    }
}