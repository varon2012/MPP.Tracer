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
        private struct currentFrame
        {
            StackFrame Instance;
            int depth;
        } 
        private Tracer()
        {
        }

        private TraceResult result { get; set; } = new TraceResult();
        private StackTrace stackTrace { get; set; }
        private KeyValuePair<int, StackFrame> stackFrame { get; set; }

        private object threadLock = new object();

        public void StartTrace()
        {
            lock(threadLock)
            {
                AddTraceItem();
            }         
        }

        public void StopTrace()
        {
            lock (threadLock)
            {
                stackFrame = ResetStackFrame(2);

                string currentMethodName = stackFrame.Value.GetMethod().Name;
                int currentThreadId = Thread.CurrentThread.ManagedThreadId;
                TraceResultItem analyzedItem = result.Find(currentMethodName, currentThreadId);

                analyzedItem.StopRuntimeMeasuring();
            }
        }

        public TraceResult GetTraceResult()
        {
            lock (threadLock)
            {
                return result;
            }
        }

        private void AddTraceItem()
        {
            stackFrame = ResetStackFrame(3);

            var method = stackFrame.Value.GetMethod();
            var parametersAmount = method.GetParameters().Length;
            var className = method.ReflectedType.Name;
            var threadId = Thread.CurrentThread.ManagedThreadId;

            var newAnalyzedItem = new TraceResultItem(new Stopwatch(), className, method.Name, parametersAmount, threadId, stackFrame.Key);
            UpdateTraceResult(newAnalyzedItem, threadId);
            UpdateCallDepth(stackFrame.Key);
        }

        private KeyValuePair<int, StackFrame> ResetStackFrame(int depth)
        {
            stackTrace = new StackTrace();
            return new KeyValuePair<int, StackFrame>(stackTrace.FrameCount, stackTrace.GetFrame(depth));           
        }
        private void UpdateTraceResult(TraceResultItem newAnalyzedItem, int threadId)
        {
            result.Add(newAnalyzedItem);
            result.MarkThread(threadId);
        }
        private void UpdateCallDepth(int newDepth)
        {
            if (newDepth > result.callDepth)
                result.callDepth = newDepth;            
        }

    }
}
