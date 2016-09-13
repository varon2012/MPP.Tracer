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
        public long time { get; set; }
        private Stopwatch timer { get; set; }
        public string className { get; set; }
        public string methodName { get; set; }
        public int parametersAmount { get; set; }
        public int threadId { get; set; }

        public TraceResultItem(Stopwatch timer, string className, string methodName, int parametersAmount, int threadId)
        {
            this.methodName = methodName;
            this.parametersAmount = parametersAmount;
            this.className = className;
            this.timer = timer;
            this.threadId = threadId;
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
            time = timer.ElapsedMilliseconds;
        }
        
        public void PrintToConsole()
        {
            Console.WriteLine("["+ threadId + "] "+className + "->" + methodName + " (кол-во параметров - " + parametersAmount + ") выполнялся " + time * 0.001 + " сек.");
        }
    }
}
