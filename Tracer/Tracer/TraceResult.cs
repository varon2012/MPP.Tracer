using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    public class TraceResult : IEnumerable, ICloneable
    {
        private List<TraceResultItem> analyzedItems { get; set; } = new List<TraceResultItem>();
        public List<int> threadIds { get; } = new List<int>();
        public int callDepth { get; set; }
        public int Count
        {
            get
            {
                return analyzedItems.Count;
            }
        }

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
        public void Remove(TraceResultItem item)
        {
            analyzedItems.Remove(item);
        }
        public TraceResultItem Find(string methodName, int threadId, int callDepth)
        {
            return analyzedItems.Find(TraceResultItem => (TraceResultItem.methodName == methodName)&&(TraceResultItem.threadId == threadId)&&(TraceResultItem.callDepth == callDepth));
        }

        public void MarkThread(int id)
        {
            if (!threadIds.Contains(id))
                threadIds.Add(id);
        }

        public void SortByThread()
        {
            analyzedItems.Sort((x, y) => x.threadId.CompareTo(y.threadId));
        }

        public TraceResultItem[] ToArray()
        {
            return analyzedItems.ToArray();
        }


        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
