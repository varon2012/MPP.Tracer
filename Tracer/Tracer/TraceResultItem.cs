using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
namespace Tracer
{
    public class TraceResultItem
    {
        public TraceResult children;

        public double time { get; set; }
        private Stopwatch timer { get; set; }
        public string className { get; set; }
        public string methodName { get; set; }
        public string parent { get; set; }
        public int parametersAmount { get; set; }
        public int threadId { get; set; }
        public int callDepth { get; set; }

        public TraceResultItem(Stopwatch timer, string className, string methodName, int parametersAmount, int threadId, int callDepth, string parent)
        {
            this.methodName = methodName;
            this.parametersAmount = parametersAmount;
            this.className = className;
            this.timer = timer;
            this.threadId = threadId;
            this.callDepth = callDepth;
            this.parent = parent;
            try
            {
                timer.Start();
            }
            catch (NullReferenceException)
            {
                throw new NullReferenceException("Timer is missing! Check arguments passed to TraceResultItem.");
            }
        }

        public void StopRuntimeMeasuring()
        {
            timer.Stop();
            time = Math.Round(timer.Elapsed.TotalSeconds, 3);
        }

        public void AddChild(TraceResultItem child)
        {
            if (children == null)
                children = new TraceResult();
            children.Add(child);
        }        
        public override string ToString()
        {
            return string.Format("{0}->{1}(параметры: {2}): {3} сек.",  className, methodName, parametersAmount, time );
        }

    }
}
