using System.Collections.Generic;

namespace Tracer
{
    public class ThreadTraceInfo
    {
        private List<MethodTraceInfo> methodsTraceInfo;

        public ThreadTraceInfo()
        {
            methodsTraceInfo = new List<MethodTraceInfo>();
        }

        public long Duration { get; set; }
        public List<MethodTraceInfo> MethodsTraceInfo => methodsTraceInfo;
    }
}