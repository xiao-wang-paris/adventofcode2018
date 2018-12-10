using System;
using AdventCalendar.day1;

namespace AdventCalendar
{
    class Program
    {
        static void Main(string[] args)
        {
            var inputPath =@"C:\Users\xiawang\source\repos\AdventCalendar\AdventCalendar\day1\input.txt";
            var computer = new Day1(inputPath);
            Console.WriteLine("question 1: " + computer.ComputeFrequence());
            Console.WriteLine("question 2: " + computer.ComputeFirstDuplicate());
        }
    }
}
