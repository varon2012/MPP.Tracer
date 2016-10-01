using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.Helpers;

namespace Infrastructure.Models
{
    public class ThreadModel
    {
        private Stack<MethodModel> methodStack;

        private MethodModel currentMethod;

        private bool state;

        public ThreadModel()
        {
            Methods = new List<MethodModel>();
            methodStack = new Stack<MethodModel>();
        }

        public List<MethodModel> Methods { get; set; }

        public TimeSpan Time { get; set; }

        public int ModelId { get; set; }

        public void AddStartPartMethod(MethodModel model)
        {
            model.Stopwatch.Start();
            if (methodStack.Count < 1)
            {
                Methods.Add(model);
            }
            else
            {
                methodStack.Peek().Children.Add(model);
            }

            methodStack.Push(model);
        }

        public void AddEndPartMethod()
        {
            methodStack.Peek().Stopwatch.Stop();
            methodStack.Peek().Time = methodStack.Peek().Stopwatch.Elapsed;
            methodStack.Pop();
        }
    }
}