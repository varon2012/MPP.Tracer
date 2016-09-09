using System;
using System.Threading;

class Program
{
	static Tracer myTracer = new Tracer();

	static void foo()
	{
		myTracer.StartTrace();

		Random rand = new Random();
		Thread.Sleep(350 + rand.Next() % 800);

		bar();
		lol();

		Thread.Sleep(150);

		myTracer.StopTrace();
	}

	static void bar()
	{
		myTracer.StartTrace();

		Random rand = new Random();
		Thread.Sleep(350 + rand.Next() % 800);

		lol();

		myTracer.StopTrace();
	}

	static void lol()
	{
		myTracer.StartTrace();

		Thread.Sleep(150);

		myTracer.StopTrace();
	}

	static void Main(string[] args)
	{
		myTracer.StartTrace();

		Thread.Sleep(500);
		foo();

		myTracer.StopTrace();
		var result = myTracer.GetTraceResult();

		ConsoleTraceResultFormatter consoleFormatter = new ConsoleTraceResultFormatter();
		consoleFormatter.Format(result);

		Console.ReadLine();
	}
}
