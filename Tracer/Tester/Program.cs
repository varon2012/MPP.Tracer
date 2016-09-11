using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracer;
namespace Tester
{
    class Program
    {
        public static ITest test;

        static void Main(string[] args)
        {
            Init();
        }
        private static void Init()
        {
            test = new SingleThreadTest();
            test.Run();
        }
    }
}
