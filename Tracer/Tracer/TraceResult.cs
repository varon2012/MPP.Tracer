using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    public sealed class TraceResult
    {
        public Dictionary<int, TraceResultThreadNode> ThreadNodes { get; private set; }

        public TraceResult()
        {
            this.ThreadNodes = new Dictionary<int, TraceResultThreadNode>();
        }

        public void StartNode(int threadId, string methodName, 
            string className, int paramCount)
        {
            if (!this.ThreadNodes.Keys.Contains(threadId))
            {
                TraceResultThreadNode threadNode = new TraceResultThreadNode();
                this.ThreadNodes.Add(threadId, threadNode);
            }

            this.ThreadNodes[threadId].StartNode(methodName, className, paramCount);
        }

        public void FinishNode(int threadId)
        {
            this.ThreadNodes[threadId].FinishNode();
        }
    }
}
