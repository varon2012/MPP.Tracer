using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.IO;

namespace Tracer.Formatters
{
    public class ConsoleTraceResultFormatter : ITraceResultFormatter
    {
        public void Format(TraceResult traceResult)
        {
            TraceResult resultToModify = (TraceResult)traceResult.Clone();
            AddMethodNesting(resultToModify);
            Print(resultToModify, 0);           
        }

        private void Print(TraceResult traceResult, int depth)
        {
            depth++;
            foreach (TraceResultItem analyzedItem in traceResult)
            {
                if (depth == 1)
                    Console.WriteLine("\nПоток №{0}",analyzedItem.threadId);
                Console.WriteLine("{0}|\n{0}{1}",addTabs(depth), analyzedItem.ToString());
                if (analyzedItem.children != null)
                    Print(analyzedItem.children, depth);
            }
            
        }

        private string addTabs(int n)
        {
            return new String('\t', n);
        }

        private void AddMethodNesting(TraceResult traceResult)
        {
            traceResult.SortByThread();

            for (int callDepth = traceResult.callDepth - 1; callDepth >= 0; callDepth--)
            {
                foreach (TraceResultItem parentElement in traceResult.ToArray())
                {
                    NestChildren(parentElement, callDepth, traceResult);
                }
            }
        }

        private void NestChildren(TraceResultItem parentElement, int callDepth, TraceResult traceResult)
        {
            if (callDepth == parentElement.callDepth)
            {
                foreach (TraceResultItem childElement in traceResult.ToArray())
                {
                    ProcessPossibleChild(childElement, parentElement, traceResult);
                }
            }
        }

        private void ProcessPossibleChild(TraceResultItem childElement, TraceResultItem parentElement, TraceResult traceResult)
        {
            if (childElement.callDepth > parentElement.callDepth && childElement.threadId == parentElement.threadId && parentElement.methodName == childElement.parent)
            {
                parentElement.AddChild(childElement);
                traceResult.Remove(childElement);
            }
        }
    }
}
