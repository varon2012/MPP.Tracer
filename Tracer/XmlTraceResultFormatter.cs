using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Tracer
{
    public class XmlTraceResultFormatter : ITraceResultFormatter
    {
        private Stream stream;
        public XmlTraceResultFormatter(Stream stream)
        {
            this.stream = stream;
        }

        public void Format(TraceResult traceResult)
        {
            if (traceResult == null) return;
            XDocument xmlDoc = new XDocument();
            XElement rootElement = new XElement("root");

            IEnumerable threadsInfo = traceResult.ThreadInfoDictionary;
            foreach (KeyValuePair<long, ThreadInfo> threadInfo in threadsInfo)
            {
                XElement threadElement = new XElement("thread");
                threadElement.Add(new XAttribute("id", threadInfo.Key),
                    new XAttribute("time", threadInfo.Value.ExecutionTime));
                FormatMethodsInfo(threadElement, threadInfo.Value.MethodsInfo);
                rootElement.Add(threadElement);
            }
            xmlDoc.Add(rootElement);
            xmlDoc.Save(stream);
        }

        private void FormatMethodsInfo(XElement parentElement, IEnumerable methodsInfo)
        {
            foreach (MethodInfo methodInfo in (List<MethodInfo>)methodsInfo)
            {
                XElement element = new XElement("method");

                element.Add(new XAttribute("name", methodInfo.Name));
                element.Add(new XAttribute("class", methodInfo.ClassName));
                element.Add(new XAttribute("time", methodInfo.ExecutionTime));
                element.Add(new XAttribute("params", methodInfo.ParamsCount));
                parentElement.Add(element);
                FormatMethodsInfo(element, methodInfo.MethodsInfo);
            }
        }
    }
}
