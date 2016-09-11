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

        private static Dictionary<string, ITraceResultFormatter> formatters;

        private static string fileName = "Output.xml";

        private static Random randomTime = new Random();

        private static List<Thread> threads = new List<Thread>();
        private const int threadCount = 16;

        private static ThreadStart[] methods;

        private static void InitializeMethods()
        {
            methods = new ThreadStart[4];
            methods[0] = SomeTestMethod1;
            methods[1] = SomeTestMethod2;
            methods[2] = SomeTestMethod3;
            methods[3] = SomeTestMethod4;
        }

        private static void InitializeFormattersDictionary()
        {
            formatters = new Dictionary<string, ITraceResultFormatter>();
            formatters.Add("xml", new XmlTraceResultFormatter(fileName));
            formatters.Add("console", new ConsoleTraceResultFormatter());
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

        private static void SomeTestMethod5()
        {
            tracer.StartTrace();

            SomeTestMethod1();
            SomeTestMethod2();
            SomeTestMethod3();
            SomeTestMethod4();

            tracer.StopTrace();
        }

        private static int GetRandomTimeInMilliseconds()
        {
            const int minTime = 100;
            const int maxTime = 250;
            return randomTime.Next(minTime, maxTime);
        }

        private static int GetRandomNumberOfMethod()
        {
            const int min = 0;
            const int max = 3;
            return randomTime.Next(min, max);
        }

        private static void StartThreads()
        {
            for (int i = 0; i < threadCount; i++)
            {
                int methodNumber = GetRandomNumberOfMethod();
                ThreadStart method = methods[methodNumber];
                Thread newThread = new Thread(method);
                threads.Add(newThread);
                newThread.Start();
            }
        }

        private static void StopThreads()
        {
            for (int i = 0; i < threadCount; i++)
            {
                threads[i].Join();
            }
        }

        private static void SayGoodBye()
        {
            Console.WriteLine("Enter some key to exit...");
            Console.ReadLine();
        }

        static void Main(string[] args)
        {
            InitializeFormattersDictionary();
            ITraceResultFormatter formatter;
            try
            {
                formatter = formatters[args[0]];
            }
            catch (Exception)
            {
                formatter = new ConsoleTraceResultFormatter();
            }

            InitializeMethods();

            tracer.StartTrace();

            StartThreads();
            Thread.Sleep(GetRandomTimeInMilliseconds());
            StopThreads();

            tracer.StopTrace();

            TraceResult traceResult = tracer.GetTraceResult();
            formatter.Format(traceResult);

            SayGoodBye();
        }
    }
}
