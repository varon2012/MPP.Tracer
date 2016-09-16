using System;
using System.Collections.Generic;


namespace MPPTracer.Tree
{
    public abstract class ChildNode : INode
    {
        public abstract void StopLastTrace(long endTime);
        private List<MethodNode> NestedMethods { get;}
        private ChildNode ParentNode { get; }

        public ChildNode(ChildNode parent)
        {
            NestedMethods = new List<MethodNode>();
            ParentNode = parent;
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

        private bool IsLastAddedMethod(ChildNode method)
        {
            int methodIndex = NestedMethods.IndexOf((MethodNode)method);
            return (methodIndex == NestedMethods.Count - 1);
        }

        public bool IsLastAtLevel()
        {
            return ParentNode.IsLastAddedMethod(this);
        }

        public MethodNode GetFirstNestedMethod()
        {
            if (NestedMethods.Count == 0)
                return null;
            return NestedMethods[0];
        }

        public MethodNode GetNextAddedMethod()
        {
            int methodIndex = ParentNode.NestedMethods.IndexOf((MethodNode)this);
            if (methodIndex == ParentNode.NestedMethods.Count - 1)
                return null;
            return ParentNode.NestedMethods[methodIndex + 1];
        }

        private void AddNestedMethod(long startTime, MethodDescriptor descriptor)
        {
            MethodNode method = new MethodNode(descriptor, this);
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
