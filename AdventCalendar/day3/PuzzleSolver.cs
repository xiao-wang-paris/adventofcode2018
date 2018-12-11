using System;
using System.Collections.Generic;

namespace AdventCalendar.day3
{
    //D2 interval overlapping problem 

    public class Interval
    {
        public int Start { get; set; }
        public int End { get; set; }
    }
    public class Claim : Size, IComparable<Claim>
    {
        public int Id { get; set; }
        
        public int LeftMargin { get; set; }
        public int UpMargin { get; set; }
        

        public Interval Horizental { get; set; }
        public Interval Vertical { get; set; }

        public int CompareTo(Claim other)
        {
            if(other == null)
            {
                return 1;
            }
            if(this.LeftMargin < other.LeftMargin)
            {
                return -1;
            }
        }
    }

    public class Size
    {
        public int Height { get; set; }
        public int Width { get; set; }
    }

    public class PuzzleSolver
    {
        private List<Claim> claims = new List<Claim>();
        private Size areaSize;
        private readonly string inputPath;
        private int[][] matrix = null;

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
                LeftMargin = int.Parse(str[2]),
                UpMargin = int.Parse(str[3]),
                RightmostPos = int.Parse(str[2]) + int.Parse(str[4]),
                DownmostPos = int.Parse(str[3]) + int.Parse(str[5])
            };
        }

        private void GetAreaSize(string[] lines)
        {
            int width = 0;
            int height = 0;
            foreach (string line in lines)
            {
                Claim c = this.ParseFromString(line);
                this.claims.Add(c);
                width = Math.Max(width, c.LeftMargin + width);
                height = Math.Max(height, c.UpMargin + height);
            }
            this.areaSize = new Size() { Width = width, Height = height };
        }

        private void DrawClaim(Claim c)
        {
            for (int i = c.LeftMargin; i < c.RightmostPos; i++)
            {
                for (int j = c.UpMargin; j < c.DownmostPos; j++)
                {
                    if (this.matrix[i][j] == 0)
                    {
                        this.matrix[i][j] = 1;
                    }
                    else if (this.matrix[i][j] == 1)
                    {
                        this.matrix[i][j] = -1;
                    }
                }
            }
        }

        private int CountOverlap()
        {
            int counter = 0;
            for (int i = 0; i < this.areaSize.Width; i++)
            {
                for (int j = 0; j < this.areaSize.Height; j++)
                {
                    if (matrix[i][j] == -1)
                    {
                        counter++;
                    }
                }
            }
            return counter;
        }

        //question 1 
        public int CountDuplicateSquareClaims()
        {
            string[] lines = System.IO.File.ReadAllLines(inputPath);
            //parse 
            GetAreaSize(lines);

            //sort list 

            //compute overlapped area 
            //merge with overlapping list 

            //compute area
            this.matrix = new int[this.areaSize.Width][];
            for (int i = 0; i < this.matrix.Length; i++)
            {
                this.matrix[i] = new int[this.areaSize.Height];
            }
            foreach (Claim c in this.claims)
            {
                DrawClaim(c);
            }
            return CountOverlap();
        }
    }
}
