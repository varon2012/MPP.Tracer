using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Tracer
{
    public class XmlFormatter
    {
        public XElement Format(TraceResult traceResult)
        {
            XElement root = new XElement("root");
            foreach (var threadItem in traceResult)
            {
                root.Add(CreateThreadNode(threadItem.Value));
            }
            return root;
        }

        private XElement CreateThreadNode(TraceResultItem threadItem)
        {
            XElement result =  new XElement("thread", new XAttribute("id", threadItem.ThreadId),
                                                      new XAttribute("time", $"{threadItem.Time} ms"));
            if (threadItem.Methods.Any())
                foreach (TraceMethodItem methodItem in threadItem.Methods)
                    result.Add(CreateMethodNode(methodItem));
            return result;
        }

        private XElement CreateMethodNode(TraceMethodItem methodItem)
        {
            XElement result = new XElement("method", new XAttribute("name", methodItem.Name),
                                                     new XAttribute("class", methodItem.ClassName),
                                                     new XAttribute("params", methodItem.ParamsCount),
                                                     new XAttribute("time", $"{methodItem.Time} ms"));
            if (methodItem.NestedMethods.Any())
                foreach (TraceMethodItem nestedMethod in methodItem.NestedMethods)
                    result.Add(CreateMethodNode(nestedMethod));
            return result;
        }
    }
}
