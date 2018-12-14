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
            var computer = new day1.Solution(inputPath);
            Console.WriteLine("question 1: " + computer.ComputeFrequence());
            Console.WriteLine("question 2: " + computer.ComputeFirstDuplicate());
        }

        static void SolvePuzzleDay2()
        {
            var basePath = System.IO.Directory.GetCurrentDirectory(); 
            var inputPath = @"{basePath}\..\..\..\day2\input.txt";
            //var mockInputPath = @"{basePath}\..\..\..\day2\mock.txt";
            //var mock2InputPath = @"{basePath}\..\..\..\day2\mock2.txt";
            //var solver = new Solution(mock2InputPath);
            //var solver = new Solution(mockInputPath);
            var solver = new day2.Solution(inputPath);
            Console.WriteLine("question 1: " + solver.GetCheckSum());
            Console.WriteLine("quesiton 2: " + solver.GetCommonString());
        }

        private static void SolvePuzzleDay3()
        {
            var basePath = System.IO.Directory.GetCurrentDirectory();
            //var mockInputPath = @"{basePath}\..\..\..\day3\mock.txt";
            //var solver = new AdventCalendar.day3.Solution(mockInputPath);
            var inputPath = @"{basePath}\..\..\..\day3\input.txt";
            var solver = new AdventCalendar.day3.Solution(inputPath);
            Console.WriteLine("question 1: " + solver.CountDuplicateSquareClaims());
            Console.WriteLine("quesiton 2: " + solver.FindNotOverlappedId());
        }

        private static void SolvePuzzleDay4()
        {
            var basePath = System.IO.Directory.GetCurrentDirectory();
            //var mockInputPath = @"{basePath}\..\..\..\day4\mock.txt";
            //var solver = new AdventCalendar.day4.Solution(mockInputPath);
            var inputPath = @"{basePath}\..\..\..\day4\input.txt";
            var solver = new AdventCalendar.day4.Solution(inputPath);
            Console.WriteLine("question 1: " + solver.ComputeProductOfIdAndMinute());
            //Console.WriteLine("quesiton 2: " + solver.FindNotOverlappedId());
        }

        private static void SolvePuzzleDay5()
        {
            var basePath = System.IO.Directory.GetCurrentDirectory();
            //var mockInputPath = @"{basePath}\..\..\..\day5\mock.txt";
            //var solver = new AdventCalendar.day5.Solution(mockInputPath);
            var inputPath = @"{basePath}\..\..\..\day5\input.txt";
            var solver = new AdventCalendar.day5.Solution(inputPath);
            Console.WriteLine("question 1: " + solver.React());
            Console.WriteLine("question 2: " + solver.ReactUpdated());
            //Console.WriteLine("quesiton 2: " + solver.FindNotOverlappedId());
        }

        private static void SolvePuzzleDay6()
        {
            var basePath = System.IO.Directory.GetCurrentDirectory();
            //var mockInputPath = @"{basePath}\..\..\..\day6\mock.txt";
            //var solver = new AdventCalendar.day6.Solution(mockInputPath);
            var inputPath = @"{basePath}\..\..\..\day6\input.txt";
            var solver = new day6.Solution(inputPath);
            Console.WriteLine("question 1: " + solver.ComputeArea());
            //Console.WriteLine("question 2: " + solver.GetRegionSize(32));
            Console.WriteLine("question 2: " + solver.GetRegionSize(10000));
        }

        private static void SolvePuzzleDay7()
        {
            var basePath = System.IO.Directory.GetCurrentDirectory();
            //var mockInputPath = @"{basePath}\..\..\..\day7\mock.txt";
            //var solver = new AdventCalendar.day7.Solution(mockInputPath);
            var inputPath = @"{basePath}\..\..\..\day7\input.txt";
            var solver = new day7.Solution(inputPath);
            Console.WriteLine("question 1: " + solver.GetPath());
            Console.WriteLine("question 1: " + solver.ComputeCompletionTime(5));
        }

        private static void SolutionDay8()
        {
            var basePath = System.IO.Directory.GetCurrentDirectory();
            //var mockInputPath = @"{basePath}\..\..\..\day8\mock.txt";
            //var solver = new day8.Solution(mockInputPath);
            var inputPath = @"{basePath}\..\..\..\day8\input.txt";
            var solver = new day8.Solution(inputPath);
            Console.WriteLine("question 1: " + solver.GetMetaSum());
        }
        static void Main(string[] args)
        {
            SolutionDay8();
        }
    }
}
