using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    public class MethodNode
    {
        private MethodInfo Info;
        private int Heignt;

        public MethodNode(MethodInfo info, int height)
        {
            Info = info;
            Heignt = height;
        }

    }
}
