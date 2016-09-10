using System;
using Trace.Classes;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Tracer tracer = new Tracer();
            tracer.StartTrace();
            Console.ReadLine();
        }
    }
}
