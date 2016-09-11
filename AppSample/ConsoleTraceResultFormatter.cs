using System;

public class ConsoleTraceResultFormatter : ITraceResultFormatter
{
	const string PaddingSpace = "  ";
	const string PaddingEnd = "* ";

	public void Format(TraceResult traceResult)
	{
		var TracedThreadsData = traceResult.ThreadsData;

		foreach (var threadData in TracedThreadsData)
		{
			Console.WriteLine("Thread ID: {0}", threadData.Key);

			traceResult.GetThreadRootComponent(threadData.Key).Visit(Processor);
		}
	}

	public void Processor(TraceResult.TraceComponent component, int depth)
	{
		WritePadding(depth - 1);

		Console.WriteLine("Method \"{0}\" [Time: {1} ms]; class {2}, params: {3}",
			component.MethodName, component.ExecutionTime, component.ClassName, component.ParamCount);
	}

	public void WritePadding(int padding)
	{
		for (int i = 0; i < padding; i++)
		{
			Console.Write(PaddingSpace);
		}
		Console.Write(PaddingEnd);
	}
}
