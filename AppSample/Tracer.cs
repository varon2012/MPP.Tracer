using System.Diagnostics;
using System.Reflection;

public class Tracer : ITracer
{
	private TraceResult _result;

	public TraceResult Result
	{
		get
		{
			if (!_result.Validate())
			{
				throw new InvalidTraceException("Attempt to read trace result from tracer in invalid state!");
			}

			return _result;
		}
	}

	public Tracer()
	{
		_result = new TraceResult();
	}

	public void StartTrace()
	{
		var method = GetCurrentMethodInfo();
		_result.StartComponent(method.Name, method.GetParameters().Length, method.DeclaringType.Name);
	}

	public void StopTrace()
	{
		_result.StopComponent();
	}

	public MethodBase GetCurrentMethodInfo()
	{
		StackTrace stackTrace = new StackTrace();
		StackFrame callingFrame = stackTrace.GetFrame(2);
		return callingFrame.GetMethod();
	}

	public void SetThreadTime(long time)
	{
		_result.SetThreadTime(time);
	}
}
