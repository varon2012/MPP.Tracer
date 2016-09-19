using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Tracer;

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

            Mine();

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

            Console.Read();
        }

        static void Mine()
        {
            Console.WriteLine("test");
        }

        static void ThreadStart()
        {
            OtherMethod();
            OneMoreMethod();
        }

        static void OtherMethod()
        {
            a.StartTrace();
            a.StartTrace();

            Thread.Sleep(1050);

            OneMoreMethod();

            a.StopTrace();
            a.StopTrace();
        }

        static void OneMoreMethod()
        {
            a.StartTrace();
            Thread.Sleep(50);
            a.StopTrace();
        }
    }
}
