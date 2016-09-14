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

		Random rand = new Random();
		Thread.Sleep(350 + rand.Next() % 800);

		Bar();
		Lol();

		Thread.Sleep(150);

		UsedTracer.StopTrace();
	}

	void Bar()
	{
		UsedTracer.StartTrace();

		Random rand = new Random();
		Thread.Sleep(350 + rand.Next() % 800);

		Lol();

		UsedTracer.StopTrace();
	}

	void Lol()
	{
		UsedTracer.StartTrace();

		Thread.Sleep(150);

		UsedTracer.StopTrace();
	}
}
