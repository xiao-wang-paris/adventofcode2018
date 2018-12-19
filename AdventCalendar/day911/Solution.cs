using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventCalendar.day11
{
    class Solution
    {
        private int GetFuel(int x, int y, int secret)
        {
            var rackId = x + 10;
            var powerLevel = ((rackId * y + secret) * rackId) / 100 % 10 - 5;
            return powerLevel;
        }

        int[][] matrix = new int[301][];
        int[][] power = new int[301][];
        int[][] res = new int[301][];
        public long GetMaxFuelCellCoordinate()
        {
            int secret = 1955;
            for (int i = 0; i < 301; i++)
            {
                matrix[i] = new int[301];
                power[i] = new int[301];
                res[i] = new int[301];
            }

            for (int i = 1; i < 301; i++)
            {
                for (int j = 1; j < 301; j++)
                {
                    power[i][j] = GetFuel(i, j, secret);
                }
            }
            int max = 0;
            int max_x = 0;
            int max_y = 0;
            int square_size = 0;
            for (int s = 1; s <= 300; s++)
            {
                Console.WriteLine(s);
                for (int i = 1; i <= 301 - s; i++)
                {
                    for (int j = 1; j <= 301 - s; j++)
                    {
                        res[i][j] = 0;
                        for (int m = 0; m < s; m++)
                        {
                            for (int n = 0; n < s; n++)
                            {
                                res[i][j] += power[i + m][j + n];
                            }
                        }
                        max = Math.Max(max, res[i][j]);
                        if (max == res[i][j])
                        {
                            max_x = i;
                            max_y = j;
                            square_size = s;
                        }
                    }
                }
            }

            Console.WriteLine(max_x);
            Console.WriteLine(max_y);
            Console.WriteLine(square_size);
            Console.WriteLine(max);
            return -1;
            //Console.WriteLine(GetFuel(122, 79, 57));
            //Console.WriteLine(GetFuel(217, 196, 39));
            //Console.WriteLine(GetFuel(101, 153, 71));
            //return GetFuel(3, 5, 8);
        }

    }
}

