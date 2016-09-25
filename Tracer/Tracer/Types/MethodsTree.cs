using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BSUIR.Mishin.Tracer.Types;

namespace BSUIR.Mishin.Tracer.Types
{
    public class MethodsTree
    {
        public MethodInfo Element;
        public List<MethodsTree> Childs;
    }
}
