using System.Collections.Generic;
using System.Xml.Serialization;

namespace Tracer.Models
{
    public class ThreadTraceResult
    {

        public ThreadTraceResult()
        {
            MethodsTraceResult = new List<MethodTraceResult>();    
        }

        [XmlAttribute("id")]
        public int Id { get; set; }
        [XmlAttribute("time")]
        public long Duration { get; set; }
        [XmlElement(ElementName = "method")]
        public List<MethodTraceResult> MethodsTraceResult { get; set; }

        public void AddMethodTraceResult(MethodTraceResult methodTraceResult)
        {
            MethodsTraceResult.Add(methodTraceResult);
            Duration += methodTraceResult.Duration;
        }

        public override string ToString()
        {
            return $"Thread [id={Id} time={Duration}]";
        }
    }
}