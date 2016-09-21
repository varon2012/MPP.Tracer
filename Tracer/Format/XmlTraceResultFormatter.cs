using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using Tracer.TraceResultData;

namespace Tracer.Format
{
    public sealed class XmlTraceResultFormatter : ITraceResultFormatter
    {
        private readonly Stream stream;
        public XmlTraceResultFormatter(Stream stream)
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
            
            XDocument xmlDoc = new XDocument();
            XElement rootElement = new XElement("root");

            Dictionary<long,ThreadInfoResult> threadsInfo = traceResult.Value;
            foreach (KeyValuePair<long, ThreadInfoResult> threadInfo in threadsInfo)
            {
                XElement threadElement = new XElement("thread");
                threadElement.Add(new XAttribute("id", threadInfo.Key),
                    new XAttribute("time", threadInfo.Value.ExecutionTime));
                FormatMethodsInfo(threadElement, threadInfo.Value.ChildMethods);
                rootElement.Add(threadElement);
            }
            xmlDoc.Add(rootElement);
            xmlDoc.Save(stream);
        }

        private void FormatMethodsInfo(XElement parentElement, IEnumerable<MethodInfoResult> methodsInfo)
        {
            foreach (MethodInfoResult methodInfo in methodsInfo)
            {
                XElement element = new XElement("method");

                element.Add(new XAttribute("name", methodInfo.Name));
                element.Add(new XAttribute("class", methodInfo.ClassName));
                element.Add(new XAttribute("time", methodInfo.ExecutionTime));
                element.Add(new XAttribute("params", methodInfo.ParamsCount));
                parentElement.Add(element);
                FormatMethodsInfo(element, methodInfo.ChildMethods);
            }
        }
    }
}
