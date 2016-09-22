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
        internal TraceResult children;

        private Stopwatch timer { get; set; }

        internal double time { get; set; }
        internal string className { get; set; }
        internal string methodName { get; set; }
        internal string parent { get; set; }
        internal int parametersAmount { get; set; }

        internal int threadId { get; set; }
        internal int callDepth { get; set; }        

        internal TraceResultItem(Stopwatch timer, string className, string methodName, int parametersAmount, int threadId, int callDepth, string parent)
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
                throw new TraceResultException("Timer is missing! Check arguments passed to TraceResultItem.");
            }
        }

        internal void StopRuntimeMeasuring()
        {
            timer.Stop();
            time = Math.Round(timer.Elapsed.TotalSeconds, 3);
        }

        internal void AddChild(TraceResultItem child)
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
