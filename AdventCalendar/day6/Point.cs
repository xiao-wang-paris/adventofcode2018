using System;

namespace AdventCalendar.day6
{
    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }
        public char Closest { get; set; }

        public int Distance(int x, int y)
        {
            return Math.Abs(X - x) + Math.Abs(Y - y);
        }

        public static Point FromString(string s, char symbol)
        {
            if (string.IsNullOrEmpty(s))
                return null;
            string[] coordinates = s.Split(',');
            return new Point()
            {
                X = int.Parse(coordinates[0]),
                Y = int.Parse(coordinates[1]),
                Closest = symbol
            };
        }
    }
}
