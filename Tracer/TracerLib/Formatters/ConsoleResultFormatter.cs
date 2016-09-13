using System;
using TracerLib.Interfaces;
using TracerLib.Utils;

namespace TracerLib.Formatters
{
    public class ConsoleResultFormatter : ITraceResultFormatter
    {
        public void Format(TraceResult result)
        {
            var headNodes = result.Threads;

            foreach (var Id in headNodes.Keys)
            {
                Console.WriteLine($"Thread Id: {Id}");
                PrintMethodResults(headNodes[Id].HeadNode, 0);
                Console.WriteLine("------------------------------------------------------------");
            }
        }

        private static void PrintMethodResults(Node<TracedMethodInfo> node, int nesting)
        {   
            var tab = GetTab(nesting);
            Console.WriteLine($"{tab}Method: {node.Item.MethodName}");
            Console.WriteLine($"{tab}Class: {node.Item.ClassName}");
            Console.WriteLine($"{tab}Execution time: {node.Item.Watcher.ElapsedMilliseconds} ms");
            Console.WriteLine($"{tab}Number of parameters: {node.Item.ArgumentsNumber}");

            if (node.Children.Count > 0)
            {
                Console.WriteLine($"{tab}Called methods");

                nesting++;
                foreach (var child in node.Children)
                {
                    PrintMethodResults(child, nesting);
                    Console.WriteLine($"{tab}{tab}*****************************");
                }
            }
        }

        private static string GetTab(int nesting)
        {
            var tabs = ""; 
            for (int i = 0; i < nesting; i++)
            {
                tabs += "    ";
            }

            return tabs;
        }
    }
}