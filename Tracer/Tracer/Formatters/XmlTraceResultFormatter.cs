using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml;
namespace Tracer.Formatters
{
    public  class XmlTraceResultFormatter : ITraceResultFormatter
    {
        private string resultFilePath;

        public XmlTraceResultFormatter()
        {
            resultFilePath = "TraceResult.xml";
        }

        public XmlTraceResultFormatter(string resultFilePath)
        {
            this.resultFilePath = resultFilePath;
        }

        public void Format(TraceResult traceResult)
        {
            XDocument resultFile = new XDocument();
            List<XElement> xmlTraceResult = TraceResultToXml(traceResult);

            AddMethodNesting(traceResult.callDepth, xmlTraceResult);           

            XElement rootElement = GenerateThreadMarkup(traceResult.threadIds, xmlTraceResult);

            resultFile.Add(rootElement);
            SaveToFile(resultFile, resultFilePath);
        }

        private void AddMethodNesting(int maximumDepth, List<XElement> xmlTraceResult)
        {
            for (int callDepth = maximumDepth - 1; callDepth >= 0; callDepth--)
            {
                foreach (XElement parentElement in xmlTraceResult.ToArray())
                {
                    NestChildren(parentElement, callDepth, xmlTraceResult);
                }
            }
        }

        private void NestChildren(XElement parentElement, int callDepth, List<XElement> xmlTraceResult)
        {
            int parentDepth = Int32.Parse(parentElement.Attribute("depth").Value);
            int parentThreadId = Int32.Parse(parentElement.Attribute("tid").Value);
            if (callDepth == parentDepth)
            {
                foreach (XElement childElement in xmlTraceResult.ToArray())
                {
                    ProcessPossibleChild(childElement, parentElement, parentThreadId, parentDepth, xmlTraceResult);
                }
            }
        }

        private void ProcessPossibleChild(XElement childElement, XElement parentElement, int parentThreadId, int parentDepth, List<XElement> xmlTraceResult)
        {
            int childDepth = Int32.Parse(childElement.Attribute("depth").Value);
            int childThreadId = Int32.Parse(childElement.Attribute("tid").Value);
            if (childDepth > parentDepth && childThreadId == parentThreadId)
            {
                childElement.Attribute("time").Value += 's';
                parentElement.Add(childElement);
                xmlTraceResult.Remove(childElement);

            }
        }

        private XElement GenerateThreadMarkup(List<int> threadIds, List<XElement> xmlTraceResult)
        {   
            XElement rootElement = new XElement("root");
            foreach (int id in threadIds)
            {
                XElement xmlThread = WrapThreadAroundMethods(id, xmlTraceResult);
                rootElement.Add(xmlThread);
            }
            RemoveAttributeFromDescendants(rootElement, "depth");
            RemoveAttributeFromDescendants(rootElement, "tid");
            return rootElement;
        }

        private XElement WrapThreadAroundMethods(int id, List<XElement> xmlTraceResult)
        {
            Double threadRuntime = 0;
            XElement xmlThread = new XElement("thread");
            xmlThread.Add(new XAttribute("id", id));
            foreach (XElement resultItem in xmlTraceResult)
            {
                if (id.Equals(Int32.Parse(resultItem.Attribute("tid").Value)))
                {
                    threadRuntime += Double.Parse(resultItem.Attribute("time").Value.Replace('.', ','));
                    resultItem.Attribute("time").Value += 's';
                    xmlThread.Add(resultItem);
                    
                }
            }
            xmlThread.Add(new XAttribute("time", threadRuntime));
            xmlThread.Attribute("time").Value += 's';
            return xmlThread;
        }

        private List<XElement> TraceResultToXml(TraceResult traceResult)
        {
            List<XElement> xmlTraceResult = new List<XElement>();
            foreach (TraceResultItem resultItem in traceResult)
            {
                xmlTraceResult.Add(MethodToXml(resultItem));
            }
            return xmlTraceResult;
        }

        private XElement MethodToXml(TraceResultItem resultItem)
        {
            XElement xmlMethod = new XElement("method");
            AddMainAttributes(xmlMethod, resultItem);
            AddServiceData(xmlMethod, resultItem);
            return xmlMethod;
        }

        private void AddMainAttributes(XElement element, TraceResultItem sourceItem)
        {
            element.Add(new XAttribute("name", sourceItem.methodName));
            element.Add(new XAttribute("time", sourceItem.time));
            element.Add(new XAttribute("class", sourceItem.className));
            element.Add(new XAttribute("args", sourceItem.parametersAmount));
        }

        private void AddServiceData( XElement element, TraceResultItem sourceItem)
        {
            element.Add(new XAttribute("tid", sourceItem.threadId));
            element.Add(new XAttribute("depth", sourceItem.callDepth));
        }

        private void RemoveAttributeFromDescendants(XElement element, string attribute)
        {
            foreach (XElement childElement in element.Descendants())
                if (childElement.Attribute(attribute) != null)
                    childElement.Attribute(attribute).Remove();
        }
        private void SaveToFile(XDocument xmlDocument, string path)
        {
            XmlWriterSettings settings = new XmlWriterSettings
            {
                Indent = true,
                IndentChars = "\t",
                OmitXmlDeclaration = true
            };
            using (XmlWriter writer = XmlWriter.Create(path, settings))
                xmlDocument.Save(writer);

        }

    }
}
