using System;
using System.Threading;
using System.Diagnostics;

public class ThreadJob
{
	public static Tracer UsedTracer;

	public void Run()
	{
		Stopwatch ThreadWatch = new Stopwatch();
		ThreadWatch.Start();

		UsedTracer.StartTrace();

		Thread.Sleep(500);
		foo();

		UsedTracer.StopTrace();

		ThreadWatch.Stop();
		UsedTracer.SetThreadTime(ThreadWatch.ElapsedMilliseconds);

		Program.SignalThreadEnded();
	}

	void foo()
	{
		UsedTracer.StartTrace();

		Random rand = new Random();
		Thread.Sleep(350 + rand.Next() % 800);

		bar();
		lol();

		Thread.Sleep(150);

		UsedTracer.StopTrace();
	}

	void bar()
	{
		UsedTracer.StartTrace();

		Random rand = new Random();
		Thread.Sleep(350 + rand.Next() % 800);

		lol();

		UsedTracer.StopTrace();
	}

	void lol()
	{
		UsedTracer.StartTrace();

		Thread.Sleep(150);

		UsedTracer.StopTrace();
	}
}
