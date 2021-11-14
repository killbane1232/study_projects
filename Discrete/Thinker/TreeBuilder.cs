
using System;
using System.Collections.Generic;
namespace Thinker
{
    public class Node
    {
        public int Priority
        {
            get
            {
                switch (Data)
                {
                    case '&':
                    case '^':
                    case '*':
                    case '/': return 3;
                    case '|':
                    case 'v':
                    case '+':
                    case '→':
                    case '-': return 2;
                    case '(': return 1;
                    case ')': return 0;
                    default: return -1;
                }
            }
        }
        public char? Data;
        public Node Left;
        public Node Right;
        public Node Parent;
        public Node(Node parent)
        {
            Parent = parent;
        }
        public Node(char data)
        {
            Data = data;
        }
        public string Run()
        {
            string res = "";
            if (Left != null)
                res += "("+Left.Run();
            res += Data;
            if (Right != null)
                res += Right.Run()+")";
            return res;
        }
        public bool Check()
        {
            if(Left!=null)
                return (Priority!=-1&&Left.Check()&&Right.Check());
            else
                return (Priority==-1);
        }
    }
    public class TreeBuilder
    {
        public Node Root;
        int VariablesCount;
        public TreeBuilder(string input)
        {
            List<Node> list = new List<Node>();
            foreach (var each in input)
                list.Add(new Node(each));
            List<Node> outPut = new List<Node>();
            Stack<Node> stack = new Stack<Node>();
            foreach (var each in list)
            {
                var prior = each.Priority;
                if (prior < 0)
                {
                    outPut.Add(each);
                    continue;
                }
                if (prior == 0)
                {
                    while (stack.Peek().Priority != 1)
                        outPut.Add(stack.Pop());
                    stack.Pop();
                    continue;
                }
                if(prior==1)
                {
                    stack.Push(each);
                    continue;
                }
            l1:
                if (stack.Count <= 0 || stack.Peek().Priority < prior)
                {
                    stack.Push(each);
                    continue;
                }
                while (stack.Count > 0 && prior <= stack.Peek().Priority)
                    outPut.Add(stack.Pop());
                goto l1;
            }
            while (stack.Count > 0)
                outPut.Add(stack.Pop());
            foreach (var eac in outPut)
                Console.Write(eac.Data);
            Console.WriteLine();
            Root = new Node(' ');
            Root.Data = null;
            var traveler = Root;
            var i = outPut.Count - 1;
            while (i >= 0)
            {
            Console.WriteLine(Root.Run());
                var each = outPut[i];
                while(traveler.Left!=null && traveler.Right!=null)
                    traveler = traveler.Parent;
                if (traveler.Data == null)
                {
                    traveler.Data = each.Data;
                    i--;
                    if (each.Priority == -1)
                    {
                        traveler = traveler.Parent;
                        continue;
                    }
                    traveler.Right = new Node(traveler);
                    traveler = traveler.Right;
                }
                else
                {
                    if (traveler.Right != null)
                    {
                        traveler.Left = new Node(traveler);
                        traveler = traveler.Left;
                    }
                }
            }
        }
    }
}