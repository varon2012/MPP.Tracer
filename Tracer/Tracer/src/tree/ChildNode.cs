using System;
using System.Collections.Generic;


namespace Tracer.Tree
{
    public abstract class ChildNode : INode
    {
        public abstract void FixateCountEnd(long endTime);

        private Stack<MethodNode> methods;

        public void FixateCountStart(long startTime, CallerDescriptor caller)
        {
            if (NoNestedMethods() || AllCountsFinished())
            {
                AddMethodToList(startTime, caller);
            }
            else
            {
                MethodNode method = GetLastAddedMethod();
                method.FixateCountStart(startTime, caller);
            }
        }

        protected MethodNode GetLastAddedMethod()
        {
            return methods.Peek();
        }

        private void AddMethodToList(long startTime, CallerDescriptor caller)
        {
            MethodNode method = new MethodNode(caller);
            method.StartTime = startTime;
            methods.Push(method);
        }

        protected Boolean NoNestedMethods()
        {
            return (methods.Count == 0);
        }

        protected Boolean AllCountsFinished()
        {
            return GetLastAddedMethod().CountFinished;
        }
    }
}
