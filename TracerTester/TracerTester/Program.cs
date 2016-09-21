using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TracerTester
{
    class Program
    {


        public static void TestMethod5()
        {
            Tracer.Tracer.Instance.StartTrace();
            Thread.Sleep(10);
            Tracer.Tracer.Instance.StopTrace();
        }

        public void TestMethod1(int param)
        {
            List<Thread> list = new List<Thread>();
            Tracer.Tracer.Instance.StartTrace();
            for (int i = 0; i < 6; i++)
            {
                list.Add(new Thread(new ThreadStart(Program.TestMethod5)));
                list[i].Start();
            }
            Tracer.Tracer.Instance.StopTrace();
            for (int i = 0; i < 6; i++)
            {
                list[i].Join();
            }
        }

        public void TestMethod2(int param1, int param2)
        {
            Tracer.Tracer.Instance.StartTrace();
            Thread.Sleep(200);
            Tracer.Tracer.Instance.StopTrace();
        }

        public void TestMethod3(int param1, int param2, int param3)
        {
            Tracer.Tracer.Instance.StartTrace();
            TestMethod2(2, 3);
            Thread.Sleep(300);
            TestMethod1(1);
            Tracer.Tracer.Instance.StopTrace();
        }

        public void TestMethod4()
        {
            Tracer.Tracer.Instance.StartTrace();
            TestMethod3(1, 2, 3);
            Tracer.Tracer.Instance.StopTrace();
        }

        static void Main(string[] args)
        {

            Program program = new Program();

            Thread thread = new Thread(program.TestMethod4);
            thread.Start();
            
            program.TestMethod4();
            thread.Join();

            Tracer.ConsoleTraceResultFormatter formatter = new Tracer.ConsoleTraceResultFormatter();
            formatter.Format(Tracer.Tracer.Instance.GetTraceResult());

            Tracer.XmlTraceResultFormatter formatterXml = new Tracer.XmlTraceResultFormatter("xml.xml");
            formatterXml.Format(Tracer.Tracer.Instance.GetTraceResult());
            Console.Read();
        }
    }
}
