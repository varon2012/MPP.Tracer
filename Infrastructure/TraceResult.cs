using System.Collections.Concurrent;
using System.Collections.Generic;
using Infrastructure.Models;

namespace Infrastructure
{
    public class TraceResult
    {
        public TraceResult()
        {
            Children = new Dictionary<int, ThreadModel>();
        }

        public Dictionary<int, ThreadModel> Children { get; set; }
    }
}