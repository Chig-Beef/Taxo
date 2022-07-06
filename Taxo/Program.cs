using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

/*
 * 
 * This will be a new programming language,
 * based almost entirely on the objective of a perfect syntax.
 * 
*/

namespace Taxo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Test test = new Test();
        }

        public class Test
        {
            public Test()
            {
                Console.WriteLine("hello");
            }
        }
    }
}
