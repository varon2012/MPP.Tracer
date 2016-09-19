using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using Tracer;
using Tracer.Fromatters;
using Tracer.Interfaces;

namespace TracerTest
{
    internal class Program
    {
        private static readonly ITracer _tracer = Tracer.Tracer.Instance;

        private static void TestA()
        {
            _tracer.StartTrace();
            Thread.Sleep(100);
            TestB();
            _tracer.StopTrace();
        }

        private static void TestB()
        {
            _tracer.StartTrace();
            Thread.Sleep(100);
            _tracer.StopTrace();
        }

        private static void TestC(int a, int b)
        {
            _tracer.StartTrace();
            Thread.Sleep(100);
            _tracer.StopTrace();
        }

        private static void Main(string[] args)
        {
            new Thread(TestA).Start();
            TestB();
            TestC(0, 1);
            PrintResult(_tracer.GetTraceResult());
        }

        private static void PrintResult(TraceResult result)
        {
            new ConsoleTraceResultFormatter().Format(result);
            using (Stream fileStream = File.Create("result.xml"))
            {
                new XmlTraceResultFormatter(fileStream).Format(result);
            }
        }
    }
}


