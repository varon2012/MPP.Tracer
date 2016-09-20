using MPPTracer.Tree;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;

namespace MPPTracer.Format
{
    public class XMLFormatter : IFormatter
    {
        private const string Version = "1.0";
        private const string Encoding = "utf-8";
        private const string StandAlone = "yes";

        private const string RootTag = "root";
        private const string ThreadTag = "thread";
        private const string MethodTag = "method";

        private const string IdAttribute = "id";
        private const string NameAttribute = "name";
        private const string TimeAttribute = "time";
        private const string ClassAttribute = "class";
        private const string ParamsAttribute = "params";

        private string fileName;

        public XMLFormatter(string fileName)
        {
            this.fileName = fileName;
        }

        public string Format(TraceResult traceResult)
        {
            XDocument xmlDocument = CreateXMLDocument(traceResult);

            using (StreamWriter writer = File.CreateText(fileName))
            {
                xmlDocument.Save(writer);
            }
            return "Xml file path:\n"+Path.GetFullPath(fileName);
        }

        private XDocument CreateXMLDocument(TraceResult traceResult)
        {
            XDocument document = new XDocument();
            document.Declaration = new XDeclaration(Version, Encoding, StandAlone);
            XElement root = new XElement(RootTag);
            document.Add(root);

            List<XElement> threadElements = CreateThreadTree(traceResult.GetEnumerator());
            root.Add(threadElements);

            return document;
        }

        private List<XElement> CreateThreadTree(IEnumerator<ThreadNode> enumerator)
        {
            List<XElement> elements = new List<XElement>();
            while (enumerator.MoveNext())
            {
                ThreadNode thread = enumerator.Current;

                XAttribute[] attributes = {
                    new XAttribute(IdAttribute, thread.ID),
                    new XAttribute(TimeAttribute, thread.GetTraceTime())
                };
                XElement threadElement = new XElement(ThreadTag, attributes);
                List<XElement> methodElements = CreateMethodElements(thread.GetEnumerator());
                threadElement.Add(methodElements);

                elements.Add(threadElement);
            }

            return elements;
        }

        private List<XElement> CreateMethodElements(IEnumerator<MethodNode> enumerator)
        {
            List<XElement> elements = new List<XElement>();
            while (enumerator.MoveNext())
            {
                MethodNode method = enumerator.Current;
                MethodDescriptor descriptor = method.Descriptor;
                XAttribute[] attributes = {
                    new XAttribute(NameAttribute, descriptor.Name),
                    new XAttribute(TimeAttribute, descriptor.TraceTime),
                    new XAttribute(ClassAttribute, descriptor.ClassName),
                    new XAttribute(ParamsAttribute, descriptor.ParamsNumber)
                };

                XElement methodElement = new XElement(MethodTag, attributes);
                List<XElement> methodElements = CreateMethodElements(method.GetEnumerator());
                methodElement.Add(methodElements);

                elements.Add(methodElement);
            }

            return elements;
        }

    }
}
