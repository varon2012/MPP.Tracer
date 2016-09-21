using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    public class TraceMethodItem : IMeasureable
    {
        public string Name { get; private set; }
        public string ClassName { get; private set; }
        public int Time { get; private set;} 
        public int ParamsCount { get; private set; }

        private Stopwatch stopwatch;

        public List<TraceMethodItem> NestedMethods = new List<TraceMethodItem>();

        public TraceMethodItem(string name, string className, int paramsCount)
        {
            Name = name;
            ClassName = className;
            ParamsCount = paramsCount;
            stopwatch = new Stopwatch();
            stopwatch.Start();
        }

        public void Measure()
        {
            stopwatch.Stop();
            TimeSpan t = stopwatch.Elapsed;
            Time = t.Milliseconds;
        }
    }
}
