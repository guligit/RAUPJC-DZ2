using System;
using System.Linq;

namespace GroupBy
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] integers = new[] {1, 2, 2, 2, 3, 3, 4, 5};
            string[] strings = integers.GroupBy(x => x)
                                       .ToList()
                                       .Select(i => "Broj " + i.Key + " se ponavlja " + i.Count() + " puta.")
                                       .ToArray();

            foreach (string item in strings)
            {
                Console.WriteLine(item);
            }
        }

    }
}
