using System;
using System.Threading;
using Infrastructure;
using Infrastructure.Interfaces;

namespace Examples
{
    public class Example
    {
        private readonly ITracer tracer;

        public Example()
        {
            tracer = new Tracer();
        }

        public void Count(object obj)
        {
            tracer.StartTrace();
            var x = (int) obj;
            int t;
            for (var i = 1; i < 9; i++, x++)
                Console.WriteLine("        <from example project>   {0}", x*i);
            tracer.StopTrace();
        }

        public void John()
        {
            tracer.StartTrace();
            var myThread = new Thread(Alon);
            myThread.Start();

            Thread.Sleep(200);

            var num = 0;
            //TimerCallback tm = Count;
            //var timer = new Timer(tm, num, 0, 20);

            Alon();
            Alon();
            //Trevor(2);
            Trevor(1, 2);
            Alon();
            Mark();

            //timer.Dispose();
            tracer.StopTrace();
        }

        public void Alon()
        {
            tracer.StartTrace();
            Trevor(2);
            Thread.Sleep(100);

            tracer.StopTrace();
        }

        public void Trevor(int x)
        {
            tracer.StartTrace();
            Thread.Sleep(100);
            Mark();
            tracer.StopTrace();
        }

        public void Trevor(int x, int y)
        {
            tracer.StartTrace();
            Thread.Sleep(100);
            Mark();
            tracer.StopTrace();
        }

        public void Mark()
        {
            tracer.StartTrace();
            Thread.Sleep(333);
            tracer.StopTrace();
        }

        public TraceResult GetTracer()
        {
            return tracer.GetTraceResult();
        }
    }
}