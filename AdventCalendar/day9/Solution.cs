using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventCalendar.day9
{
    public class GameSet
    {
        public int NumPlayer { get; set; }
        public int LastMarblePoints { get; set; }
        public static GameSet FromString(string s)
        {
            string[] separators = new string[] { "players; last marble is worth", "points" };
            string[] str = s.Split(separators, StringSplitOptions.RemoveEmptyEntries);
            return new GameSet()
            {
                NumPlayer = int.Parse(str[0]),
                LastMarblePoints = int.Parse(str[1]),
            };
        }
    }

    public class CircularList
    {
        LinkedList<int> l = new LinkedList<int>();
        LinkedListNode<int> current;
        public CircularList()
        {
            l = new LinkedList<int>();
            current = l.AddFirst(0);
        }

        public void Insert(int i)
        {
            var node = current.Next ?? l.First;
            current = l.AddAfter(node, i);
        }

        public void Print()
        {
            for (int i = 0; i < l.Count; i++)
            {
                Console.Write(l.ElementAt(i) + " ");
            }
            Console.WriteLine("\t" + current.Value);
        }

        public int Remove(int v)
        {
            LinkedListNode<int> node = current;
            for (int i = 0; i < 7; i++)
            {
                node = node.Previous ?? l.Last;
            }
            current = node.Next ?? l.First;
            l.Remove(node);
            return node.Value + v;
        }
    }

    class Solution
    {
        private readonly string inputPath;
        public Solution(string path)
        {
            inputPath = path;
        }

        public long GetWinningScore()
        {
            var lines = System.IO.File.ReadLines(inputPath);
            var gameset = GameSet.FromString(lines.ElementAt(0));
            return GetScore2(gameset);
        }

        private long GetScore2(GameSet g)
        {
            var dict = new Dictionary<int, long>();
            var l = new CircularList();
            int currentPlayer = 0;
            for (int i = 1; i <= g.LastMarblePoints; i++)
            {
                //if (i % 71863 == 0)
                //{
                //    Console.WriteLine(i / 71863);
                //}
                if (i % 23 != 0 || i == 0)
                {
                    l.Insert(i);
                }
                else
                {
                    int score = l.Remove(i);
                    if (!dict.ContainsKey(currentPlayer))
                    {
                        dict.Add(currentPlayer, 0);
                    }
                    dict[currentPlayer] += score;
                }

                currentPlayer = (currentPlayer + 1) % g.NumPlayer;
            }
            return dict.Values.Max();

        }
        //private int GetScore(GameSet g)
        //{
        //    var dict = new Dictionary<int, int>();
        //    var l = new ArrayList();
        //    int currentIndx = 0;
        //    int currentPlayer = 0;
        //    for (int i = 0; i <= g.LastMarblePoints; i++)
        //    {
        //        if (i % 71863 == 0)
        //        {
        //            Console.WriteLine(i / 71863);
        //        }
        //        if (i % 23 != 0 || i == 0)
        //        {
        //            l.Insert(currentIndx, i);
        //        }
        //        else
        //        {
        //            int currentScore = i;
        //            int indexToRemove = (currentIndx - 9 + l.Count) % l.Count;
        //            currentScore += (int)l[indexToRemove];
        //            l.RemoveAt(indexToRemove);
        //            currentIndx = (indexToRemove) % l.Count;
        //            if (!dict.ContainsKey(currentPlayer))
        //            {
        //                dict.Add(currentPlayer, 0);
        //            }
        //            dict[currentPlayer] += currentScore;
        //        }

        //        //for (int j = 0; j < l.Count(); j++)
        //        //{
        //        //    Console.Write(l[j] + " ");
        //        //}
        //        //Console.WriteLine(l[currentIndx]);
        //        currentIndx = (currentIndx + 1) % l.Count + 1;
        //        currentPlayer = (currentPlayer + 1) % g.NumPlayer;
        //    }
        //    return dict.Values.Max();
        //}
    }
}
