using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    public static class TraceResultBuilder
    {
        private const int PREVIOUS_METHODS = 3;
        private const int BASE_STACK_CAPACITY = 1;
        public static TraceResult TraceResult;
        private static Dictionary<int, Stack<TraceMethodItem>> threadsStacks = new Dictionary<int, Stack<TraceMethodItem>>();

        public static void StartTrace(TraceResultItem threadItem)
        {
            Stack<TraceMethodItem> currentStack = requiredStack(threadItem);
            currentStack.Push(createMethod(currentStack));
            if (currentStack.Count == 1)
                threadItem.Methods.Add(currentStack.Peek());
        }

        public static void StopTrace(TraceResultItem threadItem)
        {
            Stack<TraceMethodItem> currentStack = threadsStacks[threadItem.ThreadId];
            TraceMethodItem methodItem = currentStack.Pop();
            methodItem.Measure();
            threadItem.Time += threadTime(currentStack, methodItem);
        }

        private static int threadTime(Stack<TraceMethodItem> currentStack, TraceMethodItem methodItem)
        {
            if (currentStack.Count == 0)
                return methodItem.Time;
            else
                return 0;
        }

        private static Stack<TraceMethodItem> requiredStack(TraceResultItem threadItem)
        {
            if (threadsStacks.ContainsKey(threadItem.ThreadId))
                return threadsStacks[threadItem.ThreadId];
            else
            {
                var tempStack = new Stack<TraceMethodItem>();
                threadsStacks.Add(threadItem.ThreadId, tempStack);
                return tempStack;
            }

        }

        private static TraceMethodItem createMethod(Stack<TraceMethodItem> currentStack)
        {
            StackTrace stackTrace = new StackTrace(true);
            StackFrame stackFrame = stackTrace.GetFrame(PREVIOUS_METHODS);
            MethodInfo methodInfo = (MethodInfo)stackFrame.GetMethod();
            TraceMethodItem newMethodItem = new TraceMethodItem(methodInfo.Name, methodInfo.DeclaringType.Name, methodInfo.GetParameters().Count(), DateTime.Now);
            if (currentStack.Count > BASE_STACK_CAPACITY)
                currentStack.Peek().NestedMethods.Add(newMethodItem);
            return newMethodItem;
        }

    }
}
