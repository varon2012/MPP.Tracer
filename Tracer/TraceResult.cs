using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading;

namespace Tracer
{
    public class TraceResult
    {
        private static readonly object LockObject = new object();

        public Dictionary<int, List<MethodsTreeNode>> TraceTree { get; set; }

        private Dictionary<int, MethodsTreeNode> _currentNodes;

        public TraceResult()
        {
            TraceTree = new Dictionary<int, List<MethodsTreeNode>>();
            _currentNodes = new Dictionary<int, MethodsTreeNode>();
        }

        public void AddMethodToTree(MethodBase methodBase)
        {
            var methodNode = new MethodsTreeNode(
                null,
                new MethodInfo(
                    methodBase.Name, 
                    methodBase.DeclaringType.ToString(), 
                    methodBase.GetParameters().Length,
                    Stopwatch.StartNew()));

            if (!TraceTree.ContainsKey(Thread.CurrentThread.ManagedThreadId))
            {
                TraceTree.Add(Thread.CurrentThread.ManagedThreadId, new List<MethodsTreeNode>());
                methodNode.Father = null;
                _currentNodes.Add(Thread.CurrentThread.ManagedThreadId, null);
            }

            if (_currentNodes[Thread.CurrentThread.ManagedThreadId] == null)
            {
                TraceTree[Thread.CurrentThread.ManagedThreadId].Add(methodNode);
            }
            else
            {
                _currentNodes[Thread.CurrentThread.ManagedThreadId].Children.Add(methodNode);
                methodNode.Father = _currentNodes[Thread.CurrentThread.ManagedThreadId];
            }

            _currentNodes[Thread.CurrentThread.ManagedThreadId] = methodNode;
        }

        public void OutOfMethodOnTree()
        {
            if (_currentNodes[Thread.CurrentThread.ManagedThreadId] != null)
            {
                _currentNodes[Thread.CurrentThread.ManagedThreadId].Method.Watcher.Stop();
                _currentNodes[Thread.CurrentThread.ManagedThreadId] = _currentNodes[Thread.CurrentThread.ManagedThreadId].Father;
            }
        }
    }
}