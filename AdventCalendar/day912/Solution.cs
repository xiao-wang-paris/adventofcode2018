using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventCalendar.day12
{
    public class State
    {
        public State(string negStr, string posStr)
        {
            //NegState = negStr.TrimStart('.');
            //PosState = posStr.TrimEnd('.');

            //string trimmedPosState = PosState.TrimStart('.');
            //PositiveShift = PosState.Length - trimmedPosState.Length;

            //PosState = trimmedPosState;

            NegState = negStr;
            PosState = posStr;
        }

        public string NegState { get; set; }
        public string PosState { get; set; }
        public int PositiveShift { get; set; }
        public int NegShift { get; set; }
        public int CountPlant()
        {
            var res = 0;
            for (int i = 0; i < NegState.Length; i++)
            {
                if (NegState.ToCharArray()[i] == '#')
                {
                    res += i - NegState.Length;
                }
            }
            for (int i = 0; i < PosState.Length; i++)
            {
                if (PosState.ToCharArray()[i] == '#')
                {
                    res += i + PositiveShift;
                }
            }
            return res;
        }
        public static State FromString(string s)
        {
            string[] str = s.Split(new string[] { "initial state: " }, StringSplitOptions.None);
            return new State("", str[1]);
        }
        public static State NextGeneration(State s, List<Rule> rules)
        {
            //if(s.PositiveShift == 0)
            string str = s.NegState + s.PosState;
            string init = "...." + str + "....";
            string res = "";
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
            return new State(res.Substring(0, 2 + s.NegState.Length), res.Substring(2 + s.NegState.Length));
        }

        public void Print()
        {
            Console.WriteLine(NegState + "%" + PosState.TrimStart('.'));
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
            for (int g = 1; g <= 20; g++)
            {
                state = State.NextGeneration(state, list);
                //state.Print();
            }
            state.Print();
            //for (int g = 1; g < 10; g++)
            //{
            //    state = State.NextGeneration(state, list);
            //    state.Print();
            //}
            Console.WriteLine(state.CountPlant());
        }
    }
}

