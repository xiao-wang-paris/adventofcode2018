using System.Collections.Generic;
using System.Linq;

namespace AdventCalendar.day6
{
    public class Solution
    {
        private readonly string inputPath;
        public Solution(string path)
        {
            inputPath = path;
        }

        private char FindNearest(int x, int y, List<Point> points)
        {
            List<Point> distance = new List<Point>(points);
            List<int> distances = distance.Select(p => p.Distance(x, y)).ToList();
            int minD = distances.Min();
            int count = distances.Count(p => p == minD);
            if (count > 1)
            {
                return '.';
            }
            else
            {
                int ind = distances.FindIndex(p => p == minD);
                return distance[ind].Closest;
            }
        }

        private int ComputeTotalDistance(int x, int y, List<Point> points)
        {
            List<Point> distance = new List<Point>(points);
            List<int> distances = distance.Select(p => p.Distance(x, y)).ToList();
            return distances.Sum();
        }

        public int ComputeArea()
        {
            string[] lines = System.IO.File.ReadAllLines(inputPath);
            List<Point> points = new List<Point>();
            char symbol = 'A';
            foreach (string line in lines)
            {
                points.Add(Point.FromString(line, symbol++));
            }

            int rightborder = points.Max(p => p.X);
            int downborder = points.Max(p => p.Y);

            char[][] matrix = new char[rightborder + 2][];
            for (int i = 0; i <= rightborder + 1; i++)
            {
                matrix[i] = new char[downborder + 2];
            }
            foreach (Point p in points)
            {
                matrix[p.X][p.Y] = p.Closest;
            }

            var counter = new Dictionary<char, int>();
            //number cases 
            for (int i = 0; i <= rightborder + 1; i++)
            {
                for (int j = 0; j <= downborder + 1; j++)
                {
                    //find nearest point
                    char nearest = FindNearest(i, j, points);
                    matrix[i][j] = nearest;

                    if (!counter.ContainsKey(nearest))
                    {
                        counter.Add(nearest, 1);
                    }
                    else
                    {
                        var val = counter[nearest];
                        counter.Remove(nearest);
                        counter.Add(nearest, val + 1);
                    }
                }
            }
            //remove borders
            for (int i = 0; i <= rightborder + 1; i++)
            {
                char nearest = matrix[i][0];
                if (counter.ContainsKey(nearest))
                {
                    counter.Remove(nearest);
                }
                nearest = matrix[i][downborder + 1];
                if (counter.ContainsKey(nearest))
                {
                    counter.Remove(nearest);
                }
            }

            for (int i = 0; i <= downborder + 1; i++)
            {
                char nearest = matrix[0][i];
                if (counter.ContainsKey(nearest))
                {
                    counter.Remove(nearest);
                }
                nearest = matrix[rightborder + 1][i];
                if (counter.ContainsKey(nearest))
                {
                    counter.Remove(nearest);
                }
            }
            counter.Remove('.');
            return counter.Values.Max();
        }

        public int GetRegionSize(int limit)
        {
            string[] lines = System.IO.File.ReadAllLines(inputPath);
            List<Point> points = new List<Point>();
            char symbol = 'A';
            foreach (string line in lines)
            {
                points.Add(Point.FromString(line, symbol++));
            }

            int rightborder = points.Max(p => p.X);
            int downborder = points.Max(p => p.Y);

            char[][] matrix = new char[rightborder + 2][];
            for (int i = 0; i <= rightborder + 1; i++)
            {
                matrix[i] = new char[downborder + 1];
            }
            foreach (Point p in points)
            {
                matrix[p.X][p.Y] = p.Closest;
            }

            var counter = 0;
            //number cases 
            for (int i = 0; i <= rightborder + 1; i++)
            {
                for (int j = 0; j <= downborder + 1; j++)
                {
                    //find nearest point
                    int distance = ComputeTotalDistance(i, j, points);
                    if (distance < limit)
                    {
                        counter++;
                    }
                }
            }
            return counter;
        }
    }
}
