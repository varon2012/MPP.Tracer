using System;
using System.Collections.Generic;
using System.Linq;
using Tracer.Interfaces;
using Tracer.Models;

namespace Tracer.Fromatters
{
    public class ConsoleTraceResultFormatter : ITraceResultFormatter
    {
        public void Format(TraceResult traceResult)
        {
            foreach (KeyValuePair<int, ThreadTraceInfo> threadTraceInfo in traceResult.ThreadsTraceInfo)
            {
                Console.WriteLine($"Thread [id={threadTraceInfo.Key} time={threadTraceInfo.Value.Duration}]");
                PrintMethodsTraceInfo(threadTraceInfo.Value.MethodsTraceInfo, 1);
            }
        }

        private static void PrintMethodsTraceInfo(List<MethodTraceInfo> methodsTraceInfo, int nestingLevel)
        {
            foreach (MethodTraceInfo methodTraceInfo in methodsTraceInfo)
            {
                Console.Write(GetIndent(nestingLevel));
                PrintMethodTraceInfo(methodTraceInfo, nestingLevel + 1);
            }
        }

        private static string GetIndent(int nestingLevel)
        {
            return string.Concat(Enumerable.Repeat(" ", nestingLevel * 4));
        }

        private static void PrintMethodTraceInfo(MethodTraceInfo methodTraceInfo, int nestingLevel)
        {
            Console.Write($"Method [name={methodTraceInfo.Name} ");
            Console.Write($"class={methodTraceInfo.ClassName} ");
            Console.Write($"time={methodTraceInfo.Duration} ");
            Console.Write($"params={methodTraceInfo.ArgumentsCount}]");
            PrintMethodsTraceInfo(methodTraceInfo.NestedMethodsTraceInfo, nestingLevel + 1);
        }
    }
}