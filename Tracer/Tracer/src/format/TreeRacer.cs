
using System.Collections.Generic;
using MPPTracer.Tree;

namespace MPPTracer.Format
{
    class TreeRacer
    {
        public int NestingLevel { get; private set; } = -1;
        public bool NestedMethodsVisited { get; private set;}
        private List<MethodNode> methodForest;
        public List<MethodNode> MethodForest
        {
            set
            {
                GoLevelDown(value);
            }
        }
        private Stack<MethodNode> methodStack;

        public TreeRacer()
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
            if (method.NoNestedMethods() || NestedMethodsVisited)
            {
                if (LastAtCurrentLevel(method))
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
            NestedMethodsVisited = false;
        }

        private void GoLevelUp()
        {
            methodStack.Pop();
            NestingLevel--;
            NestedMethodsVisited = true;
        }

        private void StayCurrentLevel(MethodNode method)
        {
            methodStack.Pop();
            int methodIndex = methodForest.IndexOf(method);
            methodStack.Push(methodForest[methodIndex + 1]);
            NestedMethodsVisited = false;
        }

        private bool LastAtCurrentLevel(MethodNode method)
        {
            int methodIndex = methodForest.IndexOf(method);
            return (methodIndex == methodForest.Count - 1);
        }
    }
}
