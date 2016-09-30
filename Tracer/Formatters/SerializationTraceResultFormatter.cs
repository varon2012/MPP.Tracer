using System.IO;
using System.Xml.Serialization;
using Tracer.Interfaces;
using Tracer.Models;

namespace Tracer.Formatters
{
    public class SerializationTraceResultFormatter : ITraceResultFormatter
    {

        private readonly Stream _outStream;

        public SerializationTraceResultFormatter(Stream outStream)
        {
            _outStream = outStream;
        }

        public void Format(TraceResult traceResult)
        {
            using (_outStream)
            {
                var serializer = new XmlSerializer(typeof(TraceResult));
                serializer.Serialize(_outStream, traceResult);
            }
        }
    }
}