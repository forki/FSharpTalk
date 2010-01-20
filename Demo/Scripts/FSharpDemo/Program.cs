using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SF1
{
    class Program
    {
        static void Main(string[] args)
        {
            const string f = "foo";
            const string b = "bar";
            const string e = "";
            string[] arr = { f + b, e, e, f, e, b, f, e, e, f, b, e, f, e, e };

            int l = 0;

            Console.WriteLine("Limit:");
            int n = int.Parse(Console.ReadLine());
            while (l < n)
            {
                for (int i = 0; i < 15; i++)
                {
                    if (l > n)
                        break;

                    //ret = l.ToString() + ": " + arr[i];
                    Console.WriteLine(l + ": " + arr[i]);
                    l++;
                }
            }

            Console.ReadLine();
        }
    }
}
