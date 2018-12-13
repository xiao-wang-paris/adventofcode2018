using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventCalendar.day5
{
    public class Solution
    {
        private readonly string inputPath;
        public Solution(string path)
        {
            inputPath = path;
        }

        public int React()
        {
            string line = System.IO.File.ReadAllText(inputPath);
            List<Char> l = new List<char>(line.ToCharArray());
            return GetPolymerLength(l);
        }

        private int GetPolymerLength(List<char> l)
        {
            int ptr1 = 0;
            while (ptr1 < l.Count - 1 && ptr1 >= 0)
            {
                char c1 = l[ptr1];
                char c2 = l[ptr1 + 1];
                if (Math.Abs(c1 - c2) == Math.Abs('A' - 'a'))
                {
                    l.RemoveAt(ptr1);
                    l.RemoveAt(ptr1);
                    ptr1 = Math.Max(0, ptr1 - 1);
                }
                else
                {
                    ptr1++;
                }
            }
            return l.Count;

        }

        public int ReactUpdated()
        {
            string line = System.IO.File.ReadAllText(inputPath);

            bool[] appeared = new bool[26];
            var set = new HashSet<int>();
            for(int i=0; i<26; i++)
            {
                set.Add(i);
            }

            foreach(char c in line.ToArray())
            {
                if(c >='a' && c <= 'z')
                {
                    appeared[c - 'a'] = true;
                    set.Remove(c - 'a');
                }
                if(c >='A' && c <= 'Z')
                {
                    appeared[c - 'A'] = true;
                    set.Remove(c - 'A');
                }
                if (set.Count == 0)
                    break;
            }

            int[] Res = new int[26];
            for(int i=0; i<26; i++)
            {
                if (!appeared[i])
                {
                    continue;
                }
                List<char> l = new List<char>(line.ToCharArray());
                l.RemoveAll(p => p == ('a' + i) || (p == ('A' + i)));
                Res[i] = GetPolymerLength(l);
            }
            return Res.Min();
        }
    }
}
