using System.Collections.Generic;
using System.Xml.Serialization;

namespace Tracer.Models
{
    public class MethodTraceResult
    {
        public MethodTraceResult()
        {
            NestedMethodsTraceResult = new List<MethodTraceResult>();
        }

        [XmlAttribute("name")]
        public string Name { get; set; }
        [XmlAttribute("class")]
        public string ClassName { get; set; }
        [XmlAttribute("time")]
        public long Duration { get; set; }
        [XmlAttribute("params")]
        public int ArgumentsCount { get; set; }
        [XmlElement(ElementName = "method")]
        public List<MethodTraceResult> NestedMethodsTraceResult { get; set; }
         
        public void AddNestedMethodResult(MethodTraceResult methodTraceResult)
        {
            NestedMethodsTraceResult.Add(methodTraceResult);
        }

        public override string ToString()
        {
            return $"Method [name={Name} class={ClassName} time={Duration} params={ArgumentsCount}]";
        }
    }
}