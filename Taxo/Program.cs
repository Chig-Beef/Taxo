using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

/*
 * This will be a new programming language,
 * based almost entirely on the objective of a perfect syntax.
 * 
 * The language will be compiled into C#,
 * this will make it versatile in that it can be run with other programs,
 * such as unity.
 */

namespace Taxo
{
    class Program
    {

        static void Main(string[] args)
        {
            // This is used to make sure the file names are consistent between reading and writing.
            string fileName = "code";

            // Takes the filename and reads the corresponding file into an array of strings.
            string[] reader = Read_File(fileName + ".txt");

            // Here is the processing of the file, the many steps
            string[] processed = Process_File(reader);

            // Takes the resulting file after processing,
            // and turns it into a .cs
            Write_File(reader, fileName);
        }

        // Main Steps - Input, Processing, Output
        private static string[] Read_File(string filename)
        {
            string line; // To hold the current line being read
            int count = 0; // To count how many elements there are
            string[] reader = new string[50]; // To hold the lines

            using (StreamReader sr = File.OpenText(filename))
            {
                line = sr.ReadLine();
                while (line != null)
                {
                    reader[count] = line;
                    count++;
                    Console.WriteLine(line);
                    line = sr.ReadLine();
                }
            }

            return reader;
        }

        private static string[] Process_File(string[] input)
        {
            input = Add_Semicolons(input);
            input = Add_Curly_Brackets(input);
            input = Add_Usings(input);
            input = Add_Namespace(input);
            input = Rename_Methods(input);
            input = Rename_Types(input);

            return input;
        }

        private static void Write_File(string[] output, string name)
        {
            string filename = name + ".cs";
            int count = 0;

            using (StreamWriter sr = File.CreateText(filename))
            {
                while (output[count] != null)
                {
                    sr.WriteLine(output[count]);
                    count++;
                }
            }
        }

        /*
         * Processing steps split
         */

        // Adds the semicolons to each line
        private static string[] Add_Semicolons(string[] input)
        {
            return input;
        }

        // Adds curly brackets to blocks of code
        private static string[] Add_Curly_Brackets(string[] input)
        {
            return input;
        }

        // Adds the using statements to the start of the code
        private static string[] Add_Usings(string[] input)
        {
            return input;
        }

        // Adds the namespace container
        private static string[] Add_Namespace(string[] input)
        {
            return input;
        }

        // Renames certain methods that have a simpler name in the uncompiled language
        private static string[] Rename_Methods(string[] input)
        {
            return input;
        }

        // Renames some types which are created as shorthand for other types
        private static string[] Rename_Types(string[] input)
        {
            return input;
        }

    }
}
