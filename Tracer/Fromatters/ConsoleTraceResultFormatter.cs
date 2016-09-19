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
            foreach (KeyValuePair<int, ThreadTraceResult> threadTraceInfo in traceResult.ThreadsTraceResult)
            {
                Console.WriteLine($"Thread [id={threadTraceInfo.Key} time={threadTraceInfo.Value.Duration}]");
                PrintMethodsTraceResult(threadTraceInfo.Value.MethodsTraceResult, 0);
            }
        }

        private static void PrintMethodsTraceResult(List<MethodTraceResult> methodsTraceResult, int nestingLevel)
        {
            methodsTraceResult.ForEach((traceResult) => PrintMethodTraceResult(traceResult, nestingLevel + 1));
        }

        private static void PrintMethodTraceResult(MethodTraceResult methodTraceResult, int nestingLevel)
        {
            Console.Write(GetIndent(nestingLevel));
            Console.WriteLine(methodTraceResult);
            PrintMethodsTraceResult(methodTraceResult.NestedMethodsTraceResult, nestingLevel);
        }

        private static string GetIndent(int nestingLevel)
        {
            return string.Concat(Enumerable.Repeat(" ", nestingLevel * 4));
        }
    }
}