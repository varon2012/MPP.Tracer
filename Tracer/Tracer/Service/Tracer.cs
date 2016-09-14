using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using Tracer.Contracts;
using Tracer.Model.DataModel;
using Tracer.Model.InputModel;
using Tracer.Model.ViewModels;
using Tracer.Tree;

namespace Tracer.Service
{
    public class Tracer : ITracer
    {
        #region Private Members

        private static readonly Tracer _tracer = new Tracer();
        private TraceTree _traceTree;
        private ReaderWriterLockSlim _treeLock = new ReaderWriterLockSlim();

        #endregion

        #region Ctor
        private Tracer()
        {
            _traceTree = new TraceTree();
        }

        #endregion

        #region Public Methods

        public static Tracer GetInstance()
        {
            return _tracer;
        }

        public TraceResult GetTraceResult()
        {
            return Map(_traceTree);
        }

        public void StartTrace()
        {
            var method = new StackTrace(1).GetFrame(0).GetMethod();
            _treeLock.EnterWriteLock();
            try
            {
                _traceTree.StartThread(new TracerInputModel
                {
                    ThreadId = Thread.CurrentThread.ManagedThreadId,
                    ClassName = method.DeclaringType.ToString(),
                    MethodName = method.Name,
                    ParamsCount = method.GetParameters().Length
                });
            }
            finally
            {
                _treeLock.ExitWriteLock();
            }
        }

        public void StopTrace()
        {
            _treeLock.EnterWriteLock();
            try
            {
                _traceTree.EndThread(Thread.CurrentThread.ManagedThreadId);
            }
            finally
            {
                _treeLock.ExitWriteLock();
            }
        }

        #endregion

        #region Private Methods

        private TraceResult Map(TraceTree model)
        {
            if (model == null)
                return null;

            return new TraceResult
            {
                Threads = model.Threads.Select(Map).ToList()
            };
        }

        private ThreadViewModel Map(KeyValuePair<int, TraceNode<ThreadDataModel>> model)
        {
            return new ThreadViewModel
            {
                Id = model.Value.Data.Id,
                LeadTime = model.Value.Data.LeadTime,
                Methods = model.Value.Methods.Select(Map).ToList()
            };
        }

        private MethodViewModel Map(TraceNode<MethodDataModel> model)
        {
            return new MethodViewModel
            {
                ClassName = model.Data.ClassName,
                LeadTime = model.Data.LeadTime,
                MethodName = model.Data.MethodName,
                ParamsCount = model.Data.ParamsCount,
                InternalMethods = model.Methods.Select(Map).ToList()
            };
        }

        #endregion
    }
}
