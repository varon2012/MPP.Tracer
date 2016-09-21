using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Tracer.Formatters
{
    public class XMLTraceResultFormatter : ITraceResultFormatter
    {
        private string fileName;

        public XMLTraceResultFormatter(String path)
        {
            fileName = path;
        }

        public void Format(TraceResult traceResult)
        {
            var document = new XDocument();
            var root = new XElement("root");
            List<ThreadNode> treadNodeList = traceResult.threadList.Values.ToList();

            foreach (var element in treadNodeList)
            {
                var thread = new XElement("thread", new XAttribute("id", element.ID));
                AddMethodsToThread(thread, element.MethodsTree, 1, 0);
                root.Add(thread);
            }

            document.Add(root);
            document.Save(fileName);
        }

        private static int AddMethodsToThread(XElement parent, List<MethodNode> methods,int height, int currentNumberOfMethod)
        {
            XElement previousElement = AddMethod(parent, methods.ElementAt(currentNumberOfMethod));
            currentNumberOfMethod++;
            int i = currentNumberOfMethod;
            while(i < methods.Count())
            {
                if (methods.ElementAt(i).Heignt == height)
                {
                    previousElement = AddMethod(parent, methods.ElementAt(i));
                    i++;
                }
                else
                {
                    if(methods.ElementAt(i).Heignt > height)
                    {
                        i = AddMethodsToThread(previousElement, methods, height + 1, i);
                    }
                    else
                    {
                        return i;
                    }
                }
            }
            return i;
        }

        private static XElement AddMethod(XElement parent, MethodNode child)
        {
            var methodInfo = new XElement("method",
                                            new XAttribute("name", child.Info.Name),
                                            new XAttribute("time", child.Info.Time + "ms"),
                                            new XAttribute("class", child.Info.ClassName),
                                            new XAttribute("params", child.Info.ParamsNumber));
            parent.Add(methodInfo);
            return methodInfo;
        }
    }
}
