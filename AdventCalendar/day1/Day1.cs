using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventCalendar.day1
{
    public class Day1
    {
        private readonly string input;
        public Day1(string input)
        {
            this.input = input;
        }
        public long ComputeFrequence()
        {
            string[] text = System.IO.File.ReadAllLines(input);
            long sum = 0;
            foreach (string line in text)
            {
                long num = long.Parse(line);
                sum += num;
            }
            return sum;
        }

        public long ComputeFirstDuplicate()
        {
            long sum = 0;
            long duplicate = 0;
            bool foundDuplciate = false;
            HashSet<long> set = new HashSet<long> { 0 };
            string[] text = System.IO.File.ReadAllLines(input);

            while (!foundDuplciate)
            {
                foreach (string line in text)
                {
                    sum += long.Parse(line);
                    if (set.Contains(sum))
                    {
                        duplicate = sum;
                        foundDuplciate = true;
                        break;
                    }
                    else
                    {
                        set.Add(sum);
                    }
                }
            }
            return duplicate;
        }
    }
}
