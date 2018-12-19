using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventCalendar.day12
{
    public class State
    {
        public State(string state, int shift)
        {
            Shift = shift;
            PosState = state;
        }

        public string PosState { get; set; }
        public int Shift { get; set; }
        public int CountPlant()
        {
            var res = 0;
            for (int i = 0; i < PosState.Length; i++)
            {
                if (PosState.ToCharArray()[i] == '#')
                {
                    res += i + Shift;
                }
            }
            return res;
        }
        public static State FromString(string s)
        {
            string[] str = s.Split(new string[] { "initial state: " }, StringSplitOptions.None);
            return new State(str[1], 0);
        }
        public static State NextGeneration(State s, List<Rule> rules)
        {
            string res = "";
            string init = "...." + s.PosState + "....";

            for (int i = 2; i < init.Length - 2; i++)
            {
                char c = '.';
                foreach (Rule r in rules)
                {
                    if (init.Substring(i - 2, 5).Equals(r.Input.ToString()))
                    {
                        c = r.Output;
                        break;
                    }
                }
                res += c;
            }
            int countLeadingSymbol = res.Length - res.TrimStart('.').Length;
            res = res.Trim('.');
            return new State(res, s.Shift - 2 + countLeadingSymbol);
        }

        public void Print()
        {
            Console.WriteLine(PosState);
        }
    }
    public class Rule
    {
        public string Input { get; set; }
        public char Output { get; set; }
        public static Rule FromString(string s)
        {
            string[] str = s.Split(new string[] { " => " }, StringSplitOptions.None);
            return new Rule()
            {
                Input = str[0],
                Output = str[1][0],
            };
        }
    }
    class Solution
    {
        private readonly string inputPath;
        public Solution(string path)
        {
            inputPath = path;
        }

        public void GeneratePlant()
        {
            //parsing
            var lines = System.IO.File.ReadLines(inputPath).ToArray();
            var state = State.FromString(lines[0]);
            var list = new List<Rule>();
            for (int i = 2; i < lines.Count(); i++)
            {
                list.Add(Rule.FromString(lines[i]));
            }

            var generation = 50_000_000_000;
            var prev = 0;
            for (int g = 1; g <= 200; g++)
            {
                state = State.NextGeneration(state, list);
                //var count = state.CountPlant();
                //Console.WriteLine($"{g}: {count}, increase:{count - prev}");
                //prev = count;
                //state.Print();
            }

            Console.WriteLine(17904 + (88 * (generation - 200)));
        }
    }
}

