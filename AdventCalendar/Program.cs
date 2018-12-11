using System;
using AdventCalendar.day1;
using AdventCalendar.day2;

namespace AdventCalendar
{
    class Program
    {
        static void SolvePuzzleDay1()
        {
            var basePath = System.IO.Directory.GetCurrentDirectory();
            var inputPath = @"{basePath}\..\..\..\day1\input.txt";
            var computer = new Day1(inputPath);
            Console.WriteLine("question 1: " + computer.ComputeFrequence());
            Console.WriteLine("question 2: " + computer.ComputeFirstDuplicate());
        }

        static void SolvePuzzleDay2()
        {
            var basePath = System.IO.Directory.GetCurrentDirectory(); 
            var inputPath = @"{basePath}\..\..\..\day2\input.txt";
            //var mockInputPath = @"{basePath}\..\..\..\day2\mock.txt";
            //var mock2InputPath = @"{basePath}\..\..\..\day2\mock2.txt";
            //var solver = new PuzzleSolver(mock2InputPath);
            //var solver = new PuzzleSolver(mockInputPath);
            var solver = new PuzzleSolver(inputPath);
            Console.WriteLine("question 1: " + solver.GetCheckSum());
            Console.WriteLine("quesiton 2: " + solver.GetCommonString());
        }
        static void Main(string[] args)
        {
            SolvePuzzleDay2();
        }
    }
}
