using System;
using System.Collections.Generic;
using System.IO;
using Tracer.TraceResultData;

namespace Tracer.Format
{
    public sealed class PlainTextTraceResultFormatter : ITraceResultFormatter
    {
        private readonly Stream stream;
        public PlainTextTraceResultFormatter(Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }
            this.stream = stream;
        }

        public void Format(TraceResult traceResult)
        {
            if (traceResult == null)
            {
                throw new ArgumentNullException(nameof(traceResult));
            }

            Dictionary<long, ThreadInfoResult> threadsInfo = traceResult.Value;
            using (StreamWriter writer = new StreamWriter(stream))
            {
                foreach (KeyValuePair<long, ThreadInfoResult> threadInfo in threadsInfo)
                {
                    writer.WriteLine("Thread id {0} time {1} ms", threadInfo.Key, threadInfo.Value.ExecutionTime);
                    FormatMethodsInfo(writer, threadInfo.Value.ChildMethods, 1);
                }
                writer.WriteLine("_______________________________________________");
            }
        }

        private void FormatMethodsInfo(StreamWriter writer, IEnumerable<MethodInfoResult> methodsInfo, int level)
        {
            string indent = new string('\t', level);
            foreach (MethodInfoResult methodInfo in methodsInfo)
            {
                writer.WriteLine("{0} method = {1}", indent, methodInfo.Name);
                writer.WriteLine("{0} class = {1}", indent, methodInfo.ClassName);
                writer.WriteLine("{0} time = {1}", indent, methodInfo.ExecutionTime);
                writer.WriteLine("{0} params = {1}", indent, methodInfo.ParamsCount);
                writer.WriteLine("{0}---", indent);
                FormatMethodsInfo(writer, methodInfo.ChildMethods, level+1);
            }
        }
    }
}
