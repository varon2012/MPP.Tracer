using System.Collections.Generic;

namespace Tracer
{
    public sealed class TraceResultHeadNode
    {
        public TraceResultHeadNode()
        {
            TopLevelNodes = new List<TraceResultNode>();

            _currentNode = null;
            _nodesStack = new Stack<TraceResultNode>();
        }

        public void StartNode(string className, string methodName, List<ParameterInfo> parameters)
        {
            var node = new TraceResultNode(className, methodName, parameters);

            if (_currentNode != null)
            {
                _currentNode.AddInternalNode(node);
                _nodesStack.Push(_currentNode);
            }
            else
            {
                TopLevelNodes.Add(node);
            }

            _currentNode = node;
        }

        public void FinishNode()
        {
            _currentNode.FinishNode();
            _currentNode = (_nodesStack.Count > 0) ? _nodesStack.Pop() : null;
        }

        public List<TraceResultNode> TopLevelNodes { get; }

        private TraceResultNode _currentNode;
        private readonly Stack<TraceResultNode> _nodesStack;
    }
}
