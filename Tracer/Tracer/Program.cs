using System;
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


            var thread = new Thread(WOWOWOWMethod);
            thread.Start();
            thread.Join();

            SomeMethod();
            AnotherMethod(5);
            tracer.StopTrace();

            var res = tracer.GetTraceResult();
            
            var consoleFormatter = new ConsoleResultFormatter();
            consoleFormatter.Format(res);

            var xml = new XmlResultFormatter();
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
        static void WOWOWOWMethod()
        {
            var tracer = Tracer.Instance;
            tracer.StartTrace();
            var x = 1;
            Thread.Sleep(100);
            x++;
            tracer.StopTrace();
        }

        static void AnotherMethod(int a)
        {
            var x = a;
            var tracer = Tracer.Instance;
            tracer.StartTrace();
            TestInnerMethod();
            tracer.StopTrace();
        }

        static void TestInnerMethod()
        {
            var tracer = Tracer.Instance;
            tracer.StartTrace();
            tracer.StopTrace();
        }
    }
}
