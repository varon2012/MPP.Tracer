using System.Collections.Generic;
using MPPTracer.Tree;
using System;
using System.Text;

namespace MPPTracer.Format
{
    public class ConsoleFormatter : IFormatter
    {
        private const char Tab = '\t';
        private const string ThreadTag = "thread id={0} time={1}ms";
        private const string MethodTag = "method name={0} time={1}ms class={2} params={3}";

        public string Format(TraceResult traceResult)
        {
            if (traceResult == null)
            {
                throw new ArgumentNullException(nameof(traceResult));
            }

            StringBuilder result = new StringBuilder();

            IEnumerator<ThreadNode> enumerator = traceResult.GetEnumerator();
            while(enumerator.MoveNext())
            {
                ThreadNode thread = enumerator.Current;
                string threadTag = string.Format(ThreadTag, thread.ID, thread.GetTraceTime());
                string methodTree = CreateMethodTree(thread.GetEnumerator(), 0);

                result.AppendLine(threadTag);
                result.AppendLine(methodTree);
            }

            return result.ToString();
        }

        private string CreateMethodTree(IEnumerator<MethodNode> enumerator, int nestingLevel)
        {
            StringBuilder result = new StringBuilder();

            while (enumerator.MoveNext())
            {
                MethodNode method = enumerator.Current;
                MethodDescriptor descriptor = method.Descriptor;
                string methodTag = string.Format(MethodTag, descriptor.Name, descriptor.TraceTime, descriptor.ClassName, descriptor.ParamsNumber);
                string methodTree = CreateMethodTree(method.GetEnumerator(), nestingLevel + 1);

                result.Append(Tab, nestingLevel);
                result.AppendLine(methodTag);
                result.Append(methodTree);
            }
            return result.ToString();
        }

    }
}
