using System;
using System.Collections.Generic;
using Tracer.Contracts;

namespace Tracer.Model.DataModel
{
    public class ThreadDataModel: ILeadTime
    {
        public int Id { get; set; }
        public TimeSpan LeadTime { get; set; }
    }
}
