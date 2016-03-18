using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace ExpressStudy
{
    class Program
    {
        public static Foobar Target { get; private set; }

        public static MethodInfo Method { get; private set; }

        public static Action<Foobar> Executor { get; private set; }
        public Program()
        {

        }
        private static Action<Foobar> CreateExecutor(MethodInfo method)
        {
            ParameterExpression target = Expression.Parameter(typeof(Foobar), "target");

            Expression expression = Expression.Call(target, method);

            return Expression.Lambda<Action<Foobar>>(expression, target).Compile();
        }

        static void Main(string[] args)
        {
            //var cc= "1,2,3".Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).
            Array.ConvertAll<string,int>("1,2,3".Split(','), x => int.Parse(x));
            return;

            Target = new Foobar();

            Method = typeof(Foobar).GetMethod("Run");

            Executor = CreateExecutor(Method);

            Stopwatch stopwatch = new Stopwatch();

            stopwatch.Start();

            int times = 10000;
            for (int i = 0; i < times; i++)
            {
                Method.Invoke(Target, args);
            }

            long elapsed1 = stopwatch.ElapsedMilliseconds;

            stopwatch.Restart();

            for (int i = 0; i < times; i++)
            {
                Executor(Target);
            }

            long elapsed2 = stopwatch.ElapsedMilliseconds;

            Console.WriteLine("{0,-10}{1,-12}{2}", times, elapsed1, elapsed2);
        }
    }
}
