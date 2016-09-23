using System.Diagnostics;

public class TraceComponent
{
	private Stopwatch _watch;

	public string MethodName { get; set; }
	public string ClassName { get; set; }
	public int ExecutionTime { get; set; }
	public int ParamCount { get; set; }
	public Stopwatch Watch
	{
		get
		{
			return _watch;
		}
	}
	
	public TraceComponent()
	{
		_watch = new Stopwatch();
	}
}
