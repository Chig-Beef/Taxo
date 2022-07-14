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
        private static int Total { get; set; } // How many lines of code there actually is
        private static string FileName { get; set; } // The name of the file to read from/write to
        private static Dictionary<string, string> Translations { get;} = new Dictionary<string, string>(2); // Some words that are replaced to translate
        private static int[] Tabs { get; set; } // Keeps the indentation of each line recorded
        private static string[] Usings { get; set; } // An array of using statements that are needed in order for the program to work
        private static int UsingCount { get; set; } // How many usings there are

        static void Main(string[] args)
        {
            // This is used to make sure the file names are consistent between reading and writing.
            FileName = "code";

            Translations["Out"] = "Console.WriteLine";
            Translations["strs"] = "string[]";

            Tabs = new int[50];

            Usings = new string[20];
            Usings[0] = "System";
            UsingCount = 1;

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
            bool tabbed; // Whether it has finished counting tabs
            int iter; // A counter for the while loop to increment

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
                        if (line.Substring(iter * 4, 4) == "    ") // Is it an indentation?
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
            input = Add_Namespace(input);
            input = Add_Usings(input);
            input = Translate_Names(input);

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
            // This loops through each line,
            // and checks whether the line after it is indented higher than it or not.
            // If it isn't, it adds a semicolon.

            for (int i = 0; i < Total; i++)
            {
                if (Tabs[i] >= Tabs[i + 1])
                {
                    input[i] += ";";
                }
            }
            return input;
        }

        // Adds curly brackets to blocks of code
        private static string[] Add_Curly_Brackets(string[] input)
        {
            List<string> mapped = new List<string>(); // The new list of lines
            List<int> tracker = new List<int>(); // List of indent markers to keep track of where curly braces are needed
            string indents;

            for (int i = 0; i < Total; i++)
            {
                // Always adds the line, no matter what
                mapped.Add(input[i]);

                // Check whether the line ends in a semicolon or not,
                // this is the semicolon that was added in the function "Add_Semicolons"
                if (input[i][input[i].Length - 1] != ';')
                {
                    // Add a curly bracket (with corresponding indentation),
                    // then add the indent to the tracker, so it can be closed off later
                    indents = String.Concat(Enumerable.Repeat("    ", Tabs[i]));
                    mapped.Add(indents + "{");
                    tracker.Add(Tabs[i]);
                }
                else
                {
                    // Check if any curly braces need closing
                    for (int j = 0; j < tracker.Count; j++)
                    {
                        if (Tabs[i] == tracker[j])
                        {
                            // Close it, and remove the indent from the tracker
                            indents = String.Concat(Enumerable.Repeat("    ", Tabs[i]));
                            mapped.Add(indents + "}");
                            tracker.RemoveAt(j);
                        }
                    }
                }
            }

            // Adds any leftover curly braces that are needed
            for (int j = 0; j < tracker.Count; j++)
            {
                indents = String.Concat(Enumerable.Repeat("    ", tracker[j]));
                mapped.Add(indents + "}");
            }

            // Turns the list into a fixed array,
            // and updates the Total to fit the number of lines that exist now.
            string[] output = mapped.ToArray();
            Total = output.Length;

            return output;
        }

        // Adds the namespace container
        private static string[] Add_Namespace(string[] input)
        {
            // Wrap all the code in a namespace and a class,
            // then indent everything inside the class.
            // Finally, add to total so that the total number of lines includes the namespace and class

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

            Total += 6;

            return output;
        }

        // Adds the using statements to the start of the code
        private static string[] Add_Usings(string[] input)
        {
            // Add room for the "using" statements
            string[] output = new string[Total + UsingCount];

            // Add all the "using" statements
            for (int i = 0; i < UsingCount; i++)
            {
                output[i] = "using " + Usings[i] + ";";
            }

            // Then add all the lines that were there before
            for (int i = 0; i < Total; i++)
            {
                output[i + UsingCount] = input[i];
            }
            return output;
        }

        // Renames certain methods that have a simpler name in the uncompiled language
        private static string[] Translate_Names(string[] input)
        {
            // Find any instances of a translatable name each line,
            // and translate it
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

    }
}
