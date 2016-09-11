using System;
using System.Diagnostics;
using System.Reflection;
using Tracer.Tree;

namespace Tracer
{
    public class Tracer : ITracer
    {
        private RootNode rootNode;

        public Tracer()
        {
            rootNode = new RootNode();
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
            return DateTime.Now.Millisecond;
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
