using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Tracer;
using Tracer.Formatters;

namespace TracerProgram
{
    class Program
    {
        static Tracer.Tracer a;
        static void Main(string[] args)
        {
            a = new Tracer.Tracer();
             a.StartTrace();
             a.StopTrace();


             a.StartTrace();
             a.StartTrace();
             a.StartTrace();

             Thread.Sleep(1000);

             var bgThread = new Thread(ThreadStart);
             bgThread.Start();

             OtherMethod();

             a.StopTrace();
             a.StopTrace();
             a.StopTrace();

             bgThread.Join();


             a.StartTrace();
             Thread.Sleep(500);
             a.StartTrace();
             Thread.Sleep(500);
             a.StartTrace();
             Thread.Sleep(500);

             a.StopTrace();
             a.StopTrace();
             a.StopTrace();


            LoopMethod(1, 1);
            ThreadLoopMethod(1);

            ConsoleTraceResultFormatter formatter = new ConsoleTraceResultFormatter();
            formatter.Format(a.GetTraceResult());

            Console.WriteLine("Please, enter path to xml file");
            String path = Console.ReadLine();
            XMLTraceResultFormatter xmlFormatter = new XMLTraceResultFormatter(path);
            xmlFormatter.Format(a.GetTraceResult());

            Console.WriteLine("Xml file \"info.xml\" sucessfully added ");
            Console.Read();
        }

        static void LoopMethod(int q,int w)
        {
            for (int i = 0; i < 5; i++)
            {
                TestMethod();
            }
        }

        static void ThreadLoopMethod(int q)
        {
            for (int i = 0; i < 5; i++)
            {
                var bgThread = new Thread(TestMethod);
                bgThread.Start();
                bgThread.Join();
            }
            
        }

        static void TestMethod()
        {
            a.StartTrace();
            Thread.Sleep(500);
            a.StopTrace();
        }

        static void ThreadStart()
        {
            OtherMethod();
            OneMoreMethod(1,1);
        }

        static void OtherMethod()
        {
            a.StartTrace();
            a.StartTrace();

            Thread.Sleep(1050);

            OneMoreMethod(1,1);

            a.StopTrace();
            a.StopTrace();
        }

        static void OneMoreMethod(int c, int b)
        {
            a.StartTrace();
            Thread.Sleep(50);
            a.StopTrace();
        }
    }
}
