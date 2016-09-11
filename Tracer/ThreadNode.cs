using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    public sealed class ThreadNode
    {

        internal List<MethodNode> MethodNodes { get; }

        private readonly Stack<MethodNode> _methodStack;
        private MethodNode _currentNode;

        private readonly DateTime _startTime;
        private DateTime _endTime;
        internal TimeSpan ExecutionTime => _endTime - _startTime;

        internal ThreadNode()
        {
            _startTime = DateTime.Now;
            _endTime = DateTime.Now;
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
                _currentNode.AddInnerMethod(className, methodName, paramCount);
                _methodStack.Push(node);
            }
            _currentNode = node;
        }

        internal void StopTraceMethod()
        {
            _currentNode.StopTrace();
            if (_methodStack.Count == 0)
            {
                _endTime = DateTime.Now;
                _currentNode = null;
            }
            else
            {
                _currentNode = _methodStack.Pop();
            }
        }
    }
}
