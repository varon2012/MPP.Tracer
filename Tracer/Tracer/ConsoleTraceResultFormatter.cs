using System;
using System.Collections.Generic;

namespace Tracer
{
    public class ConsoleTraceResultFormatter : ITraceResultFormatter
    {
        private string baseIndent = "\t";
        public void Format(TraceResult traceResult)
        {
            Dictionary<int, TraceResultThreadNode> threadNodes = traceResult.ThreadNodes;
            foreach (var threadId in threadNodes.Keys)
            {
                Console.WriteLine("Thread ID: {0}", threadId);

                foreach (var node in threadNodes[threadId].MethodNodesList)
                {
                    PrintMethodNode(node, baseIndent);
                    Console.WriteLine();
                }

                Console.WriteLine();
                Console.WriteLine();
            }
        }

        private void PrintMethodNode(TraceResultMethodNode methodNode, string indent)
        {
            Console.WriteLine("{0}Method: {1}", indent, methodNode.MethodName);
            Console.WriteLine("{0}Class: {1}", indent, methodNode.ClassName);
            Console.WriteLine("{0}Params: {1}", indent, methodNode.ParamCount);
            Console.WriteLine("{0}Time (ms): {1}", indent, methodNode.TotalTime.TotalMilliseconds);

            string newIndent = indent + baseIndent;
            foreach (var node in methodNode.InsertedNodes)
            {
                PrintMethodNode(node, newIndent);
                Console.WriteLine();
            }
        }
    }
}
