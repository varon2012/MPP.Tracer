using System;
using System.Collections.Immutable;
using TracerLib.Interfaces;
using TracerLib.Utils;

namespace TracerLib.Formatters
{
    public class ConsoleResultFormatter : ITraceResultFormatter
    {
        public void Format(TraceResult result)
        {
            var headNodes = result.results;

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

        public void Format(ImmutableDictionary<int, ThreadDescriptor> results)
        {
            throw new NotImplementedException();
        }

        private static string GetTab(int nesting)
        {
            var tabs = string.Empty; 
            for (int i = 0; i < nesting; i++)
            {   
                tabs = new string(' ', nesting);
            }

            return tabs;
        }
    }
}