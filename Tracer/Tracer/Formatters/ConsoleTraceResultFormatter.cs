using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;

namespace Tracer.Formatters
{
    public class ConsoleTraceResultFormatter : ITraceResultFormatter
    {
        public void Format(TraceResult traceResult)
        {
            ITraceResultFormatter formatter = new XmlTraceResultFormatter();
            formatter.Format(traceResult);

            PrintThread(traceResult);           
        }

        private void PrintThread(TraceResult traceResult)
        {
            traceResult.Sort();
            foreach (TraceResultItem analyzedItem in traceResult)
            {
                Console.WriteLine(analyzedItem.ToString());
            }
        }

    }
}
