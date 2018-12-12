using System;
using System.Collections.Generic;

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
            this.Init();
        }

        private List<Record> list;
        private void Init()
        {
            this.list = new List<Record>();
            string[] lines = System.IO.File.ReadAllLines(inputPath);
            //parse 
            foreach (var line in lines)
            {
                var record = Record.FromString(line);
                list.Add(record);
            }
            //list.Sort();
        }

        public int ComputeProductOfIdAndMinute()
        {
            Dictionary<DateTime, int> dateToGuardMap = new Dictionary<DateTime, int>();
            Dictionary<DateTime, List<Record>> map = new Dictionary<DateTime, List<Record>>();
            Dictionary<int, List<int[]>> guardTime = new Dictionary<int, List<int[]>>();
            Dictionary<int, int[]> guardTimeAggr = new Dictionary<int, int[]>();
            foreach (Record r in list)
            {
                if (r.CurrentState == State.shift)
                {
                    var date = r.TimeStamp.Date;
                    if (r.TimeStamp.Hour > 0)
                    {
                        date = date.AddDays(1);
                    }
                    dateToGuardMap.Add(date, r.Guard);
                }
            }

            foreach (Record r in list)
            {
                if (r.CurrentState == State.shift)
                {
                    continue;
                }
                if (!map.ContainsKey(r.TimeStamp.Date))
                {
                    map.Add(r.TimeStamp.Date, new List<Record>());
                }
                map[r.TimeStamp.Date].Add(r);
            }

            foreach (List<Record> list in map.Values)
            {
                list.Sort();
                int[] asleep = new int[61];
                for (int i = 0; i < list.Count - 1; i++)
                {
                    if (list[i].CurrentState == State.awake)
                    {
                        continue;
                    }
                    //asleep 
                    var next = list[i + 1].TimeStamp.Minute;
                    for (int j = list[i].TimeStamp.Minute; j < next; j++)
                    {
                        asleep[j] = 1;
                    }
                }
                int guardNum = dateToGuardMap[list[0].TimeStamp.Date];
                if (!guardTime.ContainsKey(guardNum))
                {
                    guardTime.Add(guardNum, new List<int[]>());
                }
                guardTime[guardNum].Add(asleep);
            }

            //sum 
            int maxGuard = -1;
            int maxAsleepTime = -1;
            foreach (int guard in guardTime.Keys)
            {
                int[] aggreg = new int[61];
                int sum = 0;
                foreach (int[] list in guardTime[guard])
                {
                    for (int i = 0; i <= 60; i++)
                    {
                        aggreg[i] += list[i];
                        sum += list[i];
                    }
                }
                guardTimeAggr.Add(guard, aggreg);
                if (sum > maxAsleepTime)
                {
                    maxAsleepTime = sum;
                    maxGuard = guard;
                }


            }

            int maxIndex = -1;
            int temp = -1;
            int[] aggregList = guardTimeAggr[maxGuard];
            for (int i = 0; i < 60; i++)
            {
                if (aggregList[i] > temp)
                {
                    temp = aggregList[i];
                    maxIndex = i;
                }
            }
            Console.WriteLine(maxIndex * maxGuard);

            maxIndex = -1;
            maxGuard = -1;
            maxAsleepTime = -1;
            foreach(int guard in guardTimeAggr.Keys)
            {
                int[] l = guardTimeAggr[guard];
                for(int i=0; i<60; i++)
                {
                    if(l[i] > maxAsleepTime)
                    {
                        maxAsleepTime = l[i];
                        maxGuard = guard;
                        maxIndex = i;
                    }
                }
            }
            Console.WriteLine(maxGuard * maxIndex);
            return 0;
        }

    }
}
