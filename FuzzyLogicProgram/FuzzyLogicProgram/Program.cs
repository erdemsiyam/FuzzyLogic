using FuzzyLogicExample.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FuzzyLogicProgram
{
    class Program
    {
        public static void Main(string[] args)
        {
            Example example = new Example(17, 1.2);
            double result = Example.fuzzyLogic(example);
            Console.WriteLine("Example Result : " + result.ToString());
            Console.ReadLine();
        }
    }
}
