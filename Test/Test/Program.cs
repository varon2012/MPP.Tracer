using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

using BSUIR.Mishin.Tracer;
using BSUIR.Mishin.Tracer.Formatter;

namespace Test {
    class Program {
        static void Main(string[] args) {
            Tracer.Instance.StartTrace();
            Thread.Sleep(50);
            Tracer.Instance.StopTrace();

            Tracer.Instance.StartTrace();
            ErrorTrace(100);
            Tracer.Instance.StopTrace();

            Tracer.Instance.StartTrace();
            OneTrace(50);
            DoubleNesting(150, 500);
            Tracer.Instance.StopTrace();

            CreateThreads();

            List<Tracer.TracerThreadTree> traceList = Tracer.Instance.Stop();

            ConsoleView consoleView = new ConsoleView(traceList);
            consoleView.Parse();
            JsonView jsonView = new JsonView(traceList);
            Console.WriteLine("File name: " + jsonView.Parse());

            Console.ReadKey();
        }

        public static void CreateThreads() {
            Tracer.Instance.StartTrace();
            ThreadTracer.OneThreadOneTrace(3000);
            ThreadTracer.OneThreadDoubleNesting(50);
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

        public static void DoubleNesting(int ms1, int ms2) {
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
            Thread thread = new Thread(_oneThreadDoubleNesting);
            thread.Start(ms);
        }

        private static void _oneThreadDoubleNesting(object ms) {
            Tracer.Instance.StartTrace();

            OneTrace(ms);

            Tracer.Instance.StopTrace();
        }
    }
}
