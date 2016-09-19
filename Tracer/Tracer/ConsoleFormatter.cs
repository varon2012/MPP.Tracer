using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    public class ConsoleFormatter
    {
        private int currentNesting = 0;
        public void Format(TraceResult traceResult)
        {
            Console.WriteLine("root:");
            PrintThreads(traceResult);
        }

        private void PrintThreads(TraceResult traceResult)
        {
            foreach (var thread in traceResult)
            {
                currentNesting++;
                Console.WriteLine(Indent() + "thread: " + PrintAttributes(new Dictionary<string, string>(){
                    { "id" , $"{thread.Value.ThreadId}"},
                    { "time" , $"{thread.Value.Time}ms"}}));
                if (thread.Value.Methods.Any())
                    foreach (TraceMethodItem method in thread.Value.Methods)
                        PrintMethods(method);
                currentNesting--;
            }
        }

        private void PrintMethods(TraceMethodItem method)
        {
            currentNesting++;
            Console.WriteLine(Indent() + "method: " + PrintAttributes(new Dictionary<string, string>() {
               {"name", method.Name },
               {"class", method.ClassName },
               {"params", $"{method.ParamsCount}" },
               {"time", $"{method.Time} ms"}}));
            if (method.NestedMethods.Any())
                foreach (TraceMethodItem nestedMethod in method.NestedMethods)
                    PrintMethods(nestedMethod);
            currentNesting--;
        }

        private string PrintAttributes(Dictionary<string, string> attributes)
        {
            string result = "{ ";
            foreach (KeyValuePair<string, string> attr in attributes)
            {
                result += attr.Key + " => " + attr.Value + " ";
            }
            result += "}";
            return result;
        }

        private string Indent()
        {
            return new string(' ', currentNesting * 2);
        }
    }
}
