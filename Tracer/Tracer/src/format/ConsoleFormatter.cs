
using System.Collections.Generic;
using MPPTracer.Tree;

namespace MPPTracer.Format
{
    public class ConsoleFormatter : IFormatter
    {

        private const string TAB = "    ";
        private const string THREAD_TAG = "thread id={0}\n";
        private const string METHOD_TAG = "method name={0} time={1}ms class={2} params={3}\n";


        private TreeRacer iterator = new TreeRacer();

        public string Format(TraceResult traceResult)
        {
            string result = "";
            for(int i = 0; i < traceResult.ThreadsCount; i++ )
            {
                int threadID = traceResult.GetThreadId(i);
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
                methodTree += CreateIndent(iterator.NestingLevel) + methodLine;
                descriptor = iterator.getNextDescriptor();
            }
            return methodTree;
        }

        private string CreateIndent(int nestingLevel)
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
