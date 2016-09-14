using System;
using System.Threading;

internal class Program
{
	private static void Main(string[] args)
	{
		const int threadCount = 5;
		Thread[] threadPool = new Thread[threadCount];

		Tracer tracer = new Tracer();
		ThreadJob.UsedTracer = tracer;

		ThreadJob Job = new ThreadJob();

		for (int i = 0; i < threadCount; i++)
		{
			threadPool[i] = new Thread(Job.Run);
			threadPool[i].Start();
		}

		foreach (var thread in threadPool)
		{
			thread.Join();
		}

		// Print result
		var result = tracer.Result;

		int xmlArgIndex = Array.FindIndex(args, x => x.Equals("-xml"));
		const int notFoundIndex = -1;

		if (xmlArgIndex != notFoundIndex)
		{
			var xmlFormatter = new XmlTraceResultFormatter();
			xmlFormatter.Format(result);
		}

		var consoleFormatter = new ConsoleTraceResultFormatter();
		consoleFormatter.Format(result);

		Console.ReadLine();
	}
}
