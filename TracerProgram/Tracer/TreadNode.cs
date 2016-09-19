using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    public class ThreadNode
    {
        public int ID;
        public List<MethodNode> Methods;

        private int ValueOfWorkingMethods;
        private readonly static object locker = new object();

        public ThreadNode(int id)
        {
            ID = id;
            Methods = new List<MethodNode>();
            ValueOfWorkingMethods = 0;
        }


    }
}
