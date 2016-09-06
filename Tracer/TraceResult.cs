using System.Collections.Generic;

namespace Tracer
{
    public sealed class TraceResult
    {
        internal Dictionary<int, TraceResultHeadNode> HeadNodes { get; }

        internal TraceResult()
        {
            HeadNodes = new Dictionary<int, TraceResultHeadNode>();
        }

        internal void StartNode(int threadId,
            string className, string methodName, List<ParameterInfo> parameters)
        {
            if (!HeadNodes.ContainsKey(threadId))
            {
                HeadNodes[threadId] = new TraceResultHeadNode();
            }

            HeadNodes[threadId].StartNode(className, methodName, parameters);
        }

        internal void FinishNode(int threadId)
        {
            HeadNodes[threadId].FinishNode();
        }
    }
}
