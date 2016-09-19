using System.Collections.Generic;
using System.Reflection;

namespace Tracer.Models
{
    public class MethodTraceResult : TraceResult
    {
        public MethodTraceResult(MethodBase methodBase)
        {
            Name = methodBase.Name;
            ClassName = methodBase.DeclaringType?.Name;
            ArgumentsCount = methodBase.GetParameters().Length;
            NestedMethodsTraceInfo = new List<MethodTraceResult>();
        }

        public string Name { get; }
        public string ClassName { get; }
        public long Duration { get; set; }
        public int ArgumentsCount { get; }
        public List<MethodTraceResult> NestedMethodsTraceInfo { get; }

        public void AddNestedMethodResult(MethodTraceResult methodTraceResult)
        {
            NestedMethodsTraceInfo.Add(methodTraceResult);
        }

    }
}