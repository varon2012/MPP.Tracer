using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    internal sealed class TraceResultThreadNode
    {
        private Stack<TraceResultMethodNode> methodNodesStack;
        private List<TraceResultMethodNode> methodNodesList;

        private TraceResultMethodNode currentMethodNode;

        public TraceResultThreadNode()
        {
            this.methodNodesStack = new Stack<TraceResultMethodNode>();
            this.methodNodesList = new List<TraceResultMethodNode>();

            this.currentMethodNode = null;
        }

        public void StartNode(string methodName,
            string className, int paramCount)
        {
            TraceResultMethodNode newNode = new 
                TraceResultMethodNode(methodName, className, paramCount);

            if (this.currentMethodNode == null)
                this.methodNodesList.Add(newNode);
            else
                this.currentMethodNode.AddInsertedNode(newNode);
            this.methodNodesStack.Push(newNode);

            newNode.StartNode();

            this.currentMethodNode = newNode;
        }

        public void FinishNode()
        {
            TraceResultMethodNode finishNode = this.methodNodesStack.Pop();
            finishNode.FinishNode();

            if (this.methodNodesStack.Count > 0)
                this.currentMethodNode = this.methodNodesStack.Peek();
            else
                this.currentMethodNode = null;
        }
    }
}
