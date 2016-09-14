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

        private const string defaultName = "Json.txt";

        #endregion

        #region Private Members

        private readonly char delimeter = Path.DirectorySeparatorChar;
        private readonly string _fileName;

        #endregion

        #region Ctor
        public JsonResultFormatter(string fileName = defaultName)
        {
            _fileName = (string.IsNullOrEmpty(fileName)) ? defaultName : fileName;
        }
        #endregion

        #region Public Methods

        public void Format(TraceResult traceResult)
        {
            var serializedObject =  JsonConvert.SerializeObject(traceResult,Formatting.Indented);
            File.WriteAllText(Environment.CurrentDirectory +delimeter + _fileName, serializedObject); 
        }

        #endregion
    }
}
