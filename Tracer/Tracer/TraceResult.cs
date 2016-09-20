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
        internal List<int> threadIds { get; } = new List<int>();
        internal int callDepth { get; set; }
        internal int Count
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
        }
        internal void Add(TraceResultItem item)
        {
            analyzedItems.Add(item);
        }
        internal void Remove(TraceResultItem item)
        {
            analyzedItems.Remove(item);
        }
        internal TraceResultItem Find(string methodName, int threadId, int callDepth)
        {
            return analyzedItems.Find(TraceResultItem => (TraceResultItem.methodName == methodName)&&(TraceResultItem.threadId == threadId)&&(TraceResultItem.callDepth == callDepth));
        }

        internal void MarkThread(int id)
        {
            if (!threadIds.Contains(id))
                threadIds.Add(id);
        }

        internal void SortByThread()
        {
            analyzedItems.Sort((x, y) => x.threadId.CompareTo(y.threadId));
        }

        internal TraceResultItem[] ToArray()
        {
            return analyzedItems.ToArray();
        }


        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
