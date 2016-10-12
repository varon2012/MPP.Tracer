using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tracer;

namespace TracerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Tracer.Tracer tracer = Tracer.Tracer.GetInstance();
            tracer.StartTrace();

            SomeClass someClass = new SomeClass();
            Thread.Sleep(1000);
            someClass.AnotherMethod();
            Thread thread = new Thread(someClass.SomeMethod);
            thread.Start();
            thread.Join();
            
            tracer.StopTrace();
            new ConsoleFormatter().Format(tracer.TraceResult);
            new XmlFormatter("E:\\result.xml").Format(tracer.TraceResult);
            Console.ReadKey();
        }
    }

    class SomeClass
    {
        private readonly Tracer.Tracer tracer;

        public SomeClass()
        {
            tracer = Tracer.Tracer.GetInstance();
        }

        public void SomeMethod()
        {
            tracer.StartTrace();
            Thread.Sleep(1000);
            AnotherMethod();
            Thread thread = new Thread(new AnotherClass().NewMethod);
            thread.Start();
            thread.Join();
            tracer.StopTrace();
        }

        public void AnotherMethod(int i = 0)
        {
            tracer.StartTrace();
            Thread.Sleep(200);
            tracer.StopTrace();
        }
    }

    class AnotherClass
    {
        private readonly Tracer.Tracer tracer;

        public AnotherClass()
        {
            tracer = Tracer.Tracer.GetInstance();
        }

        public void NewMethod()
        {
            tracer.StartTrace();
            Thread.Sleep(400);
            tracer.StopTrace();
        }
    }
}
