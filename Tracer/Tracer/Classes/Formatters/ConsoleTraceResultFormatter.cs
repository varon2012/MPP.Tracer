using System;
using Trace.Classes.TraceInfo;
using Trace.Interfaces;

namespace Trace.Classes.Formatters
{
    public class ConsoleTraceResultFormatter : ITraceResultFormatter
    {
        public void Format(TraceResult traceResult)
        {
            foreach (var threadInfo in traceResult.ThreadsInfo)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Thread id: {threadInfo.Key}; time: {threadInfo.Value.ExecutionTime};");

                Console.ForegroundColor = ConsoleColor.White;
                foreach (var methodInfo in threadInfo.Value.AllMethodsInfo)
                {
                    WriteMethodInfo(methodInfo);
                }

                Console.WriteLine();
            }
        }

        private void WriteMethodInfo(MethodTrace methodInfo, int nestingLevel = 0)
        {
            var nesting = "";

            for (var i = 0; i < nestingLevel; i++)
                nesting += "\t";

            Console.WriteLine($"{nesting}Method name: {methodInfo.metadata.Name}; time: {methodInfo.GetExecutionTime()}; " +
                               $" class: {methodInfo.metadata.ClassName}; params: {methodInfo.metadata.CountParameters}");

            foreach (var nestedMethod in methodInfo.NestedMethods)
            {
                WriteMethodInfo(nestedMethod, nestingLevel + 1);
            }

        }
    }
}
