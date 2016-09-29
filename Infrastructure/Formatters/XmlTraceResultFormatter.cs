using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using Infrastructure.Interfaces;
using Infrastructure.Models;

namespace Infrastructure.Formatters
{
    public class XmlTraceResultFormatter : ITraceResultFormatter
    {
        public void Format(TraceResult result)
        {
            GenerateXml(result);
        }

        private void GenerateXml(TraceResult result)
        {
            var document = new XDocument();
            var root = new XElement("root");

            foreach (var thread in result.Children)
            {
                var threadElement = new XElement("thread");
                var idAttribute = new XAttribute("id", thread.Value.ModelId);
                threadElement.Add(idAttribute);
                var time = new XAttribute("time", (int) thread.Value.Time.TotalMilliseconds);
                threadElement.Add(time);
                ProcessThread(thread.Value, threadElement);
                root.Add(threadElement);
            }
            document.Add(root);
            Console.WriteLine(document.ToString());
            document.Save("results.xml");
        }

        private void ProcessThread(ThreadModel thread, XElement threadElement)
        {
            ProcessMethods(thread.Methods.ToList(), threadElement);
        }

        private void ProcessMethods(ICollection<MethodModel> methods, XElement parent)
        {
            foreach (var method in methods)
            {
                var methodElement = new XElement("method");
                var nameAttribute = new XAttribute("name", method.MethodName);
                var package = new XAttribute("package", method.ClassName);
                var time = new XAttribute("time", $"{(int) method.Time.TotalMilliseconds}ms");
                var paramsCount = new XAttribute("paramsCount", method.ParametersCount);
                methodElement.Add(nameAttribute);
                methodElement.Add(package);
                methodElement.Add(time);
                methodElement.Add(paramsCount);

                if (method.Children.Count > 0)
                    ProcessMethods(method.Children.ToList(), methodElement);

                parent.Add(methodElement);
            }
        }
    }
}