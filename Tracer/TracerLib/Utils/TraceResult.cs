using System.Collections.Generic;
using System.Collections.Immutable;

namespace TracerLib.Utils
{
    public class TraceResult
    {
        public ImmutableDictionary<int, ThreadDescriptor> Results { get; }

        internal TraceResult(Dictionary<int, ThreadDescriptor> res)
        {
            Results = res.ToImmutableDictionary();
        }
    }
}