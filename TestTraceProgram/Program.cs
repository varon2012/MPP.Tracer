using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using TracerAPI;
using System.Diagnostics;

namespace TestTraceProgram
{
    class Program
    {

        static void Main(string[] args)
        { 
            Tracer.StartTrace();   
            for (int i = 0, y = 0; i < 5; i++)
            {
                y++;
            }
            Tracer.StopTrace();

            Thread thread = new Thread(Program.Method1);
            thread.Start();

            Method1();

            TraceResult result = Tracer.GetTraceResult();
            thread.Abort();
            Console.ReadLine();
        }

        public static void Method1()
        {
            Tracer.StartTrace();
            for (int i = 0, y = 0; i < 5; i++)
            {
                y++;
            }
            Tracer.StopTrace();
        }
    }
}
