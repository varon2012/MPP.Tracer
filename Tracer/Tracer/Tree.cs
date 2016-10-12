using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    public class Tree<T>
    {
        public List<Tree<T>> NodesList { get; set; }
        public T Node { get; private set; }
        public Tree<T> PreviousNode { get; set; }
        public Tree(T node, Tree<T> previousNode)
        {
            Node = node;
            PreviousNode = previousNode;
            NodesList = new List<Tree<T>>();
        }
    }
}
