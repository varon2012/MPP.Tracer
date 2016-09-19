using System.Collections.Generic;

namespace Tracer
{
    public class MethodsTreeNode
    {
        public List<MethodsTreeNode> Children { get; set; }

        public MethodsTreeNode Father { get; set; }

        public MethodInfo Method { get; set; }

        public MethodsTreeNode(MethodsTreeNode father, MethodInfo method)
        {
            Father = father;
            Children = new List<MethodsTreeNode>();
            Method = method;
        }

        public void AddChild(MethodsTreeNode child)
        {
            Children.Add(child);
        }
    }
}
