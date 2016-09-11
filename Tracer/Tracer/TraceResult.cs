using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    public class TraceResult : List<TraceResultItem>
    {
        public TraceResultItem FirstOrCreate(int threadId)
        {
            var result = from t in this
                        where t.ThreadId.Equals(threadId)
                        select t;
            if (!result.Any())
            {
                var newItem = new TraceResultItem(threadId);
                this.Add(newItem);
                return newItem;
            }
            else
            {
                return result.ElementAt(0);
            }
        }
    }
}
