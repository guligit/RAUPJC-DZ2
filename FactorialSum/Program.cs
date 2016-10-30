using System;
using System.Linq;
using System.Threading.Tasks;

namespace FactorialSum
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Main method is the only method that
            // can ’t be marked with async .
            // What we are doing here is just a way for us to simulate
            // async - friendly environment you usually have with
            // other .NET application types ( like web apps , win apps etc .)
            // Ignore main method , you can just focus on LetsSayUserClickedAButtonOnGuiMethod() as a
            // first method in call hierarchy .
            var t = Task.Run(() => LetsSayUserClickedAButtonOnGuiMethod());
            Console.Read();
        }
        private static async void LetsSayUserClickedAButtonOnGuiMethod()
        {
            var result = await GetTheMagicNumber();
            Console.WriteLine(result);
        }
        private static async Task<int> GetTheMagicNumber()
        {
            return await IKnowIGuyWhoKnowsAGuy();
        }
        private static async Task<int> IKnowIGuyWhoKnowsAGuy()
        {
            return await IKnowWhoKnowsThis(10) + await IKnowWhoKnowsThis(5);
        }
        private static async Task<int> IKnowWhoKnowsThis(int n)
        {
            return await FactorialDigitSum(n);
        }

        public static async Task<int> FactorialDigitSum(int number)
        {
            Task<int> task = Task.Run(() => Factorial(number).ToString().Sum(c => c - '0'));
            return task.Result;
        }

        static int Factorial(int number)
        {
            int result = number;
            for (int i = 1; i < number; i++)
                result = result * i;
            return result;
        }
    }
}