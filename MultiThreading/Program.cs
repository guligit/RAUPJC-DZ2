using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreading
{
    class Program
    {
        private static object _lock = new object();

        static void Main(string[] args)
        {

            List<int> results = new List<int>();
            Parallel.For(0, 100, (i) =>
            {
                Thread.Sleep(1);
                lock (_lock)
                {
                    results.Add(i*i);
                }
            });
            Console.WriteLine("Bag length should be 100. Length is {0}", results.Count);

        }

    }


}
