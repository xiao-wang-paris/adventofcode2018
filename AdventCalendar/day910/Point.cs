using System;

namespace AdventCalendar.day10
{
    public class Point
    {
        public Position Pos { get; set; }
        public Velocity Vel { get; set; }

        public static Point FromString(string s)
        {
            string[] str = s.Split(new string[] { "position=<", ",", ">", "velocity=<", " " }, StringSplitOptions.RemoveEmptyEntries);
            return new Point
            {
                Pos = new Position()
                {
                    X = int.Parse(str[0]),
                    Y = int.Parse(str[1])
                },
                Vel = new Velocity()
                {
                    Dx = int.Parse(str[2]),
                    Dy = int.Parse(str[3])
                }
            };
        }

        public void Update()
        {
            Pos.X += Vel.Dx;
            Pos.Y += Vel.Dy;
        }
    }
}
