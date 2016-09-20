using System.Collections.Immutable;
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
        private static object syncObj = new object();

		private IntermediateTraceResult result;

        private Tracer()
		{
			result = new IntermediateTraceResult();
		}

		public static Tracer Instance
		{
		    get
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
		}

		public void StartTrace()
		{
            var stackTrace = new StackTrace();
            var caller = stackTrace.GetFrame(1).GetMethod();

            var methodInfo = new TracedMethodInfo(
                    caller.Name,
                    caller.DeclaringType.ToString(),
                    Stopwatch.StartNew(),
                    caller.GetParameters().Length,
                    Thread.CurrentThread.ManagedThreadId);

            lock (syncObj)
			{	
				result.AddNode(methodInfo);
			}
		}

		public void StopTrace()
		{
			lock (syncObj)
			{
				result.PopNodeFromStack(Thread.CurrentThread.ManagedThreadId);
			}
		}

		public TraceResult GetTraceResult()
		{
			return new TraceResult(result.Threads);
		}
	}
}