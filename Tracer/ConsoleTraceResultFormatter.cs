using System;
using System.Collections.Generic;
using System.Linq;

namespace Tracer
{
    public sealed class ConsoleTraceResultFormatter : ITraceResultFormatter
    {
        public void Format(TraceResult traceResult)
        {
            var headNodes = traceResult.HeadNodes;
            foreach (var threadId in headNodes.Keys)
            {
                Console.WriteLine($"Thread Id: {threadId}");
                foreach (var node in headNodes[threadId].TopLevelNodes)
                {
                    PrintNode(node, 0);
                }
            }
        }

        private static void PrintNode(TraceResultNode node, int level)
        {
            var indent = string.Format($"{{0, {level * 2 + 1}}}", "");

            Console.Write($"{indent}Location: {node.ClassName}#{node.MethodName}");
            PrintParameters(node.Parameters);
            Console.WriteLine($"{indent}Start time: {node.StartTime}");
            Console.WriteLine($"{indent}Finish time: {node.FinishTime}");
            Console.WriteLine($"{indent}Tracing time: {node.TracingTime}");
            Console.WriteLine();

            foreach (var internalNode in node.InternalNodes)
            {
                PrintNode(internalNode, level + 1);
            }
        }

        private static void PrintParameters(List<ParameterInfo> parameters)
        {
            Console.Write("(");
            foreach (var parameter in parameters)
            {
                Console.Write($"{parameter.Type} {parameter.Name}");
                if (!parameter.Equals(parameters.Last()))
                {
                    Console.Write(", ");
                }
            }
            Console.WriteLine(")");
        }
    }
}
