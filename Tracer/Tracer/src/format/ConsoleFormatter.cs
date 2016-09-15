
using System.Collections.Generic;
using MPPTracer.Tree;

namespace MPPTracer.Format
{
    public class ConsoleFormatter : IFormatter
    {

        private const string TAB = "    ";
        private const string THREAD_TAG = "thread id={0}\n";
        private const string METHOD_TAG = "method name={0} time={1}ms class={2} params={3}\n";

        public string Format(TraceResult traceResult)
        {
            string result = "";
            IEnumerator<KeyValuePair<int, ThreadNode>> enumerator = traceResult.GetEnumerator();
            while(enumerator.MoveNext())
            {
                int threadID = enumerator.Current.Key;
                ThreadNode thread = enumerator.Current.Value;
                result += string.Format(THREAD_TAG, threadID);
                result += CreateMethodTree(thread.GetFirstNestedMethod());
            }

            return result;

        }

        private string CreateMethodTree(MethodNode rootMethod)
        {
            string methodTree = "";
            IEnumerator<KeyValuePair<int, MethodDescriptor>> enumerator = rootMethod.GetEnumerator();
            while (enumerator.MoveNext())
            {
                int methodLevel = enumerator.Current.Key;
                MethodDescriptor descriptor = enumerator.Current.Value; ;
                string methodLine = string.Format(METHOD_TAG, descriptor.Name, descriptor.TraceTime, descriptor.ClassName, descriptor.ParamsNumber);
                methodTree += CreateIndent(methodLevel) + methodLine;
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
