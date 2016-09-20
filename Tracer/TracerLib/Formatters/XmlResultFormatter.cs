using System.IO;
using System.Xml.Linq;
using TracerLib.Interfaces;
using TracerLib.Utils;

namespace TracerLib.Formatters
{
    public class XmlResultFormatter : ITraceResultFormatter
    {
        private readonly string fileName;

        public XmlResultFormatter(string file)
        {
            fileName = file;
        }

        public void Format(TraceResult traceResult)
        {
            var document = new XDocument();
            var root = new XElement("root");

            var threads = traceResult.Results;

            foreach (var Id in threads.Keys)
            {
                var thread = new XElement("thread", new XAttribute("id", Id));
                PrintMethodResults(thread, threads[Id].HeadNode);
                root.Add(thread);
            }

            document.Add(root);
            document.Save(fileName);
        }

        private static void PrintMethodResults(XElement thread, Node<TracedMethodInfo> node)
        {
            var methodInfo = new XElement("method",
                                            new XAttribute("name", node.Item.MethodName),
                                            new XAttribute("time", node.Item.Watcher.ElapsedMilliseconds + "ms"),
                                            new XAttribute("class", node.Item.ClassName),
                                            new XAttribute("params", node.Item.ArgumentsNumber));
            thread.Add(methodInfo);

            if (node.Children.Count > 0)
            {
                foreach (var child in node.Children)
                {
                    PrintMethodResults(methodInfo, child);
                }
            }

        }

    }
}