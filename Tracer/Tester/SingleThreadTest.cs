using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracer;
namespace Tester
{
    class SingleThreadTest : ITest
    {
        private ITracer tracer = new Tracer.Tracer();
        public void Run()
        {
            RunCycle();

            tracer.StopTrace();
            TraceResult result = tracer.GetTraceResult();

            Console.WriteLine("Время: " + 0.001*result.time + " сек.");
        }

        private void RunCycle()
        {
            tracer.StartTrace();
            for (int i = 0; i < 204800000; i++)
            {
                int a = 1;
                a += i;
            }
        }

    }
}
