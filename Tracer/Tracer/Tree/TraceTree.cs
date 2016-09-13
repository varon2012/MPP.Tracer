using System.Collections.Generic;
using Tracer.Model.DataModel;
using Tracer.Model.InputModel;

namespace Tracer.Tree
{
    public class TraceTree
    {
        #region Public Members

        public Dictionary<int, TraceNode<ThreadDataModel>> Threads { get; private set; }

        #endregion

        #region Ctor

        public TraceTree()
        {
            Threads = new Dictionary<int, TraceNode<ThreadDataModel>>();
        }

        #endregion

        #region Public Methods

        public void StartThread(TracerInputModel model)
        {
            TraceNode<ThreadDataModel> thread;
            if (!Threads.TryGetValue(model.ThreadId,out thread))
            {
                thread = new TraceNode<ThreadDataModel>(MapOnThreadDataModel(model));
                Threads.Add(model.ThreadId, thread);
            }
            thread.StartMethod(MapOnMethodDataModel(model));
        }

        public void EndThread(int threadId)
        {
            Threads[threadId].EndMethod();
        }

        #endregion

        #region Private Methods

        private ThreadDataModel MapOnThreadDataModel(TracerInputModel model)
        {
            return new ThreadDataModel
            {
                Id = model.ThreadId,
            };
        }

        private MethodDataModel MapOnMethodDataModel(TracerInputModel model)
        {
            return new MethodDataModel
            {
                ClassName = model.ClassName,
                MethodName = model.MethodName,
                ParamsCount = model.ParamsCount,
            };
        }
             
        #endregion

    }
}
