using System;
using System.Collections.Generic;
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

        public DateTime StartTime { get; private set; }

        public List<TraceMethodItem> NestedMethods = new List<TraceMethodItem>();

        public TraceMethodItem(string name, string className, int paramsCount, DateTime startTime)
        {
            Name = name;
            ClassName = className;
            ParamsCount = paramsCount;
            StartTime = startTime;
        }

        public void Measure()
        {
            TimeSpan t = DateTime.Now - StartTime;
            Time = t.Milliseconds;
        }
    }
}
