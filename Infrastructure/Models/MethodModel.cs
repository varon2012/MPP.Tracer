using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;

namespace Infrastructure.Models
{
    public class MethodModel
    {
        public List<MethodModel> Children;

        public MethodModel()
        {
            Children = new List<MethodModel>();
            Stopwatch = new Stopwatch();
        }

        public string MethodName { get; set; }

        public string FullName { get; set; }

        public string ClassName { get; set; }

        public int ParametersCount { get; set; }

        public TimeSpan Time { get; set; }

        public ParameterInfo[] ParameterInfos { get; set; }

        public Stopwatch Stopwatch { get; set; }


    }
}