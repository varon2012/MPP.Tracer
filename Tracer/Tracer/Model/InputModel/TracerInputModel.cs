using System;

namespace Tracer.Model.InputModel
{
    public class TracerInputModel
    {
        public int ThreadId { get; set; }
        public string ClassName { get; set; }
        public string MethodName { get; set; }
        public int ParamsCount { get; set; }
    }
}
