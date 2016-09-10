using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    public sealed class TraceResult
    {
        private Dictionary<int, TraceResultThreadNode> threadNodes;

        public TraceResult()
        {
            this.threadNodes = new Dictionary<int, TraceResultThreadNode>();
        }

        public void StartNode(int threadId, string methodName, 
            string className, int paramCount)
        {
            if (!this.threadNodes.Keys.Contains(threadId))
            {
                TraceResultThreadNode threadNode = new TraceResultThreadNode();
                this.threadNodes.Add(threadId, threadNode);
            }

            this.threadNodes[threadId].StartNode(methodName, className, paramCount);
        }

        public void FinishNode(int threadId)
        {
            this.threadNodes[threadId].FinishNode();
        }
    }
}
