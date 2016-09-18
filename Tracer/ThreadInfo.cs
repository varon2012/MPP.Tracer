using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    public class ThreadInfo
    {
        
        private Stack<MethodInfo> methodsCallStack;
        private List<MethodInfo> firstNestingLevelTracedMethods;

        public ThreadInfo()
        {
            firstNestingLevelTracedMethods = new List<MethodInfo>();
            methodsCallStack = new Stack<MethodInfo>();
        }

        public long ExecutionTime => firstNestingLevelTracedMethods.Sum((method) => method.ExecutionTime);

        public void StartMethodTrace(MethodInfo methodInfo)
        {
            if (methodsCallStack.Count == 0)
            {
                firstNestingLevelTracedMethods.Add(methodInfo);
            }
            else
            {
                MethodInfo lastAddedMethod = methodsCallStack.Peek();
                lastAddedMethod.AddChild(methodInfo);
            }

            methodsCallStack.Push(methodInfo);
        }

        public void StopMethodTrace(MethodInfo methodInfo)
        {
            if (methodsCallStack.Count == 0)
            {
                throw new Exception("There are no MethodsInfo in stack. Maybe you have called StopTrace twice");
            }

            MethodInfo lastAddedMethod = methodsCallStack.Pop();
            lastAddedMethod.StopMethodTrace();
        }
    }
}
