using System.Collections.Generic;

namespace Tracer
{
    public class TraceResult
    {
        public TraceResult()
        {
            HeadNodes = new Dictionary<int, TraceResultHeadNode>();
        }

        public void StartNode(int threadId,
            string className, string methodName, List<ParameterInfo> parameters)
        {
            if (!HeadNodes.ContainsKey(threadId))
            {
                HeadNodes[threadId] = new TraceResultHeadNode();
            }

            HeadNodes[threadId].StartNode(className, methodName, parameters);
        }

        public void FinishNode(int threadId)
        {
            HeadNodes[threadId].FinishNode();
        }

        public Dictionary<int, TraceResultHeadNode> HeadNodes { get; }
    }
}
