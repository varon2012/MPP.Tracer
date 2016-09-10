using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer.Classes.Tree
{
    internal abstract class AbstractNode<T>
    {
        abstract internal void FixateCountStart(long startTime, CallerDescriptor caller);
        abstract internal void FixateCountEnd(long endTime);
    }
}
