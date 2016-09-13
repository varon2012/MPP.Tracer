using System;
using System.Collections.Generic;
using System.Threading;
using Tracer.Contracts;
using Tracer.Service;

namespace Test
{
    class Program
    {
        private static ITracer _tracer;
        private static ITraceResultFormatter _formatter;

        public static void Main(string[] args)
        {
            try
            {
                _tracer = Tracer.Service.Tracer.GetInstance();
                _tracer.StartTrace();

                var threads = CreateThreads(10);

                TestMethod1(0);
                TestMethod2(0, null);
                TestMethod3(0, null, null);
                TestMethod1(0);

                WaitThreadEnd(threads);

                _tracer.StopTrace();

                var traceResult = _tracer.GetTraceResult();
                _formatter = new JsonResultFormatter();
                _formatter.Format(traceResult);
                _formatter = new ConsoleResultFormatter();
                _formatter.Format(traceResult);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
            finally
            {
                Console.ReadLine();
            }
        }

        private static List<Thread> CreateThreads(int count)
        {
            _tracer.StartTrace();
            Thread.Sleep(1000);
            var threads = new List<Thread>();
            for (int i = 0; i < count; i++)
            {
                var thread = (i % 3 == 0) ? new Thread(TheardMethod3) : new Thread(TheardMethod2);
                threads.Add(thread);
                thread.Start();
            }
            _tracer.StopTrace();
            return threads;
        }

        private static void WaitThreadEnd(List<Thread> threads)
        {
            _tracer.StartTrace();
            Thread.Sleep(1000);
            foreach (var thread in threads)
            {
                thread.Join();
            }
            _tracer.StopTrace();
        }

        private static void TheardMethod2(object obj)
        {
            TestMethod2(0,null);
        }

        private static void TheardMethod3(object obj)
        {
            TestMethod3(0, null, null);
        }


        private static void TestMethod1(int testParam1)
        {
            _tracer.StartTrace();

            Thread.Sleep(300);

            _tracer.StopTrace();
        }

        private static void TestMethod2(int testParam1, object testParam2)
        {
            _tracer.StartTrace();

            TestMethod1(0);
            Thread.Sleep(100);

            _tracer.StopTrace();
        }

        private static void TestMethod3(int testParam1, string testParam2, object testParam3)
        {
            _tracer.StartTrace();

            TestMethod1(testParam1);
            Thread.Sleep(500);
            TestMethod2(testParam1,testParam2);

            _tracer.StopTrace();
        }
    }
}
