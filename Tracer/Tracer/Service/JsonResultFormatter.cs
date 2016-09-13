using Newtonsoft.Json;
using System;
using System.IO;
using Tracer.Contracts;
using Tracer.Model.ViewModels;

namespace Tracer.Service
{
    public class JsonResultFormatter : ITraceResultFormatter
    {
        #region Private Constants

        private const string delimeter = "\\";
        private const string fileName = "Json.txt";

        #endregion

        #region Public Methods

        public void Format(TraceResult traceResult)
        {
            var serializedObject =  JsonConvert.SerializeObject(traceResult,Formatting.Indented);
            File.WriteAllText(Environment.CurrentDirectory +delimeter + fileName, serializedObject); 
        }

        #endregion
    }
}
