using System;
using System.Collections.Generic;
using System.Linq;

namespace Tracer
{
    internal class ThreadNode
    {
        public int ID { get; }
        public List<MethodNode> MethodsTree;
        private int ValueOfWorkingMethods;

        public ThreadNode(int id)
        {
            ID = id;
            MethodsTree = new List<MethodNode>();
            ValueOfWorkingMethods = 0;
        }

        public void AddMethod(MethodInfo method)
        {
            if (MethodsTree != null)
            {
                ValueOfWorkingMethods++;
                MethodsTree.Add(new MethodNode(method,ValueOfWorkingMethods));
            }
            else
            {
                throw new InvalidOperationException("List of Methods is empty");
            }         
        }

        public void StopMethod(long time)
        {
            if (MethodsTree != null)
            {
                bool fixTime = false;
                int currentIndex = MethodsTree.Count() - 1;
                while (fixTime == false)
                {
                    MethodNode temp = MethodsTree.ElementAt(currentIndex);
                    if (temp.isWorking == true)
                    {
                        temp.Info.Time = time - temp.Info.Time;
                        temp.isWorking = false;
                        fixTime = true;
                    }
                    currentIndex--;
                }
                ValueOfWorkingMethods--;
            }
        }
    }
}
