using System.Diagnostics;
using System.Threading;
using TracerLib.Interfaces;
using TracerLib.Utils;

namespace TracerLib
{
	public class Tracer : ITracer
	{
		private static Tracer instance;

		private static object sync = new object();
		private static object stopMethodSync = new object();
        private static object startMethodSync = new object();

		private TraceResult result;

		private Tracer()
		{
			result = new TraceResult();
		}

		public static Tracer GetInstance()
		{
			if (instance == null)
			{
				lock (sync)
				{
					if (instance == null)
					{
						instance = new Tracer();
					}
				}
			}

			return instance;
		}

		public void StartTrace()
		{
			lock (startMethodSync)
			{
				var stackTrace = new StackTrace();
				var caller = stackTrace.GetFrame(1).GetMethod();

				var methodInfo = new TracedMethodInfo(
						caller.Name,
						caller.DeclaringType.ToString(), 
						Stopwatch.StartNew(), 
						caller.GetParameters().Length, 
						Thread.CurrentThread.ManagedThreadId);
				
				result.AddNode(methodInfo);
			}
		}

		public void StopTrace()
		{
			lock (stopMethodSync)
			{
				result.PopNodeFromStack(Thread.CurrentThread.ManagedThreadId);
			}
		}

		public TraceResult GetTraceResult()
		{
			return result;
		}
	}
}