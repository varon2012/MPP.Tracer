using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tracer;


namespace SPP_Tracer
{
    public class Program
    {
        static void Main(string[] args)
        {
            var tracer = Tracer.Tracer.Instance;
            tracer.StartTrace();
            Inner1Method1();
            Inner2Method1();
            tracer.StopTrace();
            new XmlFormatter().Format(tracer.TraceResult);
        }

        static void Inner1Method1()
        {
            var tracer = Tracer.Tracer.Instance;
            tracer.StartTrace();
            Inner1Method2();
            tracer.StopTrace();
        }

        static void Inner1Method2()
        {
            var tracer = Tracer.Tracer.Instance;
            tracer.StartTrace();
            Thread.Sleep(100);
            tracer.StopTrace();
        }

        static void Inner2Method1()
        {
            var tracer = Tracer.Tracer.Instance;

            tracer.StartTrace();

            Inner2Method2();

            tracer.StopTrace();
        }

        static void Inner2Method2()
        {
            var tracer = Tracer.Tracer.Instance;

            tracer.StartTrace();

            Thread.Sleep(100);

            tracer.StopTrace();
        }
    }
}
