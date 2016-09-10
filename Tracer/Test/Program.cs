using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Tracer;

namespace Test
{
    public class Program
    {
        private static volatile ITracer tracer = Tracer.Tracer.Instance();

        private static Dictionary<string, ITraceResultFormatter> formatters = 
            new Dictionary<string, ITraceResultFormatter>();

        private static string fileName = "Output.xml";

        private static Random randomTime = new Random();
        private const int minTime = 100;
        private const int maxTime = 250;

        private static List<Thread> threads = new List<Thread>();
        private const int threadCount = 16;

        private static ThreadStart[] InitializeMethods()
        {
            ThreadStart[] methods = new ThreadStart[4];
            methods[0] = SomeTestMethod1;
            methods[1] = SomeTestMethod2;
            methods[2] = SomeTestMethod3;
            methods[3] = SomeTestMethod4;

            return methods;
        }

        private static void SomeTestMethod1()
        {
            tracer.StartTrace();

            SomeTestMethod2();
            Thread.Sleep(GetRandomTimeInMilliseconds());

            tracer.StopTrace();
        }

        private static void SomeTestMethod2()
        {
            tracer.StartTrace();

            SomeTestMethod3();
            SomeTestMethod4();
            Thread.Sleep(GetRandomTimeInMilliseconds());

            tracer.StopTrace();
        }

        private static void SomeTestMethod3()
        {
            tracer.StartTrace();

            SomeTestMethod4();
            Thread.Sleep(GetRandomTimeInMilliseconds());

            tracer.StopTrace();
        }

        private static void SomeTestMethod4()
        {
            tracer.StartTrace();

            Thread.Sleep(GetRandomTimeInMilliseconds());

            tracer.StopTrace();
        }

        private static int GetRandomTimeInMilliseconds()
        {
            return randomTime.Next(minTime, maxTime);
        }

        private static int GetRandomNumberOfMethod()
        {
            const int min = 1;
            const int max = 4;
            return randomTime.Next(min, max);
        }

        private static void SayGoodBye()
        {
            Console.WriteLine("Enter some key to exit...");
            Console.ReadLine();
        }

        static void Main(string[] args)
        {
            formatters.Add("xml", new XmlTraceResultFormatter(fileName));
            formatters.Add("console", new ConsoleTraceResultFormatter());

            ITraceResultFormatter formatter = new ConsoleTraceResultFormatter();
            string formatterKey = args[0];
            if (formatters.Keys.Contains(formatterKey))
                formatter = formatters[formatterKey];

            ThreadStart[] methods = InitializeMethods();

            tracer.StartTrace();

            SomeTestMethod1();
            SomeTestMethod2();
            SomeTestMethod3();
            SomeTestMethod4();

            for (int i = 0; i < threadCount; i++)
            {
                int methodNumber = GetRandomNumberOfMethod();
                ThreadStart method = methods[methodNumber];
                Thread newThread = new Thread(method);
                threads.Add(newThread);
                newThread.Start();
            }
            Thread.Sleep(GetRandomTimeInMilliseconds());

            tracer.StopTrace();

            TraceResult traceResult = tracer.GetTraceResult();
            formatter.Format(traceResult);

            SayGoodBye();
        }
    }
}
