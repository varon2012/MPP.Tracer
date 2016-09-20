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
            x.Format(tracer.TraceResult).Save("tree.xml");
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
            List<Thread> threads = new List<Thread>();
            for (int i = 0; i < 4; i++)
            {
                Thread tempThread = new Thread(test5);
                threads.Add(tempThread);
                tempThread.Start();
            }
            tracer.StopTrace();
            for (int i = 0; i < 4; i++)
            {
                threads[i].Join();
            }

        }

        private static void test3()
        {
            tracer.StartTrace();
            Thread.Sleep(300);
            for (int i = 0; i <= 4; i++)
                test4();
            tracer.StopTrace();
        }

        private static void test4()
        {
            tracer.StartTrace();
            Thread.Sleep(100);
            tracer.StopTrace();
        }

        private static void test5()
        {
            tracer.StartTrace();
            Thread.Sleep(10);
            tracer.StopTrace();
        }
    }

    
}
