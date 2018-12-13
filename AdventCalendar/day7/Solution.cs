using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventCalendar.day7
{
    public class Point
    {
        public  Point(char name)
        {
            this.Name = name;
            Children = new List<Point>();
            Ancestor = new List<Point>();
        }
        public List<Point> Children { get; set; }
        public List<Point> Ancestor { get; set; }
        public char Name { get; set; }
    }

    //topologicaltree
    class Solution
    {
        private readonly string inputPath;
        public Solution(string path)
        {
            inputPath = path;
        }
        public string GetPath()
        {
            HashSet<Point> points = new HashSet<Point>();
            var dict = new Dictionary<char, Point>();
            string[] lines = System.IO.File.ReadAllLines(inputPath);
            foreach (string line in lines)
            {
                var arr = line.ToCharArray();
                char c1 = line.ToCharArray()[5];
                char c2 = line.ToCharArray()[36];
                if (!dict.ContainsKey(c1))
                {
                    dict.Add(c1, new Point(c1));
                }
                if (!dict.ContainsKey(c2))
                {
                    dict.Add(c2, new Point(c2));
                }
                dict[c1].Children.Add(dict[c2]);
                dict[c2].Ancestor.Add(dict[c1]);
            }
            List<char> res = new List<char>();
            List<char> queue = new List<char>();
            foreach(char c in dict.Keys)
            {
                if(dict[c].Ancestor.Count == 0)
                {
                    queue.Add(c);
                }
            }

            while(queue.Count > 0)
            {
                queue.Sort();
                var next = queue[0];
                res.Add(next);
                queue.RemoveAt(0);
                foreach (Point child in dict[next].Children)
                {
                    child.Ancestor.Remove(dict[next]);
                    if(child.Ancestor.Count == 0)
                    {
                        queue.Add(child.Name);
                    }
                }
            }
            return new string(res.ToArray());
        }
    }
}
