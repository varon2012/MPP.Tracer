using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Reflection;

namespace Tracer
{
    public class Tracer : ITracer
    {

        private TraceResult resultsOfTrace;

        public Tracer()
        {
            resultsOfTrace = new TraceResult();
        }

        public TraceResult GetTraceResult()
        {

            return resultsOfTrace;
        }

        public void StartTrace()
        {
            MethodInfo curentMethod = GetMethodInfo();
            resultsOfTrace.StartTrace(curentMethod);
            
        }

        public void StopTrace()
        {
            resultsOfTrace.StopTrace(GetCurrentTime());  
        }

        private long GetCurrentTime()
        {
            return DateTime.UtcNow.Ticks / TimeSpan.TicksPerMillisecond;
        }

        private MethodInfo GetMethodInfo()
        {
            long time = GetCurrentTime();
            StackTrace stackTrace = new StackTrace();
            MethodBase methodBase = stackTrace.GetFrame(2).GetMethod();
            string Name = methodBase.Name;
            string Class = methodBase.DeclaringType.ToString();
            int parametersNumber = methodBase.GetParameters().Length;

            return new MethodInfo(Name,Class, parametersNumber,time);
        }
    }
}
