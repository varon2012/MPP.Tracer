﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Tracer
{
    internal class ThreadInfo
    {
        
        private readonly Stack<MethodInfo> methodsCallStack;
        private readonly List<MethodInfo> firstNestingLevelTracedMethods;

        internal ThreadInfo()
        {
            firstNestingLevelTracedMethods = new List<MethodInfo>();
            methodsCallStack = new Stack<MethodInfo>();
        }

        internal long ExecutionTime => firstNestingLevelTracedMethods.Sum((method) => method.ExecutionTime);

        internal IEnumerable<MethodInfo> MethodsInfo => firstNestingLevelTracedMethods;

        internal void StartMethodTrace(MethodInfo methodInfo)
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

        internal void StopMethodTrace(MethodInfo methodInfo)
        {
            if (methodsCallStack.Count == 0)
            {
                throw new InvalidOperationException("There are no MethodsInfo in stack. Maybe you have called StopTrace twice");
            }

            if (!methodsCallStack.Peek().IsEquals(methodInfo))
            {
                throw new ArgumentException("StartTrace and StopTrace can't be called from different methods");
            }

            MethodInfo lastAddedMethod = methodsCallStack.Pop();
            lastAddedMethod.StopMethodTrace();
        }
    }
}