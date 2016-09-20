using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer 
{
    public class TraceResultException : Exception
    {
        internal TraceResultException()
        {
        }
        internal TraceResultException(string message) : base(message)
        {

        }
        internal TraceResultException(string message, Exception inner) : base(message, inner)
        {

        }
    }
}
