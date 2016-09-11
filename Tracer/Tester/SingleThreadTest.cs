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
            tracer.StartTrace();

            RunCycle(204800000);

            tracer.StopTrace();

            TraceResult result = tracer.GetTraceResult();
            result.PrintToConsole();
        }

        private void RunCycle(int repeatAmount)
        {
            ITracer tracer = new Tracer.Tracer();
            tracer.StartTrace();

            for (int i = 0; i < repeatAmount; i++)
            {
                int a = 1;
                a += i;
            }

            tracer.StopTrace();

            TraceResult result = tracer.GetTraceResult();
            result.PrintToConsole();
        }

    }
}
