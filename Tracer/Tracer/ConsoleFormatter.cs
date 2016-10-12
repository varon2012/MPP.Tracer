using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    public class ConsoleFormatter : IFormatter
    {
        private const string addingSpaces = "---";

        public void Format(TraceResult traceResult)
        {
            string spaces = addingSpaces;
            Console.WriteLine("root");
            foreach (var mainMethod in traceResult.MethodInfoDictionary)
            {
                Console.WriteLine("{0}(threadID = {1}, time = {2})",spaces, mainMethod.Key, mainMethod.Value.Node.Watcher.ElapsedMilliseconds);
                WriteToConsole(mainMethod.Value, spaces + addingSpaces);
            }
        }

        private void WriteToConsole(Tree<MethodInfo> method, string spacing)
        {
            Console.WriteLine("{0}(method = {1}, time = {2}, class = {3}, parametres = {4})", spacing,
                                                                                   method.Node.MethodName,
                                                                                   method.Node.Watcher.ElapsedMilliseconds,
                                                                                   method.Node.ClassName,
                                                                                   method.Node.NumberParametres);
            foreach (var node in method.NodesList)
            {
                WriteToConsole(node, spacing + addingSpaces);
            }
        }
    }
}
