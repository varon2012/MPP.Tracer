using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    public class TraceResultBuilder
    {
        private const int PreviousMethods = 3;
        private const int BaseStackCapacity = 1;
        public TraceResult TraceResult;
        private Dictionary<int, Stack<TraceMethodItem>> threadsStacks = new Dictionary<int, Stack<TraceMethodItem>>();

        public void StartTrace(TraceResultItem threadItem)
        {
            Stack<TraceMethodItem> currentStack = requiredStack(threadItem);
            currentStack.Push(createMethod(currentStack));
            if (currentStack.Count == BaseStackCapacity)
                threadItem.Methods.Add(currentStack.Peek());
        }

        public void StopTrace(TraceResultItem threadItem)
        {
            Stack<TraceMethodItem> currentStack = threadsStacks[threadItem.ThreadId];
            TraceMethodItem methodItem = currentStack.Pop();
            methodItem.Measure();
            threadItem.Time += threadTime(currentStack, methodItem);
        }

        private int threadTime(Stack<TraceMethodItem> currentStack, TraceMethodItem methodItem)
        {
            return currentStack.Count == 0 ? methodItem.Time : 0;
        }

        private Stack<TraceMethodItem> requiredStack(TraceResultItem threadItem)
        {
            if (threadsStacks.ContainsKey(threadItem.ThreadId))
            {
                return threadsStacks[threadItem.ThreadId];
            }
            var tempStack = new Stack<TraceMethodItem>();
            threadsStacks.Add(threadItem.ThreadId, tempStack);
            return tempStack;

        }

        private TraceMethodItem createMethod(Stack<TraceMethodItem> currentStack)
        {
            StackTrace stackTrace = new StackTrace(true);
            StackFrame stackFrame = stackTrace.GetFrame(PreviousMethods);
            MethodInfo methodInfo = (MethodInfo)stackFrame.GetMethod();
            TraceMethodItem newMethodItem = new TraceMethodItem(methodInfo.Name, methodInfo.DeclaringType.Name, methodInfo.GetParameters().Count(), DateTime.Now);
            if (currentStack.Count > BaseStackCapacity)
                currentStack.Peek().NestedMethods.Add(newMethodItem);
            return newMethodItem;
        }

    }
}
