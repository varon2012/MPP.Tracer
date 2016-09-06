using System.Collections.Generic;

namespace Tracer
{
    internal sealed class TraceResultHeadNode
    {
        private TraceResultNode _currentNode;
        private readonly Stack<TraceResultNode> _nodesStack;

        internal List<TraceResultNode> TopLevelNodes { get; }

        internal TraceResultHeadNode()
        {
            TopLevelNodes = new List<TraceResultNode>();

            _currentNode = null;
            _nodesStack = new Stack<TraceResultNode>();
        }

        internal void StartNode(string className, string methodName, List<ParameterInfo> parameters)
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

        internal void FinishNode()
        {
            _currentNode.FinishNode();
            _currentNode = (_nodesStack.Count > 0) ? _nodesStack.Pop() : null;
        }
    }
}
