using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        static void Main(string[] args)
        {
            List<int> list = new List<int>();
            list.Add(10);
            list.Add(30);
            list.Add(300);

            Console.WriteLine("Result = {0}", SumOfSquares(list));
            Console.ReadLine();
        }
    }
}
