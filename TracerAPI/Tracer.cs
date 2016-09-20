using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace TracerAPI
{
    public static class Tracer
    {
        public static TraceResult TraceInfo = new TraceResult();

        public static void StartTrace()
        {
            DateTime startTime = DateTime.Now;
            StackTrace stackTrace = new StackTrace();
            StackFrame methodFrame = stackTrace.GetFrame(1);
            StackFrame parentFrame = stackTrace.GetFrame(2);
            Thread thread = Thread.CurrentThread;
            int threadId = thread.ManagedThreadId; 
            string methodName = methodFrame.GetMethod().ToString();
            string parentName = parentFrame.GetMethod().ToString();
            string methodClassName = "System Main";

            Tracer.AddStartInfo(threadId, parentName, methodName, methodClassName, startTime);
  
        }
        public static void StopTrace()
        {
            DateTime stopTime = DateTime.Now;

            StackTrace stackTrace = new StackTrace();
            int frameCount = stackTrace.FrameCount;
    
            StackFrame methodStackFrame = stackTrace.GetFrame(1);
            StackFrame parentStackFrame = stackTrace.GetFrame(2);

            Thread thread = Thread.CurrentThread;
            int threadId = thread.ManagedThreadId;
            string methodName = methodStackFrame.GetMethod().ToString();

            Tracer.AddStopInfo(threadId, methodName, stopTime);

        }

        public static TraceResult GetTraceResult() { return TraceInfo; }

        public static void AddStartInfo(int threadId, string parentName,
                                        string methodName, string methodClassName, DateTime startTime)
        {
            Tree tree;           
            tree = TraceInfo.GetTreeByThreadId(threadId);           
            tree.AddNode(parentName, methodName, methodClassName, startTime);
        }

        public static void AddStopInfo(int threadId, string methodName, DateTime stopTime)
        {
            Tree tree;
            tree = TraceInfo.GetTreeByThreadId(threadId);
            tree.CompleteNode(methodName, stopTime);
        }
    }
}
