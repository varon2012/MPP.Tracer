using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer.Classes.Tree
{
    internal class ThreadNode : AbstractChildNode
    {

        private int id;
        internal int Id
        {
            get
            {
                return id;
            }
        }

        internal ThreadNode(int id)
        {
            this.id = id;
        }

    }
}
