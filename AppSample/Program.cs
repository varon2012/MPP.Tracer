using System;
using System.Threading;

internal class Program
{
	private static void Main(string[] args)
	{
		const int threadCount = 10;
		Thread[] threadPool = new Thread[threadCount];

		Tracer tracer = new Tracer();
		ThreadJob.UsedTracer = tracer;

		ThreadJob Job = new ThreadJob();

		int globalIndex;
		for (globalIndex = 0; globalIndex < threadCount / 2; globalIndex++)
		{
			threadPool[globalIndex] = new Thread(Job.Run);
			threadPool[globalIndex].Start();
		}

		Thread.Sleep(1500);

		for (; globalIndex < threadCount; globalIndex++)
		{
			threadPool[globalIndex] = new Thread(Job.Run);
			threadPool[globalIndex].Start();
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
