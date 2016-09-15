
using System.Collections.Generic;
using MPPTracer.Tree;
using System.Collections;
using System;

namespace MPPTracer.Format
{
    class TreeRacer
    {
        private int RootLevel { get; set; }
        public int MethodLevel { get; private set; }
        public bool NestedMethodsVisited { get; private set; }
        private MethodNode rootMethod;
        private Stack<MethodNode> wayStack;

        public TreeRacer(MethodNode rootMethod)
        {
            this.rootMethod = rootMethod;
            wayStack = new Stack<MethodNode>();
            wayStack.Push(rootMethod);
            RootLevel = 0;
            NestedMethodsVisited = false;
        }

        public MethodDescriptor getNextDescriptor()
        {
            if (StackIsEmpty())
                return null;
            MethodLevel = RootLevel;
            MethodNode method = wayStack.Peek();
            FindNextStep();

            return method.Descriptor;
        }

        private bool StackIsEmpty()
        {
            return (wayStack.Count == 0);
        }

        private void FindNextStep()
        {
            if (rootMethod.NoNestedMethods() || NestedMethodsVisited)
            {
                if (rootMethod.IsLastAtLevel())
                {
                    GoLevelUp();
                }
                else
                {
                    GoCurrentLevel();
                }
            }
            else
            {
                GoLevelDown();
            }
        }

        private void GoLevelDown()
        {
            rootMethod = rootMethod.GetFirstNestedMethod();
            wayStack.Push(rootMethod);
            RootLevel++;
            NestedMethodsVisited = false;

        }

        private void GoLevelUp()
        {
            wayStack.Pop();
            if (!StackIsEmpty())
                rootMethod = wayStack.Peek();
            else
                rootMethod = null;
            RootLevel--;
            NestedMethodsVisited = true;
            if(rootMethod != null)
            {
                if (rootMethod.IsLastAtLevel())
                {
                    GoLevelUp();
                }
                else
                {
                    GoCurrentLevel();
                }     
            }
        }

        private void GoCurrentLevel()
        {
            wayStack.Pop();
            rootMethod = rootMethod.GetNextAddedMethod();
            wayStack.Push(rootMethod);
            NestedMethodsVisited = false;
        }

    }
}
