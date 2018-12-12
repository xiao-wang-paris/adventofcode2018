using System;
using System.Collections.Generic;

namespace AdventCalendar.day3
{
    public class Square : IComparable<Square>
    {
        public Square(Interval horizental, Interval vertical, int id = 0)
        {
            Id = id;
            Horizental = horizental;
            Vertical = vertical;
        }

        public int Id { get; set; }
        public Interval Horizental { get; set; }
        public Interval Vertical { get; set; }

        public int CompareTo(Square other)
        {
            if (other == null)
            {
                return 1;
            }
            if (this.Horizental.CompareTo(other.Horizental) != 0)
            {
                return this.Horizental.CompareTo(other.Horizental);
            }
            return this.Vertical.CompareTo(other.Vertical);
        }

        public Square Overlapped(Square other)
        {
            Interval horizontalIntersection = this.Horizental.Intersection(other.Horizental);
            Interval verticalIntersection = this.Vertical.Intersection(other.Vertical);
            if (horizontalIntersection == null || verticalIntersection == null)
            {
                return null;
            }
            return new Square(
                horizental: horizontalIntersection,
                vertical: verticalIntersection
            );
        }

        public int IncrementalOverlappedArea(List<Square> list)
        {
            int res = 0;
            int[][] matrix = new int[this.Horizental.Length][];
            for (int i = 0; i < this.Horizental.Length; i++)
            {
                matrix[i] = new int[this.Vertical.Length];
            }

            foreach (Square c in list)
            {
                Square overlapped = this.Overlapped(c);
                if (overlapped != null)
                {
                    for (int i = overlapped.Horizental.Start; i < overlapped.Horizental.End; i++)
                    {
                        for (int j = overlapped.Vertical.Start; j < overlapped.Vertical.End; j++)
                        {
                            matrix[i - this.Horizental.Start][j - this.Vertical.Start] = 1;
                        }
                    }
                }
            }

            for (int i = 0; i < this.Horizental.Length; i++)
            {
                for (int j = 0; j < this.Vertical.Length; j++)
                {
                    res += (1 - matrix[i][j]);
                }
            }
            return res;
        }

        public void Print()
        {
            Console.WriteLine($"{this.Horizental.Start}, {this.Horizental.End}, {this.Vertical.Start}, {this.Vertical.End}");
        }

        public static Square FromString(string s)
        {
            if (string.IsNullOrEmpty(s))
                return null;

            string[] str = s.Split(new char[] { '@', '#', ',', ':', 'x' });
            int id = int.Parse(str[1]);
            int leftMargin = int.Parse(str[2]);
            int topMargin = int.Parse(str[3]);
            int width = int.Parse(str[4]);
            int height = int.Parse(str[5]);

            return new Square
            (
                id: id,
                horizental: new Interval(start: leftMargin, end: leftMargin + width),
                vertical: new Interval(start: topMargin, end: topMargin + height)
            );
        }
    }
}
