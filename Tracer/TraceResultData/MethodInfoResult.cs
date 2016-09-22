using System.Collections.Generic;

namespace Tracer.TraceResultData
{
    public class MethodInfoResult
    {
        public long ExecutionTime { get; set; }
        public int ParamsCount { get; set; }
        public string ClassName { get; set; }
        public string Name { get; set; }
        public List<MethodInfoResult> ChildMethods { get; set; }
    }
}
