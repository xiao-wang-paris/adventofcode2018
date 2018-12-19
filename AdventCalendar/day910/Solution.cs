using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AdventCalendar.day10
{
    class Solution
    {
        private readonly string inputPath;
        public Solution(string path)
        {
            inputPath = path;
        }

        public void ParsePattern()
        {
            var lines = System.IO.File.ReadLines(inputPath);
            var list = new List<Point>();
            foreach (var line in lines)
            {
                list.Add(Point.FromString(line));
            }

            var matrix = Matrix.FromPointList(list);
            for(int i=0; i< 10946; i++)
            {
                matrix.Move();
            }
            while (Console.Read() != 'a')
            {
                matrix.Print();
            }
            matrix.Print();
            //while (Console.Read() != 'a')
            //{
                
            //    int counter = 0;
            //    while (!matrix.HasLetter())
            //    {
            //        counter++;
            //        if (counter % 100 == 0)
            //        {
            //            Trace.WriteLine(counter);
            //        }
            //        matrix.Move();
            //    }
            //    Console.ForegroundColor = new ConsoleColor[] { ConsoleColor.DarkBlue, ConsoleColor.DarkCyan }[counter%2];
            //    Trace.WriteLine(counter++);
            //    matrix.Print();
            //    matrix.Move();
            //}
        }
    }
}
