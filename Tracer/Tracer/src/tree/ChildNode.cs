using System;
using System.Collections.Generic;


namespace Tracer.Tree
{
    public abstract class ChildNode : INode
    {
        public abstract void StopLastTrace(long endTime);


        private Stack<MethodNode> nestedMethods;
        protected MethodNode LastAddedMethod
        {
            get
            {
                return nestedMethods.Peek();
            }
        }

        public void AddNestedTrace(long startTime, CallerDescriptor caller)
        {
            if (NoNestedMethods() || NestedTracingsFinished())
            {
                AddNestedMethod(startTime, caller);
            }
            else
            {
                MethodNode method = LastAddedMethod;
                method.AddNestedTrace(startTime, caller);
            }
        }

        private void AddNestedMethod(long startTime, CallerDescriptor caller)
        {
            MethodNode method = new MethodNode(caller);
            method.StartTime = startTime;
            nestedMethods.Push(method);
        }

        protected Boolean NoNestedMethods()
        {
            return (nestedMethods.Count == 0);
        }

        protected Boolean NestedTracingsFinished()
        {
            return LastAddedMethod.TracingFinished;
        }
    }
}
