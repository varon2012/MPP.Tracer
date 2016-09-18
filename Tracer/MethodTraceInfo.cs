using System.Collections.Generic;

namespace Tracer
{
    public class MethodTraceInfo
    {
        private List<MethodTraceInfo> nestedMethodsTraceInfo;

        public MethodTraceInfo()
        {
            nestedMethodsTraceInfo = new List<MethodTraceInfo>();
        }

        public string Name { get; set; }
        public string ClassName { get; set; }
        public long Duration { get; set; }
        public int ArgumentsCount { get; set; }
    }
}