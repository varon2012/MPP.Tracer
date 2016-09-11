using System;

namespace Tracer.Format
{
    public class XMLFormatter : IFormatter
    {
        private const string TAB = "    ";
        private const string NL = "\n";
        private const string ROOT_TAG = "<root>\n"+
                                            "{0}\n"+
                                        "</root>";
        private const string THREAD_TAG = "<thread id={0}>\n"+
                                                "{1}\n"+
                                           "</thread>";
        private const string METHOD_TAG = "<method name={0} time={1} class={2} params={3}>\n+"+
                                                    "{4}\n"+
                                               "</method>";

        public string Format(TraceResult traceResult)
        {
            
        }
    }
}
