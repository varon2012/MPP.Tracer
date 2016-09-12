using System;
using System.Collections.Generic;


namespace MPPTracer.Tree
{
    public abstract class ChildNode : INode
    {
        public abstract void StopLastTrace(long endTime);


        internal List<MethodNode> NestedMethods { get; private set; }

        public ChildNode()
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
            MethodNode method = new MethodNode(descriptor);
            method.StartTime = startTime;
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
    }
}
