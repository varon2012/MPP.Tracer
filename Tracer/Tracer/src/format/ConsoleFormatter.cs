
using System.Collections.Generic;
using Tracer.Tree;

namespace Tracer.Format
{
    public class ConsoleFormatter : IFormatter
    {

        private const string TAB = "    ";
        private const string THREAD_TAG = "thread id={0}\n";
        private const string METHOD_TAG = "method name={0} time={1} class={2} params={3}\n";


        private Iterator iterator = new Iterator();

        public string Format(TraceResult traceResult)
        {
            string result = "";
            List<int> threads = traceResult.Threads;
            foreach(int threadID in threads)
            {
                result += string.Format(THREAD_TAG, threadID);
                result += CreateMethodTree(traceResult.GetThreadForest(threadID));
            }

            return result;

        }

        private string CreateMethodTree(List<MethodNode> methods)
        {
            string methodTree = "";
            iterator.MethodForest = methods;
            MethodDescriptor descriptor = iterator.getNextDescriptor();
            while (descriptor != null)
            {
                string methodLine = string.Format(METHOD_TAG, descriptor.Name, descriptor.TraceTime, descriptor.ClassName, descriptor.ParamsNumber);
                methodTree += GetIndent(iterator.NestingLevel) + methodLine;
            }
            return methodTree;
        }

        private string GetIndent(int nestingLevel)
        {
            string indent = TAB;
            for(int i = 0; i < nestingLevel; i++)
            {
                indent += TAB;
            }
            return indent;
        }


    }
}
