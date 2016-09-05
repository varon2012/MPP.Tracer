using System;
using System.Collections.Generic;
using System.Threading;
using Tracer;

namespace Test
{
    internal class Program
    {
        private static void LongComputing1(int initial)
        {
            _tracer.StartTrace();

            Thread.Sleep(2000);

            _tracer.StopTrace();
        }

        private static void LongComputing2(object anotherInitial)
        {
            _tracer.StartTrace();

            Thread.Sleep(1000);
            LongComputing1((int) anotherInitial);

            _tracer.StopTrace();
        }

        private static void LongComputing3(object againInitial)
        {
            _tracer.StartTrace();

            LongComputing1((int) againInitial);
            Thread.Sleep(6000);
            LongComputing2((int) againInitial);

            _tracer.StopTrace();
        }

        public static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.Error.WriteLine("Invalid number of arguments");
                return;
            }

            var formatterType = args[0];
            ITraceResultFormatter formatter;
            if (Formatters.ContainsKey(formatterType))
            {
                formatter = Formatters[formatterType];
            }
            else
            {
                Console.Error.WriteLine($"Unrecognized formatter type '{formatterType}'");
                return;
            }

            _tracer.StartTrace();
            _tracer.StartTrace();

            var threads = new List<Thread>();
            for (int i = 0; i < 20; i++)
            {
                var thread = (i % 3 == 0) ? new Thread(LongComputing3) : new Thread(LongComputing2);
                threads.Add(thread);
                thread.Start(5);
            }

            LongComputing1(5);
            LongComputing1(5);
            LongComputing2(6);

            _tracer.StopTrace();

            _tracer.StartTrace();

            LongComputing1(5);

            foreach (var thread in threads)
            {
                thread.Join();
            }

            _tracer.StopTrace();
            _tracer.StopTrace();

            var traceResult = _tracer.TraceResult;
            formatter.Format(traceResult);
        }

        private static volatile ITracer _tracer = Tracer.Tracer.Instance;

        private static readonly Dictionary<string, ITraceResultFormatter> Formatters =
            new Dictionary<string, ITraceResultFormatter>
            {
                {"console", new ConsoleTraceResultFormatter()},
                {"xml", new XmlTraceResultFormatter()}
            };
    };
}
