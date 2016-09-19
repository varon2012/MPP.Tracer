using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading;

namespace Tracer
{
    public class TraceResult
    {
        private static readonly object LockMethodAdd = new object();

        private static readonly object LockmethodDelete = new object();

        public Dictionary<int, List<MethodsTreeNode>> TraceTree { get; set; }

        public MethodsTreeNode CurrentNode { get; set; }

        public TraceResult()
        {
            TraceTree = new Dictionary<int, List<MethodsTreeNode>>();
            CurrentNode = null;
        }

        public void AddMethodToTree(MethodBase methodBase)
        {
            lock (LockMethodAdd)
            {
                var methodNode = new MethodsTreeNode(
                    CurrentNode,
                    new MethodInfo(
                        methodBase.Name, 
                        methodBase.DeclaringType.ToString(), 
                        methodBase.GetParameters().Length,
                        Stopwatch.StartNew()));

                if (CurrentNode == null)
                    CurrentNode = methodNode;
                else
                    CurrentNode.Children.Add(methodNode);

                if (TraceTree.ContainsKey(Thread.CurrentThread.ManagedThreadId))
                    TraceTree[Thread.CurrentThread.ManagedThreadId].Add(methodNode);
                else 
                    TraceTree.Add(Thread.CurrentThread.ManagedThreadId, new List<MethodsTreeNode>());
            }
        }

        public void OutOfMethodOnTree()
        {
            lock (LockmethodDelete)
            {
                if (CurrentNode != null)
                {
                    CurrentNode.Method.Watcher.Stop();
                    CurrentNode = CurrentNode.Father;
                }
            }
        }
    }
}