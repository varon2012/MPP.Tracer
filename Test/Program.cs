using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tracer;

namespace Test
{
    class Program
    {

        static Tracer.Tracer _tracer = Tracer.Tracer.Instance;

        static void Main(string[] args)
        {
            Thread thread = new Thread(() =>
            {
                _tracer.StartTrace();
                Thread.Sleep(69);
                _tracer.StopTrace();
            });
        }
    }
}
