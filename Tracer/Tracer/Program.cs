using System;
using System.Collections.Generic;
using System.Threading;
using TracerLib;
using TracerLib.Formatters;

namespace TracerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var tracer = Tracer.Instance;
            tracer.StartTrace();
            TestClass obj = new TestClass();
            var testThread = new Thread(obj.FirstMethod);
            testThread.Start();
            testThread.Join();



            LoopMethod();

            ThreadLoopMethod();
            AnotherMethod(5);
            tracer.StopTrace();

            var res = tracer.GetTraceResult();
            
            var consoleFormatter = new ConsoleResultFormatter();
            consoleFormatter.Format(res);

            var xml = new XmlResultFormatter("E:\\info.xml");
            xml.Format(res);

            Console.ReadLine();
        }

        static void SomeMethod()
        {
            var tracer = Tracer.Instance;
            tracer.StartTrace();

            Thread.Sleep(100);

            tracer.StopTrace();
        }
        static void LoopMethod()
        {
            var tracer = Tracer.Instance;
            tracer.StartTrace();

            for (int i = 0; i < 5; i++)
            {
                AnotherMethod(i);
            }

            Thread.Sleep(100);

            tracer.StopTrace();
        }

        static void ThreadLoopMethod()
        {
            var tracer = Tracer.Instance;
            tracer.StartTrace();

            var threads = new List<Thread>();
            for (int i = 0; i < 4; i++)
            {
                var thread = new Thread(SomeMethod);
                threads.Add(thread);
                thread.Start();
                thread.Join();
            }

            foreach (var thread in threads)
            {
                thread.Join();
            }

            tracer.StopTrace();
        }

        static void AnotherMethod(int a)
        {
            var x = a;
            var tracer = Tracer.Instance;
            tracer.StartTrace();
            tracer.StopTrace();
        }

    }
}
