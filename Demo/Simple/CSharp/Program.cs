using System;
using System.Collections.Generic;

namespace CSharp
{
    class Program
    {
        public static int SumOfSquares(List<int> list)
        {
            var result = 0;
            foreach (var element in list)            
                result += element * element;
            
            return result;
        }

        static void Main()
        {
            var list = new List<int> {10, 30, 300};

            Console.WriteLine("Result = {0}", SumOfSquares(list));
            Console.ReadLine();
        }
    }
}
