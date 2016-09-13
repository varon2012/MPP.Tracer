using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace BSUIR.Mishin.Tracer.Types {
    public class TracerInfo {
        public string MethodName, ClassName;
        public double Time;
        public int CountParams;

        private DateTime _startTime;
        private int _id;
        private Thread _currentThread;
        private int _threadId;

        public TracerInfo() {
            _currentThread = Thread.CurrentThread;
        }

        public Thread GetThread() { return _currentThread; }

        public int GetThreadId() { return _threadId; }
        public void SetThreadId(int id) { _threadId = id; }

        public int GetId() { return _id; }
        public void SetId(int id) { _id = id; }

        public void SetStartTime(DateTime time) { _startTime = time; }
        public DateTime GetStartTime() { return _startTime; }
    }
}
