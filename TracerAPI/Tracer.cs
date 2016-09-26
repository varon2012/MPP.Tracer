using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;

namespace TracerAPI
{
    public class Tracer
    {
        private TraceResult TraceInfo;
        private static volatile Tracer instance = null;
        private static readonly Object syncObj = new Object();
       

        private Tracer()
        {
            TraceInfo = new TraceResult();
        }

        public static Tracer Instance
        {
            get
            {
                if(instance == null)
                {
                    lock (syncObj)
                    {
                        if (instance == null)
                        {
                            instance = new Tracer();
                        }                        
                    }
                }
                return instance;
            }
        }

        public void StartTrace()
        {
            DateTime startTime = DateTime.Now;
            StackTrace stackTrace = new StackTrace();
            StackFrame methodFrame = stackTrace.GetFrame(1);
            StackFrame parentFrame = stackTrace.GetFrame(2);
            Thread thread = Thread.CurrentThread;
            int threadId = thread.ManagedThreadId; 
            string methodName = methodFrame.GetMethod().ToString();
            string parentName = parentFrame.GetMethod().ToString();
            string methodClassName = methodFrame.GetMethod().ReflectedType.ToString();

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

            Tracer.AddStopInfo(threadId, methodName, stopTime);

        }

        public TraceResult GetTraceResult() 
        {
            return TraceInfo; 
        }

        public static void AddStartInfo(int threadId, string parentName,
                                        string methodName, string methodClassName, 
                                        DateTime startTime)
        {
            Tree tree;           
            tree = Tracer.Instance.GetTraceResult().GetTreeByThreadId(threadId);           
            tree.AddNode(parentName, methodName, methodClassName, startTime);
        }

        public static void AddStopInfo(int threadId, string methodName, DateTime stopTime)
        {
            Tree tree;
            tree = Tracer.Instance.GetTraceResult().GetTreeByThreadId(threadId);
            tree.CompleteNode(methodName, stopTime);
        }
    }
}
