using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    public class TraceResult
    {
        public long time { get; set; }
        public string className { get; set; }
        public string methodName { get; set; }
        public int parametersAmount { get; set; }


        public TraceResult(long time, string className, string methodName, int parametersAmount) 
        {
            this.time = time;
            this.methodName = methodName;
            this.parametersAmount = parametersAmount;
            this.className = className;
        }
        public void PrintToConsole()
        {
            Console.WriteLine(className + "->" + methodName + " (кол-во параметров - " + parametersAmount+ ") выполнялся " + time * 0.001 + " сек." );
        }
    }
}
