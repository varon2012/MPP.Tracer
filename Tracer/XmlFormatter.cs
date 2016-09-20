using System.Xml.Linq;

namespace Tracer
{
    public class XmlFormatter : ITraceResultFormatter
    {
        public void Format(TraceResult traceResult)
        {
            var document = new XDocument();
            var root = new XElement("root");

            foreach (var element in traceResult.TraceTree)
            {
                var thread = new XElement("thread", new XAttribute("id", element.Key));
                foreach (var node in element.Value)
                {
                    AddMethodToXmlTree(thread, node);
                }
                root.Add(thread);
            }

            document.Add(root);
            document.Save("D:\\batka.xml");
        }

        private void AddMethodToXmlTree(XElement parentElement, MethodsTreeNode methodNode)
        {
            var methodInfo = new XElement("method",
                new XAttribute("name", methodNode.Method.MethodName),
                new XAttribute("time", methodNode.Method.Watcher.ElapsedMilliseconds + "ms"),
                new XAttribute("class", methodNode.Method.ClassName),
                new XAttribute("params", methodNode.Method.ParametersNumber));

            parentElement.Add(methodInfo);

            foreach (var child in methodNode.Children)
            {
                AddMethodToXmlTree(methodInfo, child);
            }
        }
    }
}