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

            Thread firstThread = new Thread(RunThread);
            Thread secondThread = new Thread(RunLongThread);
            Thread thirdThread = new Thread(RunThread);
            Thread fourthThread = new Thread(RunLongThread);

            firstThread.Start();
            secondThread.Start();
            thirdThread.Start();
            fourthThread.Start();

            RunCycle(150);
            RunOuterCycle(400, 2);

            tracer.StopTrace();

            firstThread.Join();
            secondThread.Join();           
            thirdThread.Join();
            fourthThread.Join();
            
        }

        private void RunCycle(int sleepTime)
        {
            tracer.StartTrace();

            Thread.Sleep(sleepTime);

            tracer.StopTrace();
        }
        private void RunOuterCycle(int sleepTime, int multiplier)
        {
            tracer.StartTrace();
            RunCycle(sleepTime * multiplier);
            tracer.StopTrace();
        }
        private void RunThread()
        {
            RunCycle(500);
        }
        private void RunLongThread()
        {
            RunOuterCycle(500,2);
        }

        internal void PrintTestResults()
        {
            var traceResult = tracer.GetTraceResult();

            ITraceResultFormatter formatter = new XmlTraceResultFormatter();
            formatter.Format(traceResult);

            formatter = new ConsoleTraceResultFormatter();
            formatter.Format(traceResult);


        }

    }
}
