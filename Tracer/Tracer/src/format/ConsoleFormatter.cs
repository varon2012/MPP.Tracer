using System.Collections.Generic;
using MPPTracer.Tree;
using System;

namespace MPPTracer.Format
{
    public class ConsoleFormatter : IFormatter
    {
        private const char Tab = '\t';
        private string NL = Environment.NewLine;
        private const string ThreadTag = "thread id={0}";
        private const string MethodTag = "method name={0} time={1}ms class={2} params={3}";

        public string Format(TraceResult traceResult)
        {
            if (traceResult == null)
            {
                throw new ArgumentNullException(nameof(traceResult));
            }

            string result = string.Empty;
            IEnumerator<ThreadNode> enumerator = traceResult.GetEnumerator();
            while(enumerator.MoveNext())
            {
                ThreadNode thread = enumerator.Current;
                result += string.Format(ThreadTag, thread.ID) + NL;
                result += CreateMethodTree(thread.GetEnumerator(), 0)+ NL;
            }

            return result;
        }

        private string CreateMethodTree(IEnumerator<MethodNode> enumerator, int nestingLevel)
        {
            string indent = new string(Tab, nestingLevel);
            string tagList = string.Empty;
            while (enumerator.MoveNext())
            {
                MethodNode method = enumerator.Current;
                MethodDescriptor descriptor = method.Descriptor;

                string methodTag = indent + string.Format(MethodTag, descriptor.Name, descriptor.TraceTime, descriptor.ClassName, descriptor.ParamsNumber) + NL;
                methodTag += CreateMethodTree(method.GetEnumerator(), nestingLevel + 1);
                tagList += methodTag;
            }
            return tagList;
        }

    }
}
