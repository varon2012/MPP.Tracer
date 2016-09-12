using System;
using System.Collections.Generic;

namespace Tracer
{
    public class ConsoleTraceResultFormatter: ITraceResultFormatter
    {
        public void Format(TraceResult traceResult)
        {
            foreach (int threadId in traceResult.ThreadNodes.Keys)
            {
                Console.WriteLine("Thread[ID={0}; TIME={1}]", threadId, traceResult.ThreadNodes[threadId].ExecutionTime.Milliseconds);
                foreach (MethodNode methodNode in traceResult.ThreadNodes[threadId].MethodNodes)
                {
                    Console.WriteLine("\tMethod[TIME={0}; NAME={1}; CLASS={2}; PARAMETER COUNT={3}]", methodNode.ExecutionTime.Milliseconds, methodNode.MethodName, methodNode.ClassName, methodNode.ParameterCount);
                    PrintInnerMethods(methodNode.InnerMethods, 2);
                }
            }
        }

        private void PrintInnerMethods(List<MethodNode> methodNodes, int tabCount)
        {
            foreach (MethodNode methodNode in methodNodes)
            {
                for (int i = 0; i < tabCount; i++)
                {
                    Console.Write("\t");
                }
                Console.WriteLine("Method[TIME={0}; NAME={1}; CLASS={2}; PARAMETER COUNT={3}]", methodNode.ExecutionTime.Milliseconds, methodNode.MethodName, methodNode.ClassName, methodNode.ParameterCount);
                PrintInnerMethods(methodNode.InnerMethods, ++tabCount);
            }
        }

    }
}