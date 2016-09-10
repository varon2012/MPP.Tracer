using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Tracer.Classes.Tree;

namespace Tracer.Classes
{
    public class Tracer : ITracer
    {
        private TraceResult traceResult;
        private RootNode rootNode;

        public Tracer()
        {
            rootNode = new RootNode();
        }

        public void StartTrace()
        {
            long currentTime = GetCurrentTime();
            CallerDescriptor caller = GetCallerDescriptor();
            rootNode.FixateCountStart(currentTime, caller);
        }

        public void StopTrace()
        {
            long currentTime = GetCurrentTime();
            rootNode.FixateCountEnd(currentTime);
        }

        public TraceResult GetTraceResult()
        {
            return traceResult;
        }

        private long GetCurrentTime()
        {
            return new DateTime().Ticks/10000; // 10000 ticks in millisecond
        }

        private CallerDescriptor GetCallerDescriptor()
        {
            StackTrace stackTrace = new StackTrace();
            MethodBase methodBase = stackTrace.GetFrame(2).GetMethod();

            String callerName = methodBase.Name;
            String callerClass = methodBase.DeclaringType.ToString();
            int parametersNumber = methodBase.GetParameters().Length;

            return new CallerDescriptor(callerName, callerClass, parametersNumber);
        }
    }
}
