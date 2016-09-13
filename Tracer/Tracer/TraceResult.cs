using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    public class TraceResult : IEnumerable
    {
        private List<TraceResultItem> analyzedItems { get; set; } = new List<TraceResultItem>();

        public IEnumerator GetEnumerator()
        {
            return analyzedItems.GetEnumerator();
        }

        public TraceResultItem this[int index]
        {
            get
            {
                return analyzedItems[index];
            }
            set
            {
                analyzedItems[index] = value;
            }   

        }
        public void Add(TraceResultItem item)
        {
            analyzedItems.Add(item);
        }
        public TraceResultItem Find(string value)
        {
            return analyzedItems.Find(TraceResultItem => TraceResultItem.methodName == value);
        }
    }
}
