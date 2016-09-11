using System;
using System.Threading;

class Program
{
	const int ThreadCount = 5;
	private static Thread[] ThreadPool = new Thread[ThreadCount];
	private static object ThreadEnderLock = new object();

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

		bool ThreadsFinished = false;
		do
		{
			ThreadsFinished = AllThreadsEnded();
		} while (!ThreadsFinished);

		// Print result
		var result = MyTracer.GetTraceResult();
		var formatter = new XmlTraceResultFormatter();
		formatter.Format(result);

		Console.ReadLine();
	}

	private static bool AllThreadsEnded()
	{
		lock (ThreadEnderLock)
		{
			int ThreadsEndedCount = 0;
			for (int i = 0; i < ThreadCount; i++)
			{
				if (!ThreadPool[i].IsAlive)
				{
					ThreadsEndedCount++;
				}
			}

			return (ThreadsEndedCount == ThreadCount);
		}
	}
}
