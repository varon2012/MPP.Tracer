using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer.Model.ViewModels
{
    public class ThreadViewModel
    {
        public int Id { get; set; }
        public TimeSpan LeadTime { get; set; }

        public List<MethodViewModel> Methods { get; set; }
     }
}
