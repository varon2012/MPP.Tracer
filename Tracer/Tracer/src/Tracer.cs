using System;
using System.Diagnostics;
using System.Reflection;
using MPPTracer.Tree;

namespace MPPTracer
{
    public class Tracer : ITracer
    {
        private RootNode rootNode;
        private static readonly Tracer instance;

        static Tracer()
        {
            instance = new Tracer();
        }

        private Tracer()
        {
            rootNode = new RootNode();
        }

        public static Tracer Instance
        {
            get
            {
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
            return DateTime.UtcNow.Ticks / TimeSpan.TicksPerMillisecond;
        }

        private MethodDescriptor GetMethodDescriptor()
        {
            StackTrace stackTrace = new StackTrace();
            MethodBase methodBase = stackTrace.GetFrame(2).GetMethod();

            string callerName = methodBase.Name;
            string callerClass = methodBase.DeclaringType.ToString();
            int parametersNumber = methodBase.GetParameters().Length;

            return new MethodDescriptor(callerName, callerClass, parametersNumber);
        }
    }
}
