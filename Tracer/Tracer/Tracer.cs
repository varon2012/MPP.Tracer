using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Tracer
{
    public class Tracer : ITracer
    {
        private TraceResult result {get;set;}
        private Stopwatch timer { get; set; }

        public void StartTrace()
        {
            timer = Stopwatch.StartNew();
            // something            
        }

        public void StopTrace()
        {
            // something
            timer.Stop();
        }

        public TraceResult GetTraceResult()
        {
            // something
            result = new TraceResult(timer.ElapsedMilliseconds);
            return result;
        }

    }
}
