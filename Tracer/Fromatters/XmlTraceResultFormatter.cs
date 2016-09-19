using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using Tracer.Interfaces;
using Tracer.Models;

namespace Tracer.Fromatters
{
    public class XmlTraceResultFormatter : ITraceResultFormatter
    {
        private readonly Stream _outStream;

        public XmlTraceResultFormatter(Stream outStream)
        {
            _outStream = outStream;
        }

        public void Format(TraceResult traceResult)
        {
            using (_outStream)
            {
                var document = new XDocument();
                document.Add(GetRootElement(traceResult));
                document.Save(_outStream);
            }
        }

        private static XElement GetRootElement(TraceResult traceResult)
        {
            var rootElement = new XElement("root");
            foreach (KeyValuePair<int, ThreadTraceResult> threadTraceInfo in traceResult.ThreadsTraceResult)
            {
                rootElement.Add(GetThreadElement(threadTraceInfo.Key, threadTraceInfo.Value));
            }
            return rootElement;
        }

        private static XElement GetThreadElement(int threadId, ThreadTraceResult threadTraceResult)
        {
            var threadElement = new XElement("thread");
            threadElement.Add(new XAttribute("id", threadId));
            threadElement.Add(new XAttribute("time", threadTraceResult.Duration));
            threadTraceResult.MethodsTraceResult.ForEach((method) => threadElement.Add(GetMethodElement(method)));
            return threadElement;
        }

        private static XElement GetMethodElement(MethodTraceResult methodTraceResult)
        {
            var methodElement = new XElement("method");
            methodElement.Add(new XAttribute("name", methodTraceResult.Name));
            methodElement.Add(new XAttribute("time", methodTraceResult.Duration));
            methodElement.Add(new XAttribute("class", methodTraceResult.ClassName));
            methodElement.Add(new XAttribute("params", methodTraceResult.ArgumentsCount));
            methodTraceResult.NestedMethodsTraceResult.ForEach((nested) => methodElement.Add(GetMethodElement(nested)));
            return methodElement;
        }
    }
}