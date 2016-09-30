using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace TracerAPI
{
    public class XMLFormatter: ITraceResultFormatter
    {
        private XElement Root;

        private string fileName;

        public XMLFormatter(string name)
        {
            fileName = name;
        }

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
                Method.SetAttributeValue("parameters", traceResult[key].Root.NumberOfParameters);
                Method.SetAttributeValue("class-name", traceResult[key].Root.MethodClassName);
                Method.SetAttributeValue("time", traceResult[key].Root.WholeTime);

                AddChildrenToXElement(Method, traceResult[key].Root);
            }
            Root.Save(fileName);
        }

        private void AddChildrenToXElement(XElement xElement, Node tempParent)
        {
            foreach (Node child in tempParent.Children)
            {
                XElement Method = new XElement("Method");
                xElement.Add(Method);
                Method.SetAttributeValue("name", child.MethodName);
                Method.SetAttributeValue("parameters", child.NumberOfParameters);
                Method.SetAttributeValue("class-name", child.MethodClassName);
                Method.SetAttributeValue("time", child.WholeTime);
                if (child.Children.Count > 0)
                {
                    AddChildrenToXElement(Method, child);
                }
            }
        }
    }
}
