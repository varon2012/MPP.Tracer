using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tracer;


namespace SPP_Tracer
{
    public class Program
    {
        static void Main(string[] args)
        {
            var tracer = Tracer.Tracer.Instance;
            tracer.StartTrace();

            var thread = new Thread(Inner1Method1);
            thread.Start();
            thread.Join();
            Inner1Method1();

            var thread2 = new Thread(Inner2Method1);
            thread2.Start();
            thread2.Join();

            LoopMethod();
            AnotherMethod(4);
            ThreadLoopMethod();

            tracer.StopTrace();

            new XmlFormatter("D:\\batka.xml").Format(tracer.TraceResult);
            new ConsoleFormatter().Format(tracer.TraceResult);

            Console.ReadLine();
        }

        static void Inner1Method1()
        {
            var tracer = Tracer.Tracer.Instance;
            tracer.StartTrace();
            Inner1Method2();
            tracer.StopTrace();
        }

        static void Inner1Method2()
        {
            var tracer = Tracer.Tracer.Instance;
            tracer.StartTrace();
            Thread.Sleep(100);
            tracer.StopTrace();
        }

        static void Inner2Method1()
        {
            var tracer = Tracer.Tracer.Instance;

            tracer.StartTrace();

            Inner2Method2();

            tracer.StopTrace();
        }

        static void Inner2Method2()
        {
            var tracer = Tracer.Tracer.Instance;

            tracer.StartTrace();

            Thread.Sleep(100);

            tracer.StopTrace();
        }

        static void SomeMethod()
        {
            var tracer = Tracer.Tracer.Instance;
            tracer.StartTrace();

            Thread.Sleep(100);

            tracer.StopTrace();
        }
        static void LoopMethod()
        {
            var tracer = Tracer.Tracer.Instance;
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
            var tracer = Tracer.Tracer.Instance;
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
            var tracer = Tracer.Tracer.Instance;
            tracer.StartTrace();
            tracer.StopTrace();
        }
    }
}
