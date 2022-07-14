# Taxo
A new language made with a usable syntax at its core

Taxo is less so a full language, and more an easier or alternate syntax for C#
This will be accomplished by using suggestions from many people, so that it will be widely recognised as "perfect syntax"


## Example
Here is some code written in C#.

using System;
namespace code
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("This is placeholder code for what the actual user would write");
        }
    }
}

This code has a lot of extra phrases, that could be simplified.
Taxo tries to minimise these statements.


### Steps
There are steps it takes to allow you to write cleaner code. [Note, these steps are not in order, they are just an example]

#### One: Remove semicolons

using System
namespace code
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("This is placeholder code for what the actual user would write")
        }
    }
}

#### Two: Remove namespace and the main class (Program)

using System
static void Main(string[] args)
{
    Console.WriteLine("This is placeholder code for what the actual user would write")
}

#### Three: Remove the "using" statements (automatically detect which ones are needed)

static void Main(string[] args)
{
    Console.WriteLine("This is placeholder code for what the actual user would write")
}

#### Four: Remove curly braces

static void Main(string[] args)
    Console.WriteLine("This is placeholder code for what the actual user would write")

#### Five: Use shorter versions of common names

static void Main(strs args)
    Out("This is placeholder code for what the actual user would write")
