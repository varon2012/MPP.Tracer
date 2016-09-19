using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    public class MethodsTreeNode
    {
        public List<MethodsTreeNode> Children { get; set; }

        public MethodsTreeNode()
        {
            Children = new List<MethodsTreeNode>();
        }
    }
}
