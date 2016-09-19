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
            foreach (KeyValuePair<int, ThreadTraceInfo> threadTraceInfo in traceResult.ThreadsTraceInfo)
            {
                rootElement.Add(GetThreadElement(threadTraceInfo.Key, threadTraceInfo.Value));
            }
            return rootElement;
        }

        private static XElement GetThreadElement(int threadId, ThreadTraceInfo threadTraceInfo)
        {
            var threadElement = new XElement("thread");
            threadElement.Add(new XAttribute("id", threadId));
            threadElement.Add(new XAttribute("time", threadTraceInfo.Duration));
            threadTraceInfo.MethodsTraceInfo.ForEach((method) => threadElement.Add(GetMethodElement(method)));
            return threadElement;
        }

        private static XElement GetMethodElement(MethodTraceInfo methodTraceInfo)
        {
            var methodElement = new XElement("method");
            methodElement.Add(new XAttribute("name", methodTraceInfo.Name));
            methodElement.Add(new XAttribute("time", methodTraceInfo.Duration));
            methodElement.Add(new XAttribute("class", methodTraceInfo.ClassName));
            methodElement.Add(new XAttribute("params", methodTraceInfo.ArgumentsCount));
            methodTraceInfo.NestedMethodsTraceInfo.ForEach((nested) => methodElement.Add(GetMethodElement(nested)));
            return methodElement;
        }
    }
}