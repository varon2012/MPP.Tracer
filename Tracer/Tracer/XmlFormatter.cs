using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Tracer
{
    public class XmlFormatter : IFormatter
    {
        private XmlDocument document;
        private XmlTextWriter textWriter;
        private string destination;
        public XmlFormatter(string path)
        {
            destination = path;
            textWriter = new XmlTextWriter(path, Encoding.UTF8);
            textWriter.WriteStartDocument();
            textWriter.WriteStartElement("root");
            textWriter.WriteEndElement();
            textWriter.Close();
        }

        public void Format(TraceResult traceResult)
        {
            document = new XmlDocument();
            document.Load(destination);
            foreach (var mainMethod in traceResult.MethodInfoDictionary)
            {
                XmlNode thread = CreateNode("thread");
                AddAttribute("id", mainMethod.Key, thread);
                AddAttribute("time", mainMethod.Value.Node.Watcher.ElapsedMilliseconds, thread);
                AddElementToXml(mainMethod.Value, thread);
            }
            document.Save(destination);
        }

        private void AddElementToXml(Tree<MethodInfo> method, XmlNode parent)
        {
            XmlNode node = CreateNode("method");
            AddAttribute("name", method.Node.MethodName, node);
            AddAttribute("time", method.Node.Watcher.ElapsedMilliseconds, node);
            AddAttribute("class", method.Node.ClassName, node);
            AddAttribute("parametres", method.Node.NumberParametres, node);
            parent.AppendChild(node);

            foreach (var nodeMethod in method.NodesList)
            {
                AddElementToXml(nodeMethod, node);
            }
        }

        private XmlNode CreateNode(string name)
        {
            XmlNode node = document.CreateElement(name);
            document.DocumentElement.AppendChild(node);
            return node;
        }

        private void AddAttribute(string name, object value, XmlNode parent)
        {
            XmlAttribute attribute = document.CreateAttribute(name);
            attribute.Value = value.ToString();
            parent.Attributes.Append(attribute);
        }
    }
}
