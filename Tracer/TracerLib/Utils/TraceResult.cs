using System.Collections.Generic;
using System.Collections.Immutable;

namespace TracerLib.Utils
{
    public class TraceResult
    {
        public Dictionary<int, ThreadDescriptor> results { get; }

        internal TraceResult(Dictionary<int, ThreadDescriptor> res)
        {
            results = new Dictionary<int, ThreadDescriptor>(res);
            results.ToImmutableDictionary();
        }
    }
}