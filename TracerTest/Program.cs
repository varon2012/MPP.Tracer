using System.Collections.Generic;
using System.IO;
using System.Threading;
using Tracer.Formatters;
using Tracer.Interfaces;
using Tracer.Models;

namespace TracerTest
{
    internal class Program
    {
        private static readonly ITracer Tracer = global::Tracer.Tracers.Tracer.Instance;
        private static readonly List<Thread> Threads = new List<Thread>();

        private static void Main(string[] args)
        {
            Tracer.StartTrace();
            StartMethodCallFlow();
            StopMethodCallFlow();
            Tracer.StopTrace();
            PrintResult(Tracer.GetTraceResult());
        }

        private static void StartMethodCallFlow()
        {
            Threads.Add(new Thread(TestA));
            Threads.Add(new Thread(TestB));
            Threads.Add(new Thread(TestC));
            Threads.ForEach((thread) => thread.Start());
        }

        private static void StopMethodCallFlow()
        {
            Threads.ForEach((thead) => thead.Join());
        }

        private static void TestA(object recursionLevelObj)
        {
            Tracer.StartTrace();
            Thread.Sleep(10);
            int recursionLevel = (int) (recursionLevelObj ?? 0);
            if (recursionLevel < 5)
                TestA(recursionLevel + 1);
            TestB();
            Tracer.StopTrace();
        }

        private static void TestB()
        {
            Tracer.StartTrace();
            Thread.Sleep(10);
            Tracer.StopTrace();
        }

        private static void TestC()
        {
            Tracer.StartTrace();
            Thread.Sleep(100);
            TestD(null, null);
            Tracer.StopTrace();
        }

        private static void TestD(object a, object b)
        {
            Tracer.StartTrace();
            var threads = new List<Thread>();
            for (int i = 0; i < 5; i++)
            {
                var thread = new Thread(TestB);
                threads.Add(thread);
                thread.Start();
            }
            threads.ForEach((thread) => thread.Join());
            Tracer.StopTrace();
        }

        private static void PrintResult(TraceResult result)
        {
            new ConsoleTraceResultFormatter().Format(result);
            using (Stream fileStream = File.Create("XmlResult.xml"))
            {
                new XmlTraceResultFormatter(fileStream).Format(result);
            }
            using (Stream fileStream = File.Create("SerializationResult.xml"))
            {
                new SerializationTraceResultFormatter(fileStream).Format(result);
            }
        }
    }
}


