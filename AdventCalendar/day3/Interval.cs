using System;

namespace AdventCalendar.day3
{
    //D2 interval overlapping problem 
    public class Interval : IComparable<Interval>
    {
        public Interval(int start, int end)
        {
            this.Start = start;
            this.End = end;
        }

        public int Start { get; private set; }
        public int End { get; private set; }

        public int CompareTo(Interval other)
        {
            if (other == null)
                return 1;
            if (this.Start != other.Start)
            {
                return Math.Sign(this.Start - other.Start);
            }
            return Math.Sign(this.End - other.End);
        }

        public Interval Intersection(Interval other)
        {
            Interval smaller = null;
            Interval bigger = null;
            if (this.CompareTo(other) < 0)
            {
                smaller = this;
                bigger = other;
            }
            else
            {
                smaller = other;
                bigger = this;
            }

            if (bigger.Start >= smaller.End)
            {
                return null;
            }
            return new Interval
            (
                start: bigger.Start,
                end: Math.Min(bigger.End, smaller.End)
            );
        }

        public int Length
        {
            get
            {
                return Math.Max(0, this.End - this.Start);
            }
        }
    }
}
