using System.Collections.Generic;

namespace TracerLib.Utils
{
    public class ThreadDescriptor
    {
        public Node<TracedMethodInfo> HeadNode { get; set; }

        public Stack<Node<TracedMethodInfo>> MethodStack { get; set; }

        public Node<TracedMethodInfo> CurrentNode { get; set; }

        public ThreadDescriptor(Node<TracedMethodInfo> node)
        {
            HeadNode = node;
            MethodStack = new Stack<Node<TracedMethodInfo>>();
        }

        public ThreadDescriptor()
        {

        }
    }
}