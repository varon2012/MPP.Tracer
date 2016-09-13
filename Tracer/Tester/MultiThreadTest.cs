using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracer;
using System.Threading;

namespace Tester
{
    class MultiThreadTest : ITest
    {
        private ITracer tracer = Tracer.Tracer.Instance;
        public void Run()
        {
            tracer.StartTrace();

            Thread firstThread = new Thread(RunThread);
            Thread secondThread = new Thread(RunLongThread);
            Thread thirdThread = new Thread(RunThread);

            Thread.Sleep(500);

            firstThread.Start();
            secondThread.Start();
            thirdThread.Start();

            tracer.StopTrace();

            firstThread.Join();
            secondThread.Join();           
            thirdThread.Join();

            PrintTestResults();
        }

        private void RunCycle(int sleepTime)
        {
            tracer.StartTrace();

            Thread.Sleep(sleepTime);

            tracer.StopTrace();
        }
        private void RunThread()
        {
            RunCycle(500);
        }
        private void RunLongThread()
        {
            RunCycle(1500);
        }

        private void PrintTestResults()
        {
            var traceResult = tracer.GetTraceResult();
            foreach (TraceResultItem analyzedItem in traceResult)
            {
                analyzedItem.PrintToConsole();
            }
        }

    }
}
