using System;
using System.Text;

namespace AdventCalendar.day2
{
    public class RepeatCounter
    {
        public int DoubleCount { get; set; }
        public int TripleCount { get; set; }
    }

    public class Solution
    {
        private readonly string inputPath;
        public Solution(string path)
        {
            inputPath = path;
        }

        private RepeatCounter Count(string s)
        {
            int[] counter = new int[26];
            var res = new RepeatCounter();
            foreach(char c in s.ToCharArray())
            {
                counter[(c - 'a')]++;
            }
            foreach(int count in counter)
            {
                if(count == 2)
                {
                    res.DoubleCount = 1;
                }else if(count == 3)
                {
                    res.TripleCount = 1;
                }
            }
            return res;
        }

        //question 1 
        public int GetCheckSum()
        {
            string[] ids = System.IO.File.ReadAllLines(inputPath);
            var res = new RepeatCounter();
            foreach (string line in ids)
            {
                var countId = Count(line);
                res.DoubleCount += countId.DoubleCount;
                res.TripleCount += countId.TripleCount;
            }
            return res.DoubleCount * res.TripleCount;
        }

        private int Distance(string s1, string s2)
        {
            if(s1.Length != s2.Length)
                return Math.Min(s1.Length, s2.Length);

            var dist = 0;
            char[] arr1 = s1.ToCharArray();
            char[] arr2 = s2.ToCharArray();

            for (int i=0;i<s1.Length; i++)
            {
                if (arr1[i] != arr2[i])
                    dist++; 
            }
            return dist;
        }

        private string CommonString(string s1, string s2)
        {
            if (s1.Length != s2.Length)
                return null;
            StringBuilder common = new StringBuilder();
            char[] arr1 = s1.ToCharArray();
            char[] arr2 = s2.ToCharArray();

            for (int i = 0; i < s1.Length; i++)
            {
                if (arr1[i] == arr2[i])
                    common.Append(arr1[i]);
            }
            return common.ToString();
        }

        //question2 
        public string GetCommonString()
        {
            string[] ids = System.IO.File.ReadAllLines(inputPath);
            var res = new RepeatCounter();
            for(int i=0; i<ids.Length; i++)
            {
                for(int j=i+1; j<ids.Length; j++)
                {
                    string id1 = ids[i];
                    string id2 = ids[j];
                    if(Distance(id1, id2) == 1)
                    {
                        return CommonString(id1, id2);
                    }
                }
            }
            return null;
        }
    }
}
