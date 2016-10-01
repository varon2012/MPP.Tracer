using System;
using Examples;
using Infrastructure.Formatters;

namespace TracerApplication
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var example = new Example();

            example.John();
            example.Mark();

            var res = example.GetTracer();

            var format = new XmlTraceResultFormatter();
            format.Format(res);
            Console.WriteLine("\r\n");
            var console = new ConsoleTraceResultFormatter();
            console.Format(res);

            Console.ReadLine();
        }
    }
}