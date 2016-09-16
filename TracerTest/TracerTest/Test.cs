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
            Test test = new Test();
            test.startTesting();
        }

        public void startTesting()
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
            IFormatter formatter = new XMLFormatter();
            string formatResult = formatter.Format(result);
            Console.WriteLine(formatResult);
            Console.ReadLine();
        }

        private void longMethod()
        {
            System.Threading.Thread.Sleep(100);
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
