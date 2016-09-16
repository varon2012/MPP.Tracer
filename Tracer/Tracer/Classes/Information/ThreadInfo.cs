using System.Collections.Generic;

namespace Trace.Classes.Information
{
    internal class ThreadInfo
    {
        private Stack<MethodInfo> stackOfMethods;

        public ThreadInfo()
        {
            stackOfMethods = new Stack<MethodInfo>();
        }
    }
}
