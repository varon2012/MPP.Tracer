using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                case "xml": formatter = new XMLFormatter(); break;
                default:
                    Console.Error.WriteLine("Unknown formatter type\nconsole - console formatter\nxml - xml fomatter");
                    Console.ReadLine();
                    return;
            }

            Test test = new Test();
            test.startTesting(formatter);
        }

        public void startTesting(IFormatter formatter)
        {
            List<Thread> threadList = new List<Thread>();
            for(int i = 0; i < 10; i++)
            {
                Thread thread = (i % 2 == 0) ? new Thread(method2) : new Thread(method1);
                threadList.Add(thread);
                thread.Start();
            }

            method3();
            foreach(Thread thread in threadList)
            {
                thread.Join();
            }

            TraceResult result = tracer.GetTraceResult();
            string formatResult = formatter.Format(result);
            Console.WriteLine(formatResult);
            Console.ReadLine();
        }

        private void longMethod()
        {
            Thread.Sleep(100);
        }

        private void method1()
        {
            tracer.StartTrace();
                longMethod();
                method3();
            tracer.StopTrace();
        }
        private void method2()
        {
            tracer.StartTrace();
                longMethod();
                method3();
                //method1();
            tracer.StopTrace();
        }
        private void method3()
        {
            tracer.StartTrace();
            longMethod();
            method4();
            method4();
            tracer.StopTrace();
        }
        private void method4()
        {
            tracer.StartTrace();
            longMethod();
            tracer.StopTrace();
        }

    }
}
