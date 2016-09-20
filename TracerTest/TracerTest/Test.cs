using System;
using System.Collections.Generic;
using MPPTracer;
using MPPTracer.Format;
using System.Threading;

namespace TracerTest
{
    class Test
    {
        static Tracer tracer = Tracer.Instance;
        static void Main(string[] args)
        {
            if(args.Length != 1)
            {
                Console.Error.WriteLine("Invalid number of arguments.\nPlease, enter formatter type\nconsole - console formatter\nxml - xml fomatter");
                Console.ReadLine();
                return;
            }
            IFormatter formatter;
            switch (args[0])
            {
                case "console": formatter = new ConsoleFormatter(); break;
                case "xml": formatter = new XMLFormatter("FormattedFile.xml"); break;
                default:
                    Console.Error.WriteLine("Unknown formatter type\nconsole - console formatter\nxml - xml fomatter");
                    Console.ReadLine();
                    return;
            }

            Test test = new Test();
            test.StartTesting(formatter);
        }

        public void StartTesting(IFormatter formatter)
        {
            List<Thread> threadList = new List<Thread>();
            for(int i = 0; i < 2; i++)
            {
                Thread thread = (i % 2 == 0) ? new Thread(Method1) : new Thread(Method2);
                threadList.Add(thread);
                thread.Start();
            }
            Method3();
            Method5();
            foreach(Thread thread in threadList)
            {
                thread.Join();
            }

            TraceResult result = tracer.GetTraceResult();
            string formatResult = formatter.Format(result);
            Console.WriteLine(formatResult);
            Console.ReadLine();
        }

        private void LongMethod()
        {
            Thread.Sleep(100);
        }

        private void Method1()
        {
            tracer.StartTrace();
                LongMethod();
                Method3();
                Method3();
            tracer.StopTrace();
        }
        private void Method2()
        {
            tracer.StartTrace();
                LongMethod();
                Method4();
            tracer.StopTrace();
        }
        private void Method3(double par1 = 0)
        {
            tracer.StartTrace();
            LongMethod();
            Method4();
            Method4();
            tracer.StopTrace();
        }
        private void Method4(int par1=0, long par=0)
        {
            tracer.StartTrace();
            LongMethod();
            tracer.StopTrace();
        }
        private void Method5(int par1 = 1, int par2 = 2, int par3 = 3)
        {
            tracer.StartTrace();
            Thread.Sleep(10);
            tracer.StopTrace();
        }

    }
}
