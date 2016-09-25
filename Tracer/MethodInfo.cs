using System.Diagnostics;

namespace Tracer
{
    public class MethodInfo
    {
        public string MethodName { get; set; }

        public string ClassName { get; set;}

        public int ParametersNumber { get; set; }

        public Stopwatch Watcher { get; set; }

        public MethodInfo(string methodName, string className, int parametersNumber, Stopwatch watcher)
        {
            MethodName = methodName;
            ClassName = className;
            ParametersNumber = parametersNumber;
            Watcher = watcher;
        }
    }
}
