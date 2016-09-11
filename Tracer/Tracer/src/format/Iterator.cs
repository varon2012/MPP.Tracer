
using System.Collections.Generic;
using Tracer.Tree;

namespace Tracer.Format
{
    class Iterator
    {
        public int NestingLevel { get; private set; } = -1;
        public bool AllChildVisited { get; private set;}
        private List<MethodNode> methodForest;
        public List<MethodNode> MethodForest
        {
            set
            {
                GoLevelDown(value);
            }
        }
        private Stack<MethodNode> methodStack;

        public Iterator()
        {
            methodStack = new Stack<MethodNode>();
        }

        public MethodDescriptor getNextDescriptor()
        {
            if (StackIsEmpty())
                return null;

            MethodNode method = methodStack.Peek();
            FindNextStep(method);

            return method.Descriptor;
        }

        private bool StackIsEmpty()
        {
            return (methodStack.Count == 0);
        }

        private void FindNextStep(MethodNode method)
        {
            if (method.NoNestedMethods() || AllChildVisited)
            {
                if (IsLastInList(method))
                    GoLevelUp();
                else
                    StayCurrentLevel(method);
            }
            else
            {
                GoLevelDown(method.NestedMethods);
            }
        }

        private void GoLevelDown(List<MethodNode> methodForest)
        {
            this.methodForest = methodForest;
            methodStack.Push(this.methodForest[0]);
            NestingLevel++;
            AllChildVisited = false;
        }

        private void GoLevelUp()
        {
            methodStack.Pop();
            NestingLevel--;
            AllChildVisited = true;
        }

        private void StayCurrentLevel(MethodNode method)
        {
            int methodIndex = methodForest.IndexOf(method);
            methodStack.Push(methodForest[methodIndex + 1]);
        }

        private bool IsLastInList(MethodNode method)
        {
            int methodIndex = methodForest.IndexOf(method);
            return (methodIndex < methodForest.Count - 1);
        }
    }
}
