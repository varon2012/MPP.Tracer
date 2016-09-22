using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;

namespace Tracer
{
    public class Tracer : ITracer
    {
        public static Tracer Instance { get { return lazy.Value; } }
        private static readonly Lazy<Tracer> lazy = new Lazy<Tracer>(() => new Tracer());
        private Tracer()
        {
        }
        private Dictionary<int, Stack<TraceResultItem>> threadStack = new Dictionary<int, Stack<TraceResultItem>>();
        private TraceResult result { get; set; } = new TraceResult();
        private const int DistanceToCallerMethodInFrames = 2;
        private object threadLock = new object();

        public void StartTrace()
        {
            lock(threadLock)
            {
                AddTraceItem();
            }         
        }

        private void AddTraceItem()
        {
            var stackTrace = new StackTrace();                     
            TraceResultItem analyzedItem = GenerateTraceResultItem(stackTrace);
            PushItemToThreadStack(analyzedItem);            
            UpdateCallDepth(stackTrace.FrameCount);
        }

        private TraceResultItem GenerateTraceResultItem(StackTrace stackTrace)
        {
            StackFrame stackFrame = stackTrace.GetFrame(DistanceToCallerMethodInFrames);

            var method = stackFrame.GetMethod();
            var parametersAmount = method.GetParameters().Length;
            var className = method.ReflectedType.Name;
            var threadId = Thread.CurrentThread.ManagedThreadId;
            var parentMethod = stackTrace.GetFrame(DistanceToCallerMethodInFrames+1).GetMethod().Name;

            return new TraceResultItem(new Stopwatch(), className, method.Name, parametersAmount, threadId, stackTrace.FrameCount, parentMethod);
        }

        private void PushItemToThreadStack(TraceResultItem analyzedItem)
        {
            if (threadStack.ContainsKey(analyzedItem.threadId))
            {
                threadStack[analyzedItem.threadId].Push(analyzedItem);
            }
            else
            {
                threadStack.Add(analyzedItem.threadId, new Stack<TraceResultItem>());
                threadStack[analyzedItem.threadId].Push(analyzedItem);
                result.MarkThread(analyzedItem.threadId);
            }
        }

        public void StopTrace()
        {
            lock (threadLock)
            {
                TraceResultItem analyzedItem = threadStack[Thread.CurrentThread.ManagedThreadId].Pop();
                analyzedItem.StopRuntimeMeasuring();
                result.Add(analyzedItem);
            }
        }

        public TraceResult GetTraceResult()
        {
            lock (threadLock)
            {
                return result;
            }
        }

        private void UpdateCallDepth(int newDepth)
        {
            if (newDepth > result.callDepth)
                result.callDepth = newDepth;            
        }

    }
}
