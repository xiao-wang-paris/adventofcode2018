using System;
using System.Collections.Generic;

namespace AdventCalendar.day7
{
    //topologicaltree
    class Solution
    {
        private readonly string inputPath;
        public Solution(string path)
        {
            inputPath = path;
        }

        private HashSet<Point> points;
        private Dictionary<char, Point> dict;

        private void Init()
        {
            points = new HashSet<Point>();
            dict = new Dictionary<char, Point>();

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
        }

        public string GetPath()
        {
            Init();

            List<char> queue = new List<char>() { };
            foreach (char c in dict.Keys)
            {
                if (dict[c].Ancestor.Count == 0)
                {
                    queue.Add(c);
                }
            }
            List<char> res = new List<char>();
            while (queue.Count > 0)
            {
                queue.Sort();
                var next = queue[0];
                res.Add(next);
                queue.RemoveAt(0);
                foreach (Point child in dict[next].Children)
                {
                    child.Ancestor.Remove(dict[next]);
                    if (child.Ancestor.Count == 0)
                    {
                        queue.Add(child.Name);
                    }
                }
            }
            return new string(res.ToArray());
        }

        private int GetDuration(char c)
        {
            return (c - 'A' + 1 + 60 );
        }

        public int ComputeCompletionTime(int numWorkers)
        {
            Init();
            var workerRemainingTime = new Dictionary<char, int>();

            int res = 0;
            foreach (char c in dict.Keys)
            {
                if (dict[c].Ancestor.Count == 0)
                {
                    workerRemainingTime.Add(c, GetDuration(c));
                }
            }

            while (workerRemainingTime.Count > 0)
            {
                res++;
                var currentKeys = new List<char>(workerRemainingTime.Keys);
                for(int i=0; i<Math.Min(currentKeys.Count, numWorkers); i++)
                {
                    char worker = currentKeys[i];
                    workerRemainingTime[worker]--;
                    if (workerRemainingTime[worker] == 0)
                    {
                        foreach (Point child in dict[worker].Children)
                        {
                            child.Ancestor.Remove(dict[worker]);
                            if (child.Ancestor.Count == 0)
                            {
                                workerRemainingTime.Add(child.Name, GetDuration(child.Name));
                            }
                        }
                        workerRemainingTime.Remove(worker);
                    }
                }
            }
            return res;
        }
    }
}
