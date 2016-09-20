using System.Threading;
using TracerLib;

namespace TracerTest
{
    public class TestClass
    {
        private Tracer tracer = Tracer.Instance;

        public void FirstMethod()
        {
            tracer.StartTrace();
            Thread.Sleep(2059);
            ThirdMethod();
            NestedMethod();
            tracer.StopTrace();
        }

        public void SecondMethod(int a, string b)
        {
            tracer.StartTrace();
            var thread = new Thread(ThirdMethod);
            thread.Start();
            tracer.StopTrace();
        }

        public void ThirdMethod()
        {
            tracer.StartTrace();
            Thread.Sleep(400);
            tracer.StopTrace();
        }

        public void NestedMethod()
        {
            tracer.StartTrace();
            DoubleNestedMethod();
            tracer.StopTrace();
        }

        public void DoubleNestedMethod()
        {
            tracer.StartTrace();
            tracer.StopTrace();
        }
    }
}