
using System.Collections.Generic;
using MPPTracer.Tree;

namespace MPPTracer.Format
{
    public class ConsoleFormatter : Formatter
    {

        private const string THREAD_TAG = "thread id={0}"+NL;
        private const string METHOD_TAG = "method name={0} time={1}ms class={2} params={3}"+NL;

        public override string Format(TraceResult traceResult)
        {
            string result = "";
            IEnumerator<KeyValuePair<int, ThreadNode>> enumerator = traceResult.GetEnumerator();
            while(enumerator.MoveNext())
            {
                int threadID = enumerator.Current.Key;
                ThreadNode thread = enumerator.Current.Value;
                result += string.Format(THREAD_TAG, threadID);
                result += CreateMethodTree(thread.GetFirstNestedMethod(), 0);
            }

            return result;

        }

        private string CreateMethodTree(MethodNode rootMethod, int nestingLevel)
        {
            string tagList = "";
            while (rootMethod != null)
            {
                MethodDescriptor descriptor = rootMethod.Descriptor;
                string indent = CreateIndent(nestingLevel);

                string methodTag = indent + string.Format(METHOD_TAG, descriptor.Name, descriptor.TraceTime, descriptor.ClassName, descriptor.ParamsNumber);
                methodTag += CreateMethodTree(rootMethod.GetFirstNestedMethod(), nestingLevel + 1);
                tagList += methodTag;
                rootMethod = rootMethod.GetNextAddedMethod();
            }

            return tagList;
        }

    }
}
