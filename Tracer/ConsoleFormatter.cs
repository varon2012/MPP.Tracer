using System;
using System.Linq;

namespace Tracer
{
    public class ConsoleFormatter : ITraceResultFormatter
    {
        public void Format(TraceResult traceResult)
        {
            Console.WriteLine("root");

            string indent = "  ";

            foreach (var element in traceResult.TraceTree)
            {
                Console.WriteLine(indent + "thread id=" + element.Key + " time=" + traceResult.ThreadTime[element.Key] + "ms");
                foreach (var node in element.Value)
                {
                    AddMethodToConsoleTree(indent + "  ", node);
                }
            }
        }

        private void AddMethodToConsoleTree(string indent, MethodsTreeNode methodNode)
        {
            Console.WriteLine(String.Format("{0}method name={1} time={2} class={3} params={4}", 
                indent,
                methodNode.Method.MethodName,
                methodNode.Method.Watcher.ElapsedMilliseconds,
                methodNode.Method.ClassName,
                methodNode.Method.ParametersNumber));

            foreach (var child in methodNode.Children)
            {
                AddMethodToConsoleTree(indent + "  ", child);
            }

        }
    }
}