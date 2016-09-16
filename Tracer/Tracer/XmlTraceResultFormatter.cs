using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    public class XmlTraceResultFormatter : ITraceResultFormatter
    {
        private string fileName;

        public XmlTraceResultFormatter(string fileName)
        {
            this.fileName = fileName;
        }

        public void Format(TraceResult traceResult)
        {
            XDocument xmlDocument = new XDocument();

            XElement root = new XElement("root");
            AddThreadNodes(root, traceResult);
            xmlDocument.Add(root);

            xmlDocument.Save(fileName);
        }

        private void AddThreadNodes(XElement root, TraceResult traceResult)
        {
            Dictionary<int, TraceResultThreadNode> threadNodes = traceResult.ThreadNodes;
            foreach (var threadId in threadNodes.Keys)
            {
                XElement thread = new XElement("thread");
                XAttribute id = new XAttribute("id", threadId);
                thread.Add(id);

                foreach (var node in threadNodes[threadId].MethodNodesList)
                {
                    AddMethodNode(thread, node);
                }

                root.Add(thread);
            }
        }

        private void AddMethodNode(XElement element, TraceResultMethodNode methodNode)
        {
            XElement method = new XElement("method");

            XAttribute methodName = new XAttribute("name", methodNode.MethodName);
            XAttribute className = new XAttribute("class", methodNode.ClassName);
            XAttribute parameters = new XAttribute("params", methodNode.ParamCount);

            double timeInMilliseconds = methodNode.TotalTime.TotalMilliseconds;
            XAttribute totalTime = new XAttribute("time", timeInMilliseconds);

            method.Add(methodName);
            method.Add(className);
            method.Add(parameters);
            method.Add(totalTime);

            foreach (var node in methodNode.InsertedNodes)
            {
                AddMethodNode(method, node);
            }

            element.Add(method);
        }
    }
}
