using System;
using System.Threading;
using System.Diagnostics;

public class ThreadJob
{
	public static Tracer UsedTracer;

	public void Run()
	{
		Stopwatch threadWatch = new Stopwatch();
		threadWatch.Start();

		UsedTracer.StartTrace();

		Thread.Sleep(500);
		Foo();

		UsedTracer.StopTrace();

		threadWatch.Stop();
		UsedTracer.SetThreadTime(threadWatch.ElapsedMilliseconds);
	}

	void Foo()
	{
		UsedTracer.StartTrace();

		Bar();
		Lol();

		Thread.Sleep(150);

		UsedTracer.StopTrace();
	}

	void Bar()
	{
		for (int i = 0; i < 5; i++)
		{
			UsedTracer.StartTrace();
			Thread.Sleep(10);
			UsedTracer.StopTrace();
		}	
	}

	void Lol()
	{
		UsedTracer.StartTrace();

		Thread.Sleep(150);

		UsedTracer.StopTrace();
	}
}
