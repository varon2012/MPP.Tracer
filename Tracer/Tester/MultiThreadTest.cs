using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracer;
using Tracer.Formatters;
using System.Threading;

namespace Tester
{
    class MultiThreadTest : ITest
    {
        private ITracer tracer = Tracer.Tracer.Instance;
        public void Run()
        {
            tracer.StartTrace();

            RunOuterCycle(3);
            LaunchThreadsInCycle(4);
            tracer.StopTrace();
        }

        private void RunOuterCycle(int amountOfIterations)
        {
            tracer.StartTrace();
            for (int i = 0; i < amountOfIterations; i++)
            {
                RunInnerCycle();
            }
            tracer.StopTrace();
        }
        private void RunInnerCycle()
        {
            tracer.StartTrace();
            Thread.Sleep(100);
            tracer.StopTrace();
        }
        private void LaunchThreadsInCycle(int amountOfThreads)
        {
            tracer.StartTrace();
            var threads = new List<Thread>();
            for (int i = 0; i < amountOfThreads; i++)
            {
                threads.Add(new Thread(ThreadHandler));
                threads.Last().Start();
            }
            foreach (var thread in threads)
            {
                thread.Join();
            }
            tracer.StopTrace();
        }

        private void ThreadHandler()
        {
            tracer.StartTrace();
            Thread.Sleep(100);
            tracer.StopTrace();
        }

        public void PrintTestResults()
        {
            var traceResult = tracer.GetTraceResult();

            ITraceResultFormatter formatter = new XmlTraceResultFormatter("TraceResult.xml");
            formatter.Format(traceResult);

            formatter = new ConsoleTraceResultFormatter();
            formatter.Format(traceResult);


        }

    }
}
