
using System.Collections.Generic;
using MPPTracer.Tree;
using System.Collections;
using System;

namespace MPPTracer.Format
{
    class TreeEnumerator : IEnumerator<KeyValuePair<int,MethodDescriptor>>
    {
        private int RootLevel { get; set; }
        private int MethodLevel { get; set; }
        private bool NestedMethodsVisited { get; set; }
        private MethodNode rootMethod;
        private MethodDescriptor currentDescriptor;
        private Stack<MethodNode> wayStack;

        public KeyValuePair<int, MethodDescriptor> Current
        {
            get
            {
                return new KeyValuePair<int, MethodDescriptor>(MethodLevel, currentDescriptor);
            }
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }


        public TreeEnumerator(MethodNode rootMethod)
        {
            this.rootMethod = rootMethod;
            wayStack = new Stack<MethodNode>();
            wayStack.Push(rootMethod);
            RootLevel = 0;
            NestedMethodsVisited = false;
        }

        public bool MoveNext()
        {
            currentDescriptor = getNextDescriptor();
            return (currentDescriptor != null);
        }

        private MethodDescriptor getNextDescriptor()
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
            RootLevel--;
            NestedMethodsVisited = true;

            if(!StackIsEmpty())
            {
                rootMethod = wayStack.Peek();
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

        public void Dispose()
        {
            
        }

        public void Reset()
        {
            
        }
    }
}
