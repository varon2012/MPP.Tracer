using System.Collections.Generic;

namespace Tracer.TraceResultData
{
    public class ThreadInfoResult
    {
        public long ExecutionTime { get; set; }
        public List<MethodInfoResult> ChildMethods { get; set; }
    }
}
