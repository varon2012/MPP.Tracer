using System;
using System.Threading;

public class ThreadJob
{
	public static Tracer UsedTracer;

	public void Run()
	{
		UsedTracer.StartTrace();

		Thread.Sleep(500);
		foo();

		UsedTracer.StopTrace();
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
