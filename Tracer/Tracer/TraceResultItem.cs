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
        public double time { get; set; }
        private Stopwatch timer { get; set; }
        public string className { get; set; }
        public string methodName { get; set; }
        public int parametersAmount { get; set; }
        public int threadId { get; set; }
        public int callDepth { get; set; }

        public TraceResultItem(Stopwatch timer, string className, string methodName, int parametersAmount, int threadId, int callDepth)
        {
            this.methodName = methodName;
            this.parametersAmount = parametersAmount;
            this.className = className;
            this.timer = timer;
            this.threadId = threadId;
            this.callDepth = callDepth;
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
        
        public override string ToString()
        {
            return string.Format("Поток {0}: {1}->{2}(параметры: {3}): {4} сек.", threadId, className, methodName, parametersAmount, time );
        }
    }
}
