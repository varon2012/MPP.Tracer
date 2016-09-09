using System.Diagnostics;
using System.Reflection;
using System.Threading;

public class Tracer : ITracer
{
	private TraceResult result = new TraceResult();

	public void StartTrace()
	{
		result.ThreadId = Thread.CurrentThread.ManagedThreadId;
		var method = GetCurrentMethodInfo();
		result.StartComponent(method.Name, method.GetParameters().Length, method.DeclaringType.Name);
	}

	public void StopTrace()
	{
		result.StopComponent();
	}

	public TraceResult GetTraceResult()
	{
		return result;
	}

	public static MethodBase GetCurrentMethodInfo()
	{
		StackTrace stackTrace = new StackTrace();
		StackFrame callingFrame = stackTrace.GetFrame(2);
		return callingFrame.GetMethod();
	}
}
