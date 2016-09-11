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
            TraceResultHeadNode headNode;
            if (!HeadNodes.TryGetValue(threadId, out headNode))
            {
                headNode = new TraceResultHeadNode();
                HeadNodes[threadId] = headNode;
            }

            headNode.StartNode(className, methodName, parameters);
        }

        internal void FinishNode(int threadId)
        {
            HeadNodes[threadId].FinishNode();
        }
    }
}
