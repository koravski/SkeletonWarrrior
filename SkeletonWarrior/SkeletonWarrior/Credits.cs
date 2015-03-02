using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkeletonWarrior
{
    class Credits
    {
        static void Main()
        {
            string endNames = "The participants in \"Skeleton Warrior\" game project:\n";

            string[] arrayOfNames = new string[]{
            "Nikolay Karagyozov",
            "Ivailo Kolarov",
            "Pavlina Dragneva",
            "Ivan Donchev",
            "Vasil Todorov",
            "Krasimir Georgiev",
            "Stefan  Dimitrov",
            "Kaloian Koravski",
        };

            Console.WriteLine();
            Console.WriteLine("{0}", endNames);
            foreach (var day in arrayOfNames)
            {
                Console.WriteLine("{0}\n", day);
            }
        }
    }
}
