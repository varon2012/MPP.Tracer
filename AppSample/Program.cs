using System;
using System.Threading;

class Program
{
	const int ThreadCount = 5;
	private static Thread[] ThreadPool = new Thread[ThreadCount];

	static void Main(string[] args)
	{
		Tracer MyTracer = new Tracer();
		ThreadJob.UsedTracer = MyTracer;

		ThreadJob Job = new ThreadJob();

		for (int i = 0; i < ThreadCount; i++)
		{
			ThreadPool[i] = new Thread(Job.Run);
			ThreadPool[i].Start();
		}

		foreach (var thread in ThreadPool)
		{
			thread.Join();
		}

		// Print result
		var result = MyTracer.GetTraceResult();
		var formatter = new XmlTraceResultFormatter();
		formatter.Format(result);

		Console.ReadLine();
	}
}
