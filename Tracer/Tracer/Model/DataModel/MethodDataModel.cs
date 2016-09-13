using System;
using System.Collections.Generic;
using Tracer.Contracts;

namespace Tracer.Model.DataModel
{
    public class MethodDataModel: ILeadTime 
    {
        public string ClassName { get; set; }
        public string MethodName { get; set; }
        public int ParamsCount { get; set; }
        public TimeSpan LeadTime { get; set; }
    }
}
