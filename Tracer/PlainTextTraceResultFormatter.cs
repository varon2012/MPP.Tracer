using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    public class PlainTextTraceResultFormatter : ITraceResultFormatter
    {
        private Stream stream;
        public PlainTextTraceResultFormatter(Stream stream)
        {
            this.stream = stream;
        }

        public void Format(TraceResult traceResult)
        {
            if (traceResult == null) return;

            IEnumerable threadsInfo = traceResult.ThreadInfoDictionary;
            using (StreamWriter writer = new StreamWriter(stream))
            {
                foreach (KeyValuePair<long, ThreadInfo> threadInfo in threadsInfo)
                {
                    writer.WriteLine("Thread id {0} time {1} ms", threadInfo.Key, threadInfo.Value.ExecutionTime);
                    FormatMethodsInfo(writer, threadInfo.Value.MethodsInfo, 1);
                }
                writer.WriteLine("_______________________________________________");
            }
        }

        private void FormatMethodsInfo(StreamWriter writer, IEnumerable methodsInfo, int level)
        {
            string indent = new string('\t', level);
            foreach (MethodInfo methodInfo in (List<MethodInfo>)methodsInfo)
            {
                writer.WriteLine("{0} method = {1}", indent, methodInfo.Name);
                writer.WriteLine("{0} class = {1}", indent, methodInfo.ClassName);
                writer.WriteLine("{0} time = {1}", indent, methodInfo.ExecutionTime);
                writer.WriteLine("{0} params = {1}", indent, methodInfo.ParamsCount);
                writer.WriteLine("{0}---", indent);
                FormatMethodsInfo(writer, methodInfo.MethodsInfo, level+1);
            }
        }
    }
}
