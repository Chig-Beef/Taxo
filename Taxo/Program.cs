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
        private static int Total { get; set; }
        private static string FileName { get; set; }
        private static Dictionary<string, string> Translations { get;} = new Dictionary<string, string>(2);
        private static int[] Tabs { get; set; }

        static void Main(string[] args)
        {
            // This is used to make sure the file names are consistent between reading and writing.
            FileName = "code";

            Translations["Out"] = "Console.WriteLine";
            Translations["strs"] = "string[]";

            Tabs = new int[50];

            // Takes the filename and reads the corresponding file into an array of strings.
            string[] reader = Read_File(FileName + ".txt");

            // Here is the processing of the file, the many steps
            string[] processed = Process_File(reader);

            // Takes the resulting file after processing,
            // and turns it into a .cs
            Write_File(processed, FileName + ".cs");
        }

        // Main Steps - Input, Processing, Output
        private static string[] Read_File(string filename)
        {
            string line; // To hold the current line being read
            int count = 0; // To count how many elements there are
            string[] reader = new string[50]; // To hold the lines
            bool tabbed;
            int iter;

            using (StreamReader sr = File.OpenText(filename))
            {
                line = sr.ReadLine();
                while (line != null)
                {
                    reader[count] = line;

                    // Finds out how many tabs are at the start of each line
                    tabbed = false;
                    iter = 0;
                    while (!tabbed)
                    {
                        if (line.Substring(iter * 4, 4) == "    ")
                        {
                            Tabs[count]++;
                        }
                        else
                        {
                            tabbed = true;
                        }

                        iter++;
                    }

                    count++;

                    line = sr.ReadLine();
                }
            }

            Total = count;

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

        private static void Write_File(string[] output, string filename)
        {
            using (StreamWriter sr = File.CreateText(filename))
            {
                for (int i = 0; i < output.Length; i++)
                {
                    sr.WriteLine(output[i]);
                }
            }
        }

        //
        // Processing steps split
        //

        // Adds the semicolons to each line
        private static string[] Add_Semicolons(string[] input)
        {
            for (int i = 0; i < Total; i++)
            {
                Console.WriteLine(Tabs[i]);
                if (Tabs[i] < Tabs[i + 1])
                {
                    
                }
                else
                {
                    input[i] += ";";
                }
            }
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
            string[] output = new string[Total + 6];

            output[0] = "namespace " + FileName;
            output[1] = "{";
            output[2] = "    class Program";
            output[3] = "    {";
            output[Total + 4] = "    }";
            output[Total + 5] = "}";

            for (int i = 0; i < Total; i++)
            {
                output[i + 4] = "        " + input[i];
            }

            Total += 5;

            return output;
        }

        // Renames certain methods that have a simpler name in the uncompiled language
        private static string[] Rename_Methods(string[] input)
        {
            for (int i = 0; i < Total; i++)
            {
                foreach (KeyValuePair<string, string> entry in Translations)
                {
                    if (input[i].IndexOf(entry.Key) != -1)
                    {
                        input[i] = input[i].Replace(entry.Key, entry.Value);
                    }
                }
            }

            return input;
        }

        // Renames some types which are created as shorthand for other types
        private static string[] Rename_Types(string[] input)
        {
            return input;
        }

    }
}
