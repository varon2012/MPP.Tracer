using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections;
using System.Collections.Concurrent;

using BSUIR.Mishin.Tracer;
using BSUIR.Mishin.Tracer.Formatter;
using BSUIR.Mishin.Tracer.Types;

namespace Test {
    class Program {
        static void Main(string[] args) {
            Tracer.Instance.StartTrace();
            Tracer.Instance.StartTrace();
            Tracer.Instance.StartTrace();
            Thread.Sleep(50);
            Tracer.Instance.StopTrace();
            Tracer.Instance.StopTrace();
            Tracer.Instance.StopTrace();


            Tracer.Instance.StartTrace();
            OneTrace(30);
            Tracer.Instance.StopTrace();

            Tracer.Instance.StartTrace();
            OneTrace(20);
            DoubleNesting(10, 10);
            Tracer.Instance.StopTrace();

            CreateThreads();

            Tracer.Instance.WaitStop();

            Dictionary<int, List<MethodsTree>> a = Tracer.Instance.GetTraceResult();

            ConsoleView c = new ConsoleView();
            c.Parse(a);
            JsonView b = new JsonView("out.json");
            b.Parse(a); 

            Console.ReadKey();
        }

        public static void CreateThreads() {
            Tracer.Instance.StartTrace();
            ThreadTracer.OneThreadOneTrace(1000);
            ThreadTracer.OneThreadDoubleNesting(1500);
            Tracer.Instance.StopTrace();
        }

        public static void OneTrace(int ms) {
            Tracer.Instance.StartTrace();
            Thread.Sleep(ms);
            Tracer.Instance.StopTrace();
        }

        public static void ErrorTrace(int ms) {
            Tracer.Instance.StartTrace();
            Thread.Sleep(ms);
        }

        public static void DoubleNesting(int ms1, int ms2)
        {
            Tracer.Instance.StartTrace();

            OneTrace(ms2);

            Thread.Sleep(ms1);
            Tracer.Instance.StopTrace();
        }

    }

    static class ThreadTracer {
        public static void OneThreadOneTrace(object ms) {
            Thread thread = new Thread(OneTrace);
            thread.Start(ms);
        }

        private static void OneTrace(object ms) {
            Tracer.Instance.StartTrace();
            Thread.Sleep((int) ms);
            Tracer.Instance.StopTrace();
        }

        public static void OneThreadDoubleNesting(int ms) {
            Thread thread = new Thread(OneThreadDoubleNesting);
            thread.Start(ms);
        }

        private static void OneThreadDoubleNesting(object ms) {
            Tracer.Instance.StartTrace();
            OneTrace(ms);
            Thread.Sleep(500);
            Tracer.Instance.StopTrace();
        }
    }
}
