using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Reflection;
using System.Diagnostics;

using BSUIR.Mishin.Tracer.Types;

namespace BSUIR.Mishin.Tracer
{
    public class Tracer : ITracer {
        private static Tracer _instance;

        public static Tracer Instance {
            get {

                if (_instance == null) {
                    lock (_lockObj) {
                        if (_instance == null) {
                            _instance = new Tracer();
                        }
                    }
                }

                return _instance;
            }
        }

        private static object _lockObj = new object();
        private object _lockCloseObj = new object();

        private bool _isStarted;
        private List<TracerInfo> _traceStack;
        private List<TracerThreadTree> _threadStack;
        private int _id;

        private int _Id {
            get { return _id++; }
        }

        private Tracer() {
            _id = 1;
            _traceStack = new List<TracerInfo>();
            _threadStack = new List<TracerThreadTree>();
            _isStarted = false;
        }

        public void Start() {
            _isStarted = true;
        }

        public void StartTrace() {
            lock (_lockObj) {
                Start();

                TracerInfo currentMethod = GetTracerInfo();
                TracerThreadTree currentThread = _threadStack.Find( (thread) => thread.ThreadId == currentMethod.GetThreadId() );

                if (currentThread.ThreadId == 0) {
                    currentThread = new TracerThreadTree();
                    currentThread.Child = new List<TracerTree>();
                    currentThread.ThreadId = currentMethod.GetThreadId();
                    _threadStack.Add(currentThread);
                }

                AddElementToList(currentMethod, _Id);
                _traceStack.Add(currentMethod);
            }
        }

        public void StopTrace() {
            lock (_lockObj) {
                if (_traceStack.Count == 0) return;
                TracerInfo currentTracer = GetTracerInfo();
                TracerInfo last;

                for (int i = _traceStack.Count - 1; i >= 0; i--) {
                    last = _traceStack[i];

                    if (last.GetThreadId() == currentTracer.GetThreadId()) {
                        _traceStack.RemoveAt(i);
                        last.Time = (DateTime.UtcNow - last.GetStartTime()).TotalMilliseconds;

                        if (last.ClassName == currentTracer.ClassName && last.MethodName == currentTracer.MethodName)
                            return;
                    }
                }
            }
        }

        private TracerInfo GetLastMethodInThread(int currentThreadId) {
            TracerInfo method;

            for (int i = _traceStack.Count - 1; i >= 0; i--) {
                method = _traceStack[i];
                if (currentThreadId == method.GetThreadId())
                    return method;
            }

            return null;
        }

        private void AddElementToList(TracerInfo element, int id) {
            int threadId = element.GetThreadId();

            element.SetId(id);
            TracerInfo last = GetLastMethodInThread(threadId);

            List<TracerTree> tracerTree = _threadStack.Find(elem => elem.ThreadId == threadId).Child;

            TracerTree tree = new TracerTree();
            tree.Element = element;
            tree.Child = new List<TracerTree>();

            if (last == null) {
                tracerTree.Add(tree);
                return;
            }

            FindElementInTree(last, tracerTree).Child.Add(tree);
        }

        private TracerTree FindElementInTree(TracerInfo element, List<TracerTree> header) {
            TracerTree child = new TracerTree();

            for (int i = 0; i < header.Count; i++) {
                child = header[i];
                if (child.Element.GetId() == element.GetId()) return child;
                if (child.Child.Count > 0) {
                    child = FindElementInTree(element, child.Child);
                    if (child.Element.GetId() == element.GetId()) return child;
                }
            }

            return child;
        }

        private TracerInfo GetTracerInfo() {
            TracerInfo result = new TracerInfo();

            result.SetStartTime(DateTime.UtcNow);
            result.SetThreadId(Thread.CurrentThread.ManagedThreadId);

            StackTrace stackTrace = new StackTrace();
            StackFrame frame;

            for (int i = 0; i < stackTrace.FrameCount; i++) {
                frame = stackTrace.GetFrame(i);
                MethodBase method = frame.GetMethod();
                Type type = method.DeclaringType;

                if (type != typeof(Tracer)) {
                    result.MethodName = method.Name;
                    result.ClassName = type.FullName;

                    result.CountParams = method.GetParameters().Length;

                    return result;
                }
            }

            return result;
        }

        public List<TracerThreadTree> GetThreadList() {
            return _threadStack;
        }

        public List<TracerThreadTree> Stop() {
            lock (_lockCloseObj) {
                if (!_isStarted)
                    throw new InvalidOperationException("Tracer is not started");

                int currentThreadId = Thread.CurrentThread.ManagedThreadId;
                Thread currentJoinThread;
                TracerInfo last;

                while ((currentJoinThread = GetAnotherThread(currentThreadId, _traceStack)) != null) {
                    currentJoinThread.Join();

                    for (int i = _traceStack.Count - 1; i >= 0; i--) {
                        last = _traceStack[i];

                        if (last.GetThreadId() == currentJoinThread.ManagedThreadId) {
                            _traceStack.RemoveAt(i);
                            last.Time = (DateTime.UtcNow - last.GetStartTime()).TotalMilliseconds;
                        }
                    }
                }

                for (int i = _traceStack.Count - 1; i >= 0; i--) {
                    last = _traceStack[i];

                    _traceStack.RemoveAt(i);
                    last.Time = (DateTime.UtcNow - last.GetStartTime()).TotalMilliseconds;
                }

                _isStarted = false;
                List<TracerThreadTree> tempList = _threadStack;
                _threadStack = new List<TracerThreadTree>();
                _traceStack.Clear();
                _id = 0;

                return tempList;
            }
        }

        private Thread GetAnotherThread(int currentId, List<TracerInfo> traceStack) {
            for (int i = traceStack.Count - 1; i >= 0; i--) {
                TracerInfo tracer = traceStack[i];
                if (tracer.GetThreadId() != currentId) return tracer.GetThread();
            }

            return null;
        }

    }
}
