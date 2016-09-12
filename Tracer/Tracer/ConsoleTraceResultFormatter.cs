using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    public class ConsoleTraceResultFormatter : ITraceResultFormatter
    {
        private static string GetTabSequence(int count)
        {
            string result = "";
            for (int i = 0; i < count; i++)
                result += "\t";
            return result;
        }

        public void Format(TraceResult traceResult)
        {
            foreach (int key in traceResult.threadDictionary.Keys)
            {
                Console.WriteLine("Thread id: " + key);
                foreach (TreeNode node in traceResult.threadDictionary[key].climbTree())
                {
                    Console.WriteLine(GetTabSequence(node.level) + node.methodName + " " + node.paramsCount + " " + node.totalTime + " " + node.className);
                }
            }
        }
    }
}
