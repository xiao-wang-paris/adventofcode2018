using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdventCalendar.day10
{
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
        }

        private static int[] DrawMatrix(List<Point> pl)
        {
            int leftmost = pl.Min(p => p.Pos.X);
            int rightmost = pl.Max(p => p.Pos.X);
            int topmost = pl.Min(p => p.Pos.Y);
            int bottommost = pl.Max(p => p.Pos.Y);

            return new int[] { leftmost, rightmost, topmost, bottommost };
        }
        public static Matrix FromPointList(List<Point> pl)
        {
            int[] res = DrawMatrix(pl);
            return new Matrix { LeftMost = res[0], RightMost = res[1], TopMost = res[2], BottomMost = res[3], PointList = pl };
        }

        public bool HasLetter()
        {
            if (RightMost - LeftMost > 1000 || BottomMost - TopMost > 1000)
                return false;

            int count = 0;
            foreach(Point point in PointList)
            {
                if(PointList.Count(p => (p.Pos.X == point.Pos.X) && (Math.Abs(p.Pos.Y - point.Pos.Y) < 3)) >= 4
                        || PointList.Count(p => (p.Pos.Y == point.Pos.Y) && (Math.Abs(p.Pos.X - point.Pos.X) < 3)) >= 4)
                {
                    count++;
                }
            }
            if (count > 5)
            {
                return true;
            }
            return false;
            //for (int i = LeftMost; i <= RightMost; i++)
            //{
            //    for (int j = TopMost; j <= BottomMost; j++)
            //    {
            //        if (PointList.Count(p => (p.Pos.X == i) && (Math.Abs(p.Pos.Y - j) < 4)) >= 6
            //            || PointList.Count(p => (p.Pos.Y == j) && (Math.Abs(p.Pos.X - i) < 4)) >= 6)
            //        {
            //            return true;
            //        }
            //    }
            //}
            //return false;
        }

        public void Print()
        {
            Trace.Write("\n");
            for (int i = TopMost; i < BottomMost + 1; i++)
            {
                for (int j = LeftMost; j < RightMost + 1; j++)
                {
                    if (PointList.Count(p => (p.Pos.Y == i) && (p.Pos.X == j)) == 1)
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
}
