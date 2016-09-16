using MPPTracer.Tree;
using System;
using System.Collections.Generic;

namespace MPPTracer.Format
{
    public class XMLFormatter : IFormatter
    {
        private const string TAB = "    ";
        private const string NL = "\n";
        private const string ROOT_OPEN_TAG = "<root>\n";
        private const string ROOT_CLOSE_TAG = "</root>\n";
        private const string THREAD_OPEN_TAG = "<thread id={0}>\n";
        private const string THREAD_CLOSE_TAG = "</thread>\n";
        private const string METHOD_OPEN_TAG = "<method name={0} time={1} class={2} params={3}>\n";
        private const string METHOD_CLOSE_TAG = "</method>\n";

        public string Format(TraceResult traceResult)
        {
            return CreateRootTag(traceResult);
        }

        private string CreateRootTag(TraceResult traceResult)
        {
            return ROOT_OPEN_TAG + CreateThreadTagList(traceResult) + ROOT_CLOSE_TAG;
        }

        private string CreateThreadTagList(TraceResult traceResult)
        {
            string tagList = "";
            IEnumerator<KeyValuePair<int, ThreadNode>> enumerator = traceResult.GetEnumerator();
            while (enumerator.MoveNext())
            {
                ThreadNode thread = enumerator.Current.Value;
                int threadId = enumerator.Current.Key;
                string threadTag = TAB + string.Format(THREAD_OPEN_TAG, threadId);

                threadTag += CreateMethodTagList(thread.GetFirstNestedMethod(), 2);

                threadTag += TAB + THREAD_CLOSE_TAG;
                tagList += threadTag;
            }

            return tagList;
        }

        private string CreateMethodTagList(MethodNode rootMethod, int nestingLevel)
        {
            string tagList = "";
            while(rootMethod != null)
            {
                MethodDescriptor descriptor = rootMethod.Descriptor;
                string indent = CreateIndent(nestingLevel);

                string methodTag = indent + string.Format(METHOD_OPEN_TAG, descriptor.Name, descriptor.TraceTime, descriptor.ClassName, descriptor.ParamsNumber);
                methodTag += CreateMethodTagList(rootMethod.GetFirstNestedMethod(), nestingLevel + 1);
                methodTag += indent + METHOD_CLOSE_TAG;
                tagList += methodTag;
                rootMethod = rootMethod.GetNextAddedMethod();
            }
            
            return tagList;
        }

        private string CreateIndent(int nestingLevel)
        {
            string indent = "";
            for (int i = 0; i < nestingLevel; i++)
            {
                indent += TAB;
            }
            return indent;
        }
    }
}
