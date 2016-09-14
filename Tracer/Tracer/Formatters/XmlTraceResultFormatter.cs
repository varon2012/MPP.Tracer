using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Tracer.Formatters
{
    public  class XmlTraceResultFormatter : ITraceResultFormatter
    {
        private XElement rootElement = new XElement("root");
        private List<XElement> xmlTraceResult = new List<XElement>();

        public void Format(TraceResult traceResult)
        {
            XDocument resultFile = new XDocument();

            foreach (TraceResultItem resultItem in traceResult)
            {
                xmlTraceResult.Add(ConvertToXmlElement(resultItem));
            }
            
            AddElements(traceResult);

            resultFile.Add(rootElement);
            resultFile.Save("TraceResult.xml");
        }

        private XElement ConvertToXmlElement(TraceResultItem resultItem)
        {
            XElement xResultItem = new XElement("method");
            xResultItem.Add(new XAttribute("name", resultItem.methodName));
            xResultItem.Add(new XAttribute("time", resultItem.time * 0.001));
            xResultItem.Add(new XAttribute("class", resultItem.className));
            xResultItem.Add(new XAttribute("args", resultItem.parametersAmount));
            return xResultItem;
        }
        private void AddElements(TraceResult traceResult)
        {
            foreach (int id in traceResult.threadIds)
            {
                long threadRuntime = 0;

                XElement xmlThread = new XElement("thread");
                xmlThread.Add(new XAttribute("id", id));

                foreach (TraceResultItem resultItem in traceResult)
                {
                    if (id.Equals(resultItem.threadId))
                    {
                        xmlThread.Add(ConvertToXmlElement(resultItem));
                        threadRuntime += resultItem.time;
                    }                        
                }

                xmlThread.Add(new XAttribute("time", threadRuntime*0.001));
                rootElement.Add(xmlThread);
            }
        }
    }
}
