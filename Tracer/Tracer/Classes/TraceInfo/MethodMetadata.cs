using System.Reflection;

namespace Trace.Classes.TraceInfo
{
    public class MethodMetadata
    {
        public string Name { get; }
        public string ClassName { get; }
        public int CountParameters { get; }

        public MethodMetadata(MethodBase methodBase)
        {
            ClassName = methodBase.DeclaringType?.ToString();
            CountParameters = methodBase.GetParameters().Length;
        }
    }
}
