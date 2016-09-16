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
        static Tracer tracer = new Tracer();
        static void Main(string[] args)
        {
            TestClass test = new TestClass();
            test.method1();
            test.method3();
            TraceResult result = tracer.GetTraceResult();
            IFormatter formatter = new XMLFormatter();
            string formatResult = formatter.Format(result);
            Console.WriteLine(formatResult);
            Console.ReadLine();

        }

        private void longMethod()
        {
            System.Threading.Thread.Sleep(100);
        }

        private void method1()
        {
            tracer.StartTrace();
                longMethod();
                method3();
                method2();
            tracer.StopTrace();
        }
        private void method2()
        {
            tracer.StartTrace();
                longMethod();
                method3();
                method3();
            tracer.StopTrace();
        }
        private void method3()
        {
            tracer.StartTrace();
            longMethod();
            method4();
            method4();
            tracer.StopTrace();
        }
        private void method4()
        {
            tracer.StartTrace();
            longMethod();
            tracer.StopTrace();
        }

    }
}
