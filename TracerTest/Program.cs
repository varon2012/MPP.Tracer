using System;
using System.IO;
using System.Text;
using System.Threading;
using Tracer;
using Tracer.Format;
using Tracer.TraceResultData;


namespace TracerTest
{
    internal class Program
    {
        private static readonly ITracer tracer = Tracer.Tracer.Instance;

        internal static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            try
            {
                FirstCalc();
                SecondCalc();
                ThirdCalc();
                TraceResult result = tracer.GetTraceResult();


                using (var fileStream = new FileStream(@"../../../out.xml", FileMode.Create, FileAccess.ReadWrite))
                {
                    new XmlTraceResultFormatter(fileStream).Format(result);
                }


                //new XmlTraceResultFormatter(Console.OpenStandardOutput()).Format(result);

                new PlainTextTraceResultFormatter(Console.OpenStandardOutput()).Format(result);

                Console.Read();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            
        }

        private static void FirstCalc()
        {
            tracer.StartTrace();
            Thread.Sleep(10);
            tracer.StopTrace();
        }
        private static void SecondCalc()
        {
            tracer.StartTrace();

            for (int i = 0; i < 5; i++)
            {
                new Thread(() =>
                {
                    ThreadMethod();

                }).Start();
            }
            tracer.StopTrace();
        }
        private static void ThirdCalc()
        {
            tracer.StartTrace();
            for (int i = 0; i < 10; i++)
            {
                FourthCalc(1);
            }
            tracer.StopTrace();
        }
        private static void FourthCalc(int i)
        {
            tracer.StartTrace();
            Thread.Sleep(5);
            tracer.StopTrace();
        }

        public static void ThreadMethod()
        {
            tracer.StartTrace();
            Thread.Sleep(5);
            tracer.StopTrace();
        }
    }
}
