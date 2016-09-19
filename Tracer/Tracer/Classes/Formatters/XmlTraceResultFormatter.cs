using System.Collections.Generic;
using System.Xml.Linq;
using Trace.Classes.TraceInfo;
using Trace.Interfaces;

namespace Trace.Classes.Formatters
{
    public class XmlTraceResultFormatter : ITraceResultFormatter
    {

        private readonly string _filePathToSave;

        public XmlTraceResultFormatter(string filePathToSave)
        {
            _filePathToSave = filePathToSave;
        }

        public void Format(TraceResult traceResult)
        {
            var xDoc = new XDocument();
            var rootElement = new XElement("root");

            foreach (var threadInfo in traceResult.ThreadsInfo)
            {
                var threadElement = GetInfoThread(threadInfo);

                foreach (var methodInfo in threadInfo.Value.AllMethodsInfo)
                {
                    threadElement.Add(GetAllInfoMethod(methodInfo));
                }

                rootElement.Add(threadElement);
            }
            
            xDoc.Add(rootElement);
            xDoc.Save(_filePathToSave);
        }

        private XElement GetInfoThread(KeyValuePair<int, ThreadTrace> threadInfo)
        {
            var result = new XElement("thread");
            result.Add(new XAttribute("id", threadInfo.Key));
            result.Add(new XAttribute("time", threadInfo.Value.ExecutionTime));

            return result;
        }

        private XElement GetAllInfoMethod(MethodTrace methodTrace)
        {
            var result = GetInfoMethod(methodTrace);

            foreach (var nestedMethod in methodTrace.NestedMethods)
            {
                result.Add(GetAllInfoMethod(nestedMethod));
            }

            return result;
        }

        private XElement GetInfoMethod(MethodTrace methodTrace)
        {
            var result = new XElement("method");
            result.Add(new XAttribute("name", methodTrace.Metadata.Name));
            result.Add(new XAttribute("time", methodTrace.GetExecutionTime()));
            result.Add(new XAttribute("class", methodTrace.Metadata.ClassName));
            result.Add(new XAttribute("params", methodTrace.Metadata.CountParameters));

            return result;
        }
    }
}