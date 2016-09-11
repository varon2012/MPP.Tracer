using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Tracer
{
    public static class XmlFormatter
    {
        public static XElement Format(TraceResult traceResult)
        {
            XElement root = new XElement("root");
            foreach (TraceResultItem threadItem in traceResult)
            {
                root.Add(createThreadNode(threadItem));
            }
            return root;
        }

        private static XElement createThreadNode(TraceResultItem threadItem)
        {
            XElement result =  new XElement("thread", new XAttribute("id", threadItem.ThreadId),
                                                      new XAttribute("time", $"{threadItem.Time} ms"));
            if (threadItem.Methods != null)
                foreach (TraceMethodItem methodItem in threadItem.Methods)
                    result.Add(createMethodNode(methodItem));
            return result;
        }

        private static XElement createMethodNode(TraceMethodItem methodItem)
        {
            XElement result = new XElement("method", new XAttribute("name", methodItem.Name),
                                                     new XAttribute("class", methodItem.ClassName),
                                                     new XAttribute("params", methodItem.ParamsCount),
                                                     new XAttribute("time", $"{methodItem.Time} ms"));
            if (methodItem.NestedMethods != null)
                foreach (TraceMethodItem nestedMethod in methodItem.NestedMethods)
                    result.Add(createMethodNode(nestedMethod));
            return result;
        }
    }
}
