using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventCalendar.day4
{
    public enum State
    {
        awake = 0,
        asleep,
        shift
    }

    public class Record : IComparable<Record>
    {
        public DateTime TimeStamp { get; set; }
        public State CurrentState { get; set; }

        public int Guard { get; set; }

        public static Record FromString(string line)
        {
            var datetimeStr = line.Substring(1, 16);
            string stateStr = line.Substring(19);
            DateTime datetime = DateTime.Parse(datetimeStr);
            State state;
            int numGuard = -1;
            switch (stateStr)
            {
                case "falls asleep":
                    state = State.asleep;
                    break;
                case "wakes up":
                    state = State.awake;
                    break;
                default:
                    state = State.shift;
                    string numGuardStr = stateStr.Split(new char[] { '#', ' ' })[2];
                    numGuard = int.Parse(numGuardStr);
                    break;
            }
            return new Record()
            {
                TimeStamp = datetime,
                CurrentState = state,
                Guard = numGuard,
            };
        }

        public int CompareTo(Record other)
        {
            if (other == null)
                return 1;
            return DateTime.Compare(this.TimeStamp, other.TimeStamp);
        }
    }

    public class PuzzleSolver
    {
        private readonly string inputPath;
        public PuzzleSolver(string path)
        {
            inputPath = path;
        }

        public int ComputeProductOfIdAndMinute()
        {
            Dictionary<DateTime, int> dateToGuardMap = new Dictionary<DateTime, int>();
            Dictionary<DateTime, List<Record>> map = new Dictionary<DateTime, List<Record>>();
            Dictionary<int, int[]> guardTimeAggr = new Dictionary<int, int[]>();

            string[] lines = System.IO.File.ReadAllLines(inputPath);
            //parse 
            foreach (var line in lines)
            {
                var record = Record.FromString(line);
                switch (record.CurrentState)
                {
                    case State.shift:
                        var date = record.TimeStamp.Date;
                        if (record.TimeStamp.Hour > 0)
                        {
                            date = date.AddDays(1);
                        }
                        dateToGuardMap.Add(date, record.Guard);
                        break;
                    case State.asleep:
                    case State.awake:
                        if (!map.ContainsKey(record.TimeStamp.Date))
                        {
                            map.Add(record.TimeStamp.Date, new List<Record>());
                        }
                        map[record.TimeStamp.Date].Add(record);
                        break;
                    default:
                        break;
                }
            }

            foreach (List<Record> list in map.Values)
            {
                list.Sort();

                int[] asleep = new int[60];
                for (int i = 0; i < list.Count - 1; i++)
                {
                    if (list[i].CurrentState == State.asleep)
                    {
                        var next = list[i + 1].TimeStamp.Minute;
                        for (int j = list[i].TimeStamp.Minute; j < next; j++)
                        {
                            asleep[j] = 1;
                        }
                    }
                }
                int guardNum = dateToGuardMap[list[0].TimeStamp.Date];
                if (!guardTimeAggr.ContainsKey(guardNum))
                {
                    guardTimeAggr.Add(guardNum, new int[60]);
                }
                var prevAggreg = guardTimeAggr[guardNum];
                for (int i = 0; i < 60; i++)
                {
                    prevAggreg[i] += asleep[i];
                }
            }

            int maxGuard = -1;
            int maxAsleepTime = -1;
            foreach (int guard in guardTimeAggr.Keys)
            {
                int sum = guardTimeAggr[guard].Sum(); 
                if (sum > maxAsleepTime)
                {
                    maxAsleepTime = sum;
                    maxGuard = guard;
                }
            }

            int[] aggregList = guardTimeAggr[maxGuard];
            int maxIndex = Array.IndexOf(aggregList,aggregList.Max());
            Console.WriteLine(maxIndex * maxGuard);

            maxIndex = -1;
            maxGuard = -1;
            maxAsleepTime = -1;
            foreach (int guard in guardTimeAggr.Keys)
            {
                int[] l = guardTimeAggr[guard];
                var maxOfGuard = l.Max();
                if(maxOfGuard > maxAsleepTime)
                {
                    maxAsleepTime = maxOfGuard;
                    maxGuard = guard;
                    maxIndex = Array.IndexOf(l, maxOfGuard);
                }
            }
            Console.WriteLine(maxGuard * maxIndex);
            return 0;
        }
    }
}
