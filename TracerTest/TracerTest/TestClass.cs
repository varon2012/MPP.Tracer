using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPPTracer;
using MPPTracer.Format;

namespace TracerTest
{
    class TestClass
    {
        static void Main(string[] args)
        {
            TestClass test = new TestClass();
            Tracer tracer = new Tracer();

            tracer.StartTrace();
                test.longMethod();
                tracer.StartTrace();
                    test.longMethod();
                tracer.StopTrace();
            tracer.StopTrace();

            tracer.StartTrace();
                test.longMethod();
            tracer.StopTrace();

            TraceResult result = tracer.GetTraceResult();
            ConsoleFormatter formatter = new ConsoleFormatter();
            String formatResult = formatter.Format(result);
            Console.WriteLine(formatResult);
            Console.ReadLine();
            
        }

        private void longMethod()
        {
            System.Threading.Thread.Sleep(1000);
        }
    }
}
