using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TracerAPI
{
    public class XMLFormatter
    {
        private XElement Root;

        public void Format(TraceResult traceResult) 
        {
            Root = new XElement("Threads");
            foreach(int key in traceResult.Result.Keys)
            {
                XElement Thread = new XElement("Thread", new XAttribute("id", key));
                Root.Add(Thread);
                XElement Method = new XElement("Method");
                Thread.Add(Method);

                Method.SetAttributeValue("name",traceResult[key].Root.MethodName);
                Method.SetAttributeValue("num-of-param", traceResult[key].Root.NumberOfParameters);
                Method.SetAttributeValue("method-class-name", traceResult[key].Root.MethodClassName);
                Method.SetAttributeValue("time", traceResult[key].Root.WholeTime);

                AddChildrenToXElement(Method, traceResult[key].Root);
            }
            Root.Save("ThreadsTree.xml");
        }

        private void AddChildrenToXElement(XElement xElement, Node tempParent)
        {
            foreach (Node child in tempParent.Children)
            {
                XElement Method = new XElement("Method");
                xElement.Add(Method);
                Method.SetAttributeValue("name", tempParent.MethodName);
                Method.SetAttributeValue("num-of-param", tempParent.NumberOfParameters);
                Method.SetAttributeValue("method-class-name", tempParent.MethodClassName);
                Method.SetAttributeValue("time", tempParent.WholeTime);
                if (child.Children.Count > 0)
                {
                    AddChildrenToXElement(Method, child);
                }
            }
        }
    }
}
