using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    public static class ConsoleFormatter
    {
        private static int currentNesting = 0;
        public static void Format(TraceResult traceResult)
        {
            Console.WriteLine("root:");
            printThreads(traceResult);
        }

        private static void printThreads(TraceResult traceResult)
        {
            foreach (var thread in traceResult)
            {
                currentNesting++;
                Console.WriteLine(indent() + "thread: " + printAttributes(new Dictionary<string, string>(){
                    { "id" , $"{thread.ThreadId}"},
                    { "time" , $"{thread.Time}ms"}}));
                if (thread.Methods != null)
                    foreach (TraceMethodItem method in thread.Methods)
                        printMethods(method);
                currentNesting--;
            }
        }

        private static void printMethods(TraceMethodItem method)
        {
            currentNesting++;
            Console.WriteLine(indent() + "method: " + printAttributes(new Dictionary<string, string>() {
               {"name", method.Name },
               {"class", method.ClassName },
               {"params", $"{method.ParamsCount}" },
               {"time", $"{method.Time} ms"}}));
            if (method.NestedMethods != null)
                foreach (TraceMethodItem nestedMethod in method.NestedMethods)
                    printMethods(nestedMethod);
            currentNesting--;
        }

        private static string printAttributes(Dictionary<string, string> attributes)
        {
            string result = "{ ";
            foreach (KeyValuePair<string, string> attr in attributes)
            {
                result += attr.Key + " => " + attr.Value + " ";
            }
            result += "}";
            return result;
        }

        private static string indent()
        {
            return new string(' ', currentNesting * 2);
        }
    }
}
