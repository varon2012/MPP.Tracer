using System.Collections.Generic;

namespace TracerLib.Utils
{
    public class ThreadDescriptor
    {
        internal Node<TracedMethodInfo> HeadNode { get; set; }

        internal Stack<Node<TracedMethodInfo>> MethodStack { get; set; }

        internal Node<TracedMethodInfo> CurrentNode { get; set; }

        internal ThreadDescriptor(Node<TracedMethodInfo> node)
        {
            HeadNode = node;
            MethodStack = new Stack<Node<TracedMethodInfo>>();
        }

        internal ThreadDescriptor()
        {

        }
    }
}