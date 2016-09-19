using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tracer;

namespace TestTracer
{
    class Program
    {
        public static Tracer.Tracer tracer = Tracer.Tracer.Instance;
        static void Main(string[] args)
        {
            Thread t = new Thread(test2);
            t.Start();
            test1();
            t.Join();
            XmlFormatter x = new XmlFormatter();
            ConsoleFormatter y = new ConsoleFormatter();
            Console.WriteLine(x.Format(tracer.TraceResult));
            y.Format(tracer.TraceResult);
            Console.ReadLine();
        }

        private static void test1()
        {
            tracer.StartTrace();
            Thread.Sleep(100);
            test2();
            tracer.StopTrace();
        }
        private static void test2()
        {
            tracer.StartTrace();
            Thread.Sleep(200);
            test3();
            tracer.StopTrace();
        }

        private static void test3()
        {
            tracer.StartTrace();
            Thread.Sleep(300);
            tracer.StopTrace();
        }
    }

    
}
