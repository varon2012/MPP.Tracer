using System.Collections.Generic;
using System.Xml.Serialization;

namespace Tracer.Models
{
    [XmlRoot(ElementName = "root")]
    public class TraceResult
    {

        public TraceResult()
        {
            ThreadsTraceResult = new List<ThreadTraceResult>();    
        }

        [XmlElement(ElementName = "thread")]
        public List<ThreadTraceResult> ThreadsTraceResult { get; set; }
    }
}