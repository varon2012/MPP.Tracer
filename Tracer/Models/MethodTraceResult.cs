using System.Collections.Generic;
using System.Reflection;

namespace Tracer.Models
{
    public class MethodTraceResult
    {
        public MethodTraceResult(MethodBase methodBase)
        {
            Name = methodBase.Name;
            ClassName = methodBase.DeclaringType?.Name;
            ArgumentsCount = methodBase.GetParameters().Length;
            NestedMethodsTraceResult = new List<MethodTraceResult>();
        }

        public string Name { get; }
        public string ClassName { get; }
        public long Duration { get; set; }
        public int ArgumentsCount { get; }
        public List<MethodTraceResult> NestedMethodsTraceResult { get; }

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