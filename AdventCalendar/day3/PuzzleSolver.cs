using System;
using System.Collections.Generic;

namespace AdventCalendar.day3
{
    //D2 interval overlapping problem 

    public class Interval : IComparable<Interval>
    {
        public int Start { get; set; }
        public int End { get; set; }

        public int CompareTo(Interval other)
        {
            if (other == null)
                return 1;
            if (this.Start != other.Start)
            {
                return Math.Sign(this.Start - other.Start);
            }
            return Math.Sign(this.End - other.End);
        }

        public Interval Intersection(Interval other)
        {
            Interval smaller = null;
            Interval bigger = null;
            if (this.CompareTo(other) < 0)
            {
                smaller = this;
                bigger = other;
            }
            else//(this.CompareTo(other) > 0)
            {
                smaller = other;
                bigger = this;
            }
            if (bigger.Start >= smaller.End)
            {
                return null;
            }
            return new Interval
            {
                Start = bigger.Start,
                End = Math.Min(bigger.End, smaller.End)
            };
        }

        public int Length
        {
            get
            {
                return Math.Max(0, this.End - this.Start);
            }
        }
    }
    public class Claim : Size, IComparable<Claim>
    {
        public int Id { get; set; }

        public Interval Horizental { get; set; }
        public Interval Vertical { get; set; }

        public int CompareTo(Claim other)
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

        public Claim Overlapped(Claim other)
        {
            Interval horizontalOverlap = this.Horizental.Intersection(other.Horizental);
            Interval verticalOverlap = this.Vertical.Intersection(other.Vertical);
            if (horizontalOverlap == null || verticalOverlap == null)
            {
                return null;
            }
            return new Claim()
            {
                Horizental = horizontalOverlap,
                Vertical = verticalOverlap,
            };
        }

        public int IncrementalArea(List<Claim> list)
        {
            int res = 0;
            int[][] matrix = new int[this.Horizental.Length][];
            for (int i = 0; i < this.Horizental.Length; i++)
            {
                matrix[i] = new int[this.Vertical.Length];
            }

            foreach (Claim c in list)
            {
                Claim overlapped = this.Overlapped(c);
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
    }

    public class Size
    {
        public int Height { get; set; }
        public int Width { get; set; }
    }

    public class PuzzleSolver
    {
        private readonly string inputPath;

        public PuzzleSolver(string path)
        {
            inputPath = path;
        }

        private Claim ParseFromString(string s)
        {
            if (string.IsNullOrEmpty(s))
                return null;
            string[] str = s.Split(new char[] { '@', '#', ',', ':', 'x' });
            return new Claim
            {
                Id = int.Parse(str[1]),
                Horizental = new Interval() { Start = int.Parse(str[2]), End = int.Parse(str[2]) + int.Parse(str[4]) },
                Vertical = new Interval() { Start = int.Parse(str[3]), End = int.Parse(str[3]) + int.Parse(str[5]) },
            };
        }

        //question 1 
        public int CountDuplicateSquareClaims()
        {
            string[] lines = System.IO.File.ReadAllLines(inputPath);
            //parse 
            var list = new List<Claim>();
            foreach (var line in lines)
            {
                var claim = ParseFromString(line);
                list.Add(claim);
            }

            //sort list 
            list.Sort();

            //compute overlapped area 
            var overlappedList = new List<Claim>();
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = i + 1; j < list.Count; j++)
                {
                    Claim c1 = list[i];
                    Claim c2 = list[j];
                    var overlap = c1.Overlapped(c2);
                    if (overlap != null)
                    {
                        //c1.Print();
                        //c2.Print();
                        //overlap.Print();
                        //Console.WriteLine();
                        overlappedList.Add(overlap);
                    }
                }
            }

            //compute area
            overlappedList.Sort();
            int sumArea = 0;
            List<Claim> merged = new List<Claim>();
            foreach (Claim c in overlappedList)
            {
                sumArea += c.IncrementalArea(merged);
                merged.Add(c);
            }
            return sumArea;
        }

        public int FindNotOverlappedId()
        {
            string[] lines = System.IO.File.ReadAllLines(inputPath);
            //parse 
            var list = new List<Claim>();
            foreach (var line in lines)
            {
                var claim = ParseFromString(line);
                list.Add(claim);
            }

            //sort list 
            list.Sort();

            bool[] hasOverlap = new bool[list.Count];
            //compute overlapped area 
            var overlappedList = new List<Claim>();
            for (int i = 0; i < list.Count; i++)
            {
                for (int j = i + 1; j < list.Count; j++)
                {
                    Claim c1 = list[i];
                    Claim c2 = list[j];
                    var overlap = c1.Overlapped(c2);
                    if (overlap != null)
                    {
                        overlappedList.Add(overlap);
                        hasOverlap[i] = true;
                        hasOverlap[j] = true;
                    }
                }
            }

            for(int i=0;i<hasOverlap.Length; i++)
            {
                if (!hasOverlap[i])
                {
                    return list[i].Id;
                }
            }
            return -1;
        }
    }
}
