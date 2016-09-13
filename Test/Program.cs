using System;
using System.Threading;
using Tracer;

namespace Test
{
    class Program
    {

        static Tracer.ITracer _tracer = Tracer.Tracer.Instance;

        static void Main(string[] args)
        {
            Thread thread = new Thread(() =>
            {
                _tracer.StartTrace();
                Thread.Sleep(69);
                MethodOne(13, new object());
                Thread.Sleep(50);
                MethodFive(12);
                _tracer.StopTrace();
                _tracer.StartTrace();
                Thread.Sleep(100);
                _tracer.StopTrace();
                Tracer.ConsoleTraceResultFormatter formatter = new ConsoleTraceResultFormatter();
                formatter.Format(_tracer.GetTraceResult());
            });
            thread.Start();
            Console.ReadLine();
        }

        static void MethodOne(int paramOne, object paramTwo)
        {
            _tracer.StartTrace();
            Thread.Sleep(80);
            MethodTwo(42);
            _tracer.StopTrace();
        }

        static void MethodTwo(int paramOne)
        {
            _tracer.StartTrace();
            Thread.Sleep(10);
            _tracer.StopTrace();
        }

        static void MethodThree(int paramOne, bool paramTwo, byte paramThree)
        {
            _tracer.StartTrace();
            Thread.Sleep(44);
            MethodTwo(13);
            MethodFour(42, true);
            _tracer.StopTrace();
        }

        static void MethodFour(int paramOne, bool paramTwo)
        {
            _tracer.StartTrace();
            Thread.Sleep(10);
            _tracer.StopTrace();
        }


        static void MethodFive(int paramOne)
        {
            _tracer.StartTrace();
            Thread.Sleep(80);
            MethodTwo(42);
            MethodThree(1, true, 1);
            _tracer.StopTrace();
        }


    }
}
