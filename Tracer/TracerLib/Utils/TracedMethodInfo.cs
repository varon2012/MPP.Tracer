using System.Diagnostics;

namespace TracerLib.Utils
{
	public class TracedMethodInfo
	{
		public string MethodName { get; set; }

		public string ClassName { get; set; }
		
		public Stopwatch Watcher { get; set; }
		
		public int ArgumentsNumber { get; set; }
		
		public int ThreadId { get; set; }

		public TracedMethodInfo(string methodName, string className, Stopwatch watcher, int argumentsNumber, int threadId)
		{
			MethodName = methodName;
			ClassName = className;
			Watcher = watcher;
			ArgumentsNumber = argumentsNumber;
			ThreadId = threadId;
		}
	}
}