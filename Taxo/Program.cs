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
 * The language will be compiled into C#,
 * this will make it versatile in that it can be run with other programs,
 * such as unity.
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
