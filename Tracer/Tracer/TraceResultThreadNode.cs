using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    public sealed class TraceResultThreadNode
    {
        private TraceResultMethodNode currentMethodNode;

        private Stack<TraceResultMethodNode> methodNodesStack;
        public List<TraceResultMethodNode> MethodNodesList { get; private set; }

        public TraceResultThreadNode()
        {
            this.methodNodesStack = new Stack<TraceResultMethodNode>();
            this.MethodNodesList = new List<TraceResultMethodNode>();

            this.currentMethodNode = null;
        }

        public void StartNode(string methodName,
            string className, int paramCount)
        {
            TraceResultMethodNode newNode = new 
                TraceResultMethodNode(methodName, className, paramCount);

            if (this.currentMethodNode == null)
                this.MethodNodesList.Add(newNode);
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
