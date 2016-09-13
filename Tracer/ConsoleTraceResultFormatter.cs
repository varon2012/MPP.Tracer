using System;
using System.Collections.Generic;

namespace Tracer
{
    public class ConsoleTraceResultFormatter : ITraceResultFormatter
    {
        public void Format(TraceResult traceResult)
        {
            foreach (int threadId in traceResult.ThreadNodes.Keys)
            {
                Console.WriteLine(
                    $"Thread[ID={threadId}; TIME={traceResult.ThreadNodes[threadId].ExecutionTime.Milliseconds}]");
                PrintMethods(traceResult.ThreadNodes[threadId].MethodNodes, 1);
            }
        }

        private void PrintMethods(List<MethodNode> methodNodes, int tabCount)
        {
            foreach (MethodNode methodNode in methodNodes)
            {
                for (int i = 0; i < tabCount; i++)
                {
                    Console.Write("| ");
                }
                Console.WriteLine(
                    $"Method[TIME={methodNode.ExecutionTime.Milliseconds}; NAME={methodNode.MethodName}; CLASS={methodNode.ClassName}; PARAMETER COUNT={methodNode.ParameterCount}]");
                PrintMethods(methodNode.InnerMethods, tabCount + 1);
            }
        }
    }
}