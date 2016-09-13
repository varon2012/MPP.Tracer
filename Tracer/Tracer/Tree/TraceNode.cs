using System.Collections.Generic;
using Tracer.Model.DataModel;
using System;
using Tracer.Contracts;

namespace Tracer.Tree
{
    public class TraceNode<DataType> where
        DataType: ILeadTime 
    {
        #region Private Members

        private TraceNode<MethodDataModel> _currentMethodNode;
        private DateTime _startTime;
        private DateTime _endTime;

        #endregion

        #region Public Members
        public DataType Data { get; private set; }
        public List<TraceNode<MethodDataModel>> Methods { get; private set; }

        #endregion

        #region Ctor

        public TraceNode(DataType model)
        {
            Data =  model;
            Methods = new List<TraceNode<MethodDataModel>>();
        }

        #endregion

        #region Public Methods

        public void StartMethod(MethodDataModel model)
        {
            if (_currentMethodNode == null)
            {
                _startTime = DateTime.Now;
                _currentMethodNode = new TraceNode<MethodDataModel>(model);
                _currentMethodNode._startTime = _startTime;
                Methods.Add(_currentMethodNode);
            }
            else
            {
                _currentMethodNode.StartMethod(model);
            }
        }

        public void EndMethod()
        {
            if(_currentMethodNode._currentMethodNode == null)
            {
                _endTime = DateTime.Now;
                Data.LeadTime = _endTime - _startTime;
                _currentMethodNode.Data.LeadTime = Data.LeadTime;
                _currentMethodNode = null;
            }
            else
            {
                _currentMethodNode.EndMethod();
            }
        }

        #endregion

        //#region Private Methods

        //private void IsLeaf()
        //{
        //    if (_curentMethodNode.)
        //    {

        //    }
        //}

        //#endregion
    }
}

