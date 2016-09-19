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
            long time = GetCurrentTime();
            MethodInfo curentMethod = GetMethodInfo();

            
        }

        public void StopTrace()
        {
           
        }

        private long GetCurrentTime()
        {
            return DateTime.UtcNow.Ticks / TimeSpan.TicksPerMillisecond;
        }

        private MethodInfo GetMethodInfo()
        {
            StackTrace stackTrace = new StackTrace();
            MethodBase methodBase = stackTrace.GetFrame(2).GetMethod();
            string Name = methodBase.Name;
            string Class = methodBase.DeclaringType.ToString();
            int parametersNumber = methodBase.GetParameters().Length;

            return new MethodInfo(Name,Class, parametersNumber);
        }
    }
}
