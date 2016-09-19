using System.Diagnostics;

namespace TracerLib.Utils
{
	internal class TracedMethodInfo
	{
        internal string MethodName { get; set; }

        internal string ClassName { get; set; }

        internal Stopwatch Watcher { get; set; }

        internal int ArgumentsNumber { get; set; }

        internal int ThreadId { get; set; }

        internal TracedMethodInfo(string methodName, string className, Stopwatch watcher, int argumentsNumber, int threadId)
		{
			MethodName = methodName;
			ClassName = className;
			Watcher = watcher;
			ArgumentsNumber = argumentsNumber;
			ThreadId = threadId;
		}
	}
}