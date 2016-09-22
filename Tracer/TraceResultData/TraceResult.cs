using System.Collections.Generic;

namespace Tracer.TraceResultData
{
    public class TraceResult
    {
        public Dictionary<long, ThreadInfoResult> Value { get; set; }
    }
}
