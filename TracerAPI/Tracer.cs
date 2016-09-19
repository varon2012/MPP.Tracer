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
        public TraceResult TraceInfo;

        private Tracer()
        {
            TraceInfo = new TraceResult();
        }

        public void StartTrace()
        {
            DateTime startTime = DateTime.Now;

            Node node = new Node();
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
        public void StopTrace()
        {
            DateTime stopTime = DateTime.Now;

            StackTrace stackTrace = new StackTrace();
            int frameCount = stackTrace.FrameCount;
    
            StackFrame methodStackFrame = stackTrace.GetFrame(1);
            StackFrame parentStackFrame = stackTrace.GetFrame(2);

            Thread thread = Thread.CurrentThread;
            int threadId = thread.ManagedThreadId;
            string methodName = methodStackFrame.GetMethod().ToString();
            string parentName = parentStackFrame.GetMethod().ToString();
            string methodClassName = "System Main";

            Tracer.AddStopInfo(threadId, parentName, methodName, methodClassName, stopTime);

        }

        public TraceResult GetTraceResult() { return TraceInfo; }

        public static void AddStartInfo(int threadId, string parentName,
                                        string methodName, string methodClassName, DateTime startTime)
        {
    
        }

        public static void AddStopInfo(int threadId, string parentName, 
                                       string methodName, string methodClassname, DateTime stopTime)
        {

        }
    }
}
