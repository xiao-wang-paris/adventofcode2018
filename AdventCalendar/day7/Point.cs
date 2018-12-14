using System.Collections.Generic;

namespace AdventCalendar.day7
{
    public class Point
    {
        public Point(char name)
        {
            this.Name = name;
            Children = new List<Point>();
            Ancestor = new List<Point>();
        }
        public List<Point> Children { get; set; }
        public List<Point> Ancestor { get; set; }
        public char Name { get; set; }
    }
}
