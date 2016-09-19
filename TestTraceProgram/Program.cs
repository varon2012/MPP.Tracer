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
            for (int i = 0, y = 0; i < 2; i++)
            {
                y++;
            }
            Tracer.StopTrace();

            Console.ReadLine();
        }




    }
}
