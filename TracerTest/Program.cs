using System.IO;
using System.Threading;
using Tracer;
using Tracer.Fromatters;
using Tracer.Interfaces;

namespace TracerTest
{
    internal class Program
    {
        private static readonly ITracer Tracer = global::Tracer.Tracer.Instance;

        private static void TestA()
        {
            Tracer.StartTrace();
            Thread.Sleep(100);
            TestB();
            Tracer.StopTrace();
        }

        private static void TestB()
        {
            Tracer.StartTrace();
            Thread.Sleep(100);
            Tracer.StopTrace();
        }

        private static void TestC(int a, int b)
        {
            Tracer.StartTrace();
            Thread.Sleep(100);
            Tracer.StopTrace();
        }

        private static void Main(string[] args)
        {
            new Thread(TestA).Start();
            TestB();
            TestC(0, 1);
            PrintResult(Tracer.GetTraceResult());
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


