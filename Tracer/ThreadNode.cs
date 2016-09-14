using System;
using System.Collections.Generic;

namespace Tracer
{
    public sealed class ThreadNode
    {
        public List<MethodNode> MethodNodes { get; }

        private readonly Stack<MethodNode> _methodStack;
        private MethodNode _currentNode;

        private readonly DateTime _startTime;
        private DateTime _endTime;
        public TimeSpan ExecutionTime => _endTime - _startTime;

        internal ThreadNode()
        {
            _startTime = DateTime.UtcNow;
            _endTime = DateTime.UtcNow;
            MethodNodes = new List<MethodNode>();
            _methodStack = new Stack<MethodNode>();
            _currentNode = null;
        }

        internal void AddMethod(string className, string methodName, int paramCount)
        {
            MethodNode node = new MethodNode(className, methodName, paramCount);
            if (_currentNode == null)
            {
                MethodNodes.Add(node);
            }
            else
            {
                _currentNode.AddInnerMethod(node);
                _methodStack.Push(_currentNode);
            }
            _currentNode = node;
        }

        internal void StopTraceMethod()
        {
            _currentNode.RegisterMethodEnd();
            if (_methodStack.Count == 0)
            {
                _endTime = DateTime.UtcNow;
                _currentNode = null;
            }
            else
            {
                _currentNode = _methodStack.Pop();
            }
        }
    }
}