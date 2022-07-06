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
            
            string filename = @"code.txt", line = "";
            int count = 0;
            string[] reader = new string[50];

            using (StreamReader sr = File.OpenText(filename))
            {
                line = sr.ReadLine();
                while (line != null)
                {
                    Console.WriteLine(line);
                    reader[count] = line;
                    count++;
                    line = sr.ReadLine();
                }
            }

            // Declare
            filename = @"code.cs";
            count = 0;

            // Processing
            using (StreamWriter sr = File.CreateText(filename))
            {
                while (reader[count] != null)
                {
                    sr.WriteLine(reader[count]);
                    count++;
                }
            }
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
