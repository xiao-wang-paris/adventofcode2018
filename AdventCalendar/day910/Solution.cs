using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdventCalendar.day10
{
    public class Position
    {
        public int X { get; set; }
        public int Y { get; set; }
    }

    public class Velocity
    {
        public int Dx { get; set; }
        public int Dy { get; set; }
    }

    public class Point
    {
        public Position Pos { get; set; }
        public Velocity Vel { get; set; }

        public static Point FromString(string s)
        {
            string[] str = s.Split(new string[] { "position=<", ",", ">", "velocity=<", " " }, StringSplitOptions.RemoveEmptyEntries);
            return new Point
            {
                Pos = new Position()
                {
                    X = int.Parse(str[0]),
                    Y = int.Parse(str[1])
                },
                Vel = new Velocity()
                {
                    Dx = int.Parse(str[2]),
                    Dy = int.Parse(str[3])
                }
            };
        }

        public void Update()
        {
            Pos.X += Vel.Dx;
            Pos.Y += Vel.Dy;
        }
    }
    public class Matrix
    {
        public int TopMost { get; set; }
        public int BottomMost { get; set; }
        public int LeftMost { get; set; }
        public int RightMost { get; set; }

        List<Point> PointList { get; set; }

        public void Move()
        {
            foreach (Point p in PointList)
            {
                p.Update();
            }
            int[] res = DrawMatrix(PointList);
            LeftMost = res[0];
            RightMost = res[1];
            TopMost = res[2];
            BottomMost = res[3];

            //this./*CharMatrix*/ = DrawMatrix(PointList);
        }

        private static int[] DrawMatrix(List<Point> pl)
        {
            int leftmost = pl.Min(p => p.Pos.X);
            int rightmost = pl.Max(p => p.Pos.X);
            int topmost = pl.Min(p => p.Pos.Y);
            int bottommost = pl.Max(p => p.Pos.Y);

            return new int[] { leftmost, rightmost, topmost, bottommost };

            //char[][] matrix = new char[bottommost - topmost + 1][];
            //for (int i = topmost; i <= bottommost; i++)
            //{
            //    matrix[i - topmost] = new char[rightmost - leftmost + 1];
            //}

            //foreach (Point p in pl)
            //{
            //    matrix[p.Pos.Y - topmost][p.Pos.X - leftmost] = '#';
            //}
            //return matrix;
        }
        public static Matrix FromPointList(List<Point> pl)
        {
            int[] res = DrawMatrix(pl);
            return new Matrix { LeftMost = res[0], RightMost = res[1], TopMost = res[2], BottomMost = res[3], PointList = pl };
        }

        public bool HasLetter()
        {
            if (RightMost - LeftMost > 400 || BottomMost - TopMost > 400)
                return false;
            for (int i = LeftMost; i <= RightMost; i++)
            {
                //bool res = true;
                for (int j = TopMost; j <= BottomMost; j++)
                {
                    if (PointList.Count(p => p.Pos.X == i && Math.Abs(p.Pos.Y - j) < 3) >= 4
                        || PointList.Count(p => p.Pos.Y == j && Math.Abs(p.Pos.X - i) < 3) >= 4)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void Print()
        {
            Console.WriteLine();
            for (int i = 0; i < BottomMost - TopMost + 1; i++)
            {
                for (int j = 0; j < RightMost - LeftMost + 1; j++)
                {
                    if (PointList.Count(p => (p.Pos.Y - TopMost == i) && (p.Pos.X - LeftMost == j)) == 1)
                    {
                        Trace.Write('#');
                    }
                    else
                    {
                        Trace.Write(' ');
                    }
                }
                Trace.Write("\n");
            }
        }
    }

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
            //matrix.Print();
            //Console.Out.Flush();
            //matrix.Move();

            while (Console.Read() != 'a')
            {
                int counter = 0;
                while (/*Console.Read() != 'a' || */!matrix.HasLetter())
                {
                    counter++;
                    if (counter % 100 == 0)
                    {
                        Trace.WriteLine(counter);
                    }
                    //if ((matrix.RightMost - matrix.LeftMost <= 300) && (matrix.BottomMost - matrix.TopMost <= 300))
                    //{
                    //    Console.WriteLine(counter);
                    //    matrix.Print();
                    //}
                    matrix.Move();
                }
                //if ((matrix.RightMost - matrix.LeftMost <= 150) && (matrix.BottomMost - matrix.TopMost <= 150))
                //{
                Trace.WriteLine(counter++);
                matrix.Print();
                matrix.Move();
                //}
            }
        }
    }
}
