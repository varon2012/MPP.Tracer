using System.Collections;
using System.Collections.Generic;

namespace MPPTracer.Tree
{
    public abstract class InternalNode : INode, IEnumerable<MethodNode>
    {
        public abstract void StopLastTrace(long endTime);
        private List<MethodNode> NestedMethods { get;}

        public InternalNode()
        {
            NestedMethods = new List<MethodNode>();
        }

        public void AddNestedTrace(long startTime, MethodDescriptor descriptor)
        {
            if (NoNestedMethods() || NestedTracingsFinished())
            {
                AddNestedMethod(startTime, descriptor);
            }
            else
            {
                MethodNode method = GetLastAddedMethod();
                method.AddNestedTrace(startTime, descriptor);
            }
        }

        private void AddNestedMethod(long startTime, MethodDescriptor descriptor)
        {
            MethodNode method = new MethodNode(startTime, descriptor);
            NestedMethods.Add(method);
        }

        protected MethodNode GetLastAddedMethod()
        {
            return NestedMethods[NestedMethods.Count - 1];
        }

        public bool NoNestedMethods()
        {
            return (NestedMethods.Count == 0);
        }

        protected bool NestedTracingsFinished()
        {
            return GetLastAddedMethod().TracingFinished;
        }

        public IEnumerator<MethodNode> GetEnumerator()
        {
            return NestedMethods.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
