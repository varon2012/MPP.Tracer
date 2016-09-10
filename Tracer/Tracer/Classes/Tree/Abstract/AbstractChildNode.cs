using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer.Classes.Tree
{
    abstract class AbstractChildNode : AbstractNode<MethodNode>
    {
        private Stack<MethodNode> methods;
        internal Boolean NoNestedMethods
        {
            get
            {
                return (methods.Count == 0);
            }
        }

        internal override void FixateCountStart(long startTime, CallerDescriptor caller)
        {
            MethodNode method = GetLastAddedMethod();
            if (method.CountFinished)
            {
                method = new MethodNode(caller);
                method.StartTime = startTime;
                AddMethodToList(method);
            }
            else
            {
                method.FixateCountStart(startTime, caller);
            }
        }

        internal override void FixateCountEnd(long endTime)
        {
            MethodNode method = GetLastAddedMethod();
            if (method.NoNestedMethods)
            {
                method.EndTime = endTime;
            }
        }

        private MethodNode GetLastAddedMethod()
        {
            return methods.Peek();
        }

        private void AddMethodToList(MethodNode method)
        {
            methods.Push(method);
        }

    }
}
