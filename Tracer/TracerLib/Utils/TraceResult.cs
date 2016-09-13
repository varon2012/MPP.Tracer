using System.Collections.Generic;
using System.Linq;

namespace TracerLib.Utils
{
    public class TraceResult
    {
        public Dictionary<int, ThreadDescriptor> Threads { get; set; }

        private readonly object _pushMethodSync;
        private readonly object _popMethodSync;

        public TraceResult()
        {
            Threads = new Dictionary<int, ThreadDescriptor>();
            _pushMethodSync = new object();
            _popMethodSync = new object();
        }

        public void AddNode(TracedMethodInfo obj)
        {
            lock (_pushMethodSync)
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

        public void PopNodeFromStack(int threadId)
        {
            lock (_popMethodSync)
            {
                var wrapper = Threads[threadId];

                var removedMethod = wrapper.MethodStack.Pop();
                removedMethod.Item.Watcher.Stop();

                wrapper.CurrentNode = (wrapper.MethodStack.Count > 0) ? wrapper.MethodStack.Peek() : null;
            }
        }
    }
}