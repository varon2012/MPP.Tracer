using System.Diagnostics;
using System.Reflection;

public class Tracer : ITracer
{
	private TraceResult result = new TraceResult();

	private object ValidatorLock = new object();
	private object StackFrameGetterLock = new object();

	public void StartTrace()
	{
		var method = GetCurrentMethodInfo();
		result.StartComponent(method.Name, method.GetParameters().Length, method.DeclaringType.Name);
	}

	public void StopTrace()
	{
		result.StopComponent();
	}

	public TraceResult GetTraceResult()
	{
		lock (ValidatorLock)
		{
			if (!result.Validate())
			{
				throw new InvalidTraceException("Attempt to read trace result from tracer in invalid state!");
			}

			return result;
		}
	}

	public MethodBase GetCurrentMethodInfo()
	{
		lock (StackFrameGetterLock)
		{
			StackTrace stackTrace = new StackTrace();
			StackFrame callingFrame = stackTrace.GetFrame(2);
			return callingFrame.GetMethod();
		}
	}
}
