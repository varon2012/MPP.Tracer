using System;
using System.Collections.Generic;
using Tracer.Interfaces;
using Tracer.Models;

namespace Tracer.Formatters
{
    public class ConsoleTraceResultFormatter : ITraceResultFormatter
    {
        public void Format(TraceResult traceResult)
        {           
            foreach (ThreadTraceResult threadTraceResult in traceResult.ThreadsTraceResult)
            {
                Console.WriteLine(threadTraceResult);
                PrintMethodsTraceResult(threadTraceResult.MethodsTraceResult, 0);
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
            return new string(' ', nestingLevel * 4);
        }
    }
}