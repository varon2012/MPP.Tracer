using System.Collections.Generic;
using System.Linq;

namespace TracerLib.Utils
{
    internal class IntermediateTraceResult
    {
        public Dictionary<int, ThreadDescriptor> Threads { get; set; }

        private readonly object syncObj;

        public IntermediateTraceResult()
        {
            Threads = new Dictionary<int, ThreadDescriptor>();
            syncObj = new object();
        }

        internal void AddNode(TracedMethodInfo obj)
        {
            lock (syncObj)
            {
                var decs = new ThreadDescriptor();
                var newNode = new Node<TracedMethodInfo>(obj);

                if (!Threads.TryGetValue(obj.ThreadId, out decs))
                {
                    Threads.Add(obj.ThreadId, new ThreadDescriptor(newNode));
                }

                var threadDesc = Threads[obj.ThreadId];
                if (threadDesc.CurrentNode == null)
                {
                    threadDesc.CurrentNode = newNode;
                    threadDesc.MethodStack.Push(newNode);
                }
                else
                {
                    threadDesc.CurrentNode.AddChild(newNode);
                    threadDesc.MethodStack.Push(newNode);
                    threadDesc.CurrentNode = threadDesc.CurrentNode.Children.Last();
                }
            }
        }

        internal void PopNodeFromStack(int threadId)
        {
            lock (syncObj)
            {
                var wrapper = Threads[threadId];

                var removedMethod = wrapper.MethodStack.Pop();
                removedMethod.Item.Watcher.Stop();

                wrapper.CurrentNode = (wrapper.MethodStack.Count > 0) ? wrapper.MethodStack.Peek() : null;
            }
        }
    }
}