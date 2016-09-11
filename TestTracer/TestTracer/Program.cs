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
        static void Main(string[] args)
        {
            Tracer.Tracer tracer = Tracer.Tracer.Instance;
            tracer.StartTrace();
            test1();
            tracer.StopTrace();
            tracer.StartTrace();
            test2();
            tracer.StopTrace();
            Console.WriteLine(XmlFormatter.Format(tracer.TraceResult));
            ConsoleFormatter.Format(tracer.TraceResult);
            Console.ReadLine();
        }

        private static void test1()
        {
            Thread.Sleep(100);
        }
        private static void test2()
        {
            Thread.Sleep(200);
        }
    }

    
}
