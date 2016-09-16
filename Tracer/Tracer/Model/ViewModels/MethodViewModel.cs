using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer.Model.ViewModels
{
    public class MethodViewModel
    {
        public string ClassName { get; set; }
        public string MethodName { get; set; }
        public int ParamsCount { get; set; }
        public TimeSpan LeadTime { get; set; }
        public List<MethodViewModel> InternalMethods { get; set; }
    }
}
