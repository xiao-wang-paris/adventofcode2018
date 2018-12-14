using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventCalendar.day8
{
    class Node
    {
        public Node(int id)
        {
            Id = id;
            MetaData = new List<int>();
            Children = new List<Node>();
        }
        public int Id { get; set; }
        public int NumChildren { get; set; }
        public int NumMetaData { get; set; }
        public List<int> MetaData { get; set; }
        public List<Node> Children { get; set; }
    }
    class Solution
    {
        private readonly string inputPath;
        public Solution(string path)
        {
            inputPath = path;
        }

        public int GetMetaSum()
        {
            int id = 0;
            string text = System.IO.File.ReadAllText(inputPath);
            string[] numberStrs = text.Split(' ');
            var numbers = new List<string>(numberStrs).Select(p=> int.Parse(p)).ToList();
            var stack = new Stack<Node>();
            var res = new List<Node>();
            int ptr = 0;
            var point = new Node(id++)
            {
                NumChildren = numbers[ptr++],
                NumMetaData = numbers[ptr++],
            };
            stack.Push(point);
            while(stack.Count>0)
            {
                var child = new Node(id++)
                {
                    NumChildren = numbers[ptr++],
                    NumMetaData = numbers[ptr++],
                };
                if (child.NumChildren == 0)
                {
                    for (int i = 0; i < child.NumMetaData; i++)
                    {
                        child.MetaData.Add(numbers[ptr++]);
                    }
                    res.Add(child);
                    while (stack.Count > 0 && stack.Peek().NumChildren == 1)
                    {
                        var parent = stack.Pop();
                        parent.NumChildren--;
                        parent.Children.Add(child);
                        for(int i=0; i<parent.NumMetaData; i++)
                        {
                            parent.MetaData.Add(numbers[ptr++]);
                        }
                        res.Add(parent);
                        child = parent;
                    }
                    if (stack.Count > 0)
                    {
                        var parent = stack.Peek();
                        parent.NumChildren--;
                        parent.Children.Add(child);
                    }
                }
                else
                {
                    stack.Push(child);
                }
            }
            var sum = res.Sum(p => p.MetaData.Sum());

            Console.WriteLine(sum);
            Console.WriteLine(GetMetaSum(point));
            return sum;
        }

        private Dictionary<Node, int> dict = new Dictionary<Node, int>();
        private int GetMetaSum(Node root)
        {
            if (root == null)
                return 0;
            if (dict.ContainsKey(root))
            {
                return dict[root];
            }
            int sum = 0;
            if (root.Children.Count == 0)
            {
                sum = root.MetaData.Sum();
                dict.Add(root, sum);
                return sum;
            }
            
            foreach(var i in root.MetaData)
            {
                if (i > root.Children.Count)
                    continue;
                sum += GetMetaSum(root.Children[i-1]);
            }
            return sum;
        }
    }
}
