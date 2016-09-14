using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer.Formatters
{
    public class ConsoleTraceResultFormatter : ITraceResultFormatter
    {
        public void Format(TraceResult traceResult)
        {
            foreach (TraceResultItem analyzedItem in traceResult)
            {
                analyzedItem.PrintToConsole();
            }
        }
    }
}
