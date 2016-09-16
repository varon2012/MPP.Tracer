using System;
using System.Diagnostics;
using System.Reflection;
using MPPTracer.Tree;

namespace MPPTracer
{
    public class Tracer : ITracer
    {
        private RootNode rootNode;
        private static volatile Tracer instance;
        private static object syncRoot = new object();

        private Tracer()
        {
            rootNode = new RootNode();
        }
        public static Tracer Instance
        {
            get
            {
                if(instance == null)
                {
                    lock (syncRoot)
                    {
                        if(instance == null)
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
            long currentTime = GetCurrentTime();
            MethodDescriptor descriptor = GetMethodDescriptor();
            rootNode.AddNestedTrace(currentTime, descriptor);
        }

        public void StopTrace()
        {
            long currentTime = GetCurrentTime();
            rootNode.StopLastTrace(currentTime);
        }

        public TraceResult GetTraceResult()
        {
            TraceResult traceResult = new TraceResult(rootNode);
            return traceResult;
        }

        private long GetCurrentTime()
        {
            return DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
        }

        private MethodDescriptor GetMethodDescriptor()
        {
            StackTrace stackTrace = new StackTrace();
            MethodBase methodBase = stackTrace.GetFrame(2).GetMethod();

            String callerName = methodBase.Name;
            String callerClass = methodBase.DeclaringType.ToString();
            int parametersNumber = methodBase.GetParameters().Length;

            return new MethodDescriptor(callerName, callerClass, parametersNumber);
        }
    }
}
