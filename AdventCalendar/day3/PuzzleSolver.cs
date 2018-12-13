using System.Collections.Generic;

namespace AdventCalendar.day3
{
    public class Solution
    {
        private readonly string inputPath;
        private readonly List<Square> squareList;

        public Solution(string path)
        {
            inputPath = path;

            string[] lines = System.IO.File.ReadAllLines(inputPath);
            //parse 
            this.squareList = new List<Square>();
            foreach (var line in lines)
            {
                var claim = Square.FromString(line);
                this.squareList.Add(claim);
            }
            this.squareList.Sort();
        }

        //question 1 
        public int CountDuplicateSquareClaims()
        {
            //compute overlapped area 
            var overlappedList = new List<Square>();
            for (int i = 0; i < this.squareList.Count; i++)
            {
                for (int j = i + 1; j < this.squareList.Count; j++)
                {
                    Square c1 = this.squareList[i];
                    Square c2 = this.squareList[j];
                    var overlap = c1.Overlapped(c2);
                    if (overlap != null)
                    {
                        overlappedList.Add(overlap);
                    }
                }
            }

            //compute area
            overlappedList.Sort();
            int sumArea = 0;
            List<Square> merged = new List<Square>();
            foreach (Square c in overlappedList)
            {
                sumArea += c.IncrementalOverlappedArea(merged);
                merged.Add(c);
            }
            return sumArea;
        }

        public int FindNotOverlappedId()
        {
            bool[] hasOverlap = new bool[this.squareList.Count];
            var overlappedList = new List<Square>();
            for (int i = 0; i < this.squareList.Count; i++)
            {
                for (int j = i + 1; j < this.squareList.Count; j++)
                {
                    Square c1 = this.squareList[i];
                    Square c2 = this.squareList[j];
                    var overlap = c1.Overlapped(c2);
                    if (overlap != null)
                    {
                        overlappedList.Add(overlap);
                        hasOverlap[i] = true;
                        hasOverlap[j] = true;
                    }
                }
            }

            for (int i = 0; i < hasOverlap.Length; i++)
            {
                if (!hasOverlap[i])
                {
                    return this.squareList[i].Id;
                }
            }
            return -1;
        }
    }
}
