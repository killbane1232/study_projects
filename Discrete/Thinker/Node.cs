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
                    case '→': return 2;
                    case '(': return 1;
                    case ')': return 0;
                    default: return -1;
                }
            }
        }
        public char? Data;
        public bool reverse = false;
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
            string res;
            if (Left != null){
                res = $"({Left.Run()}{Data}{Right.Run()})";
                if(reverse)
                    res="¬"+res;
            }
            else
            {
                if(reverse)
                    res = $"¬{Data}";
                else
                    res = $"{Data}";
            }
            return res;
        }
        public bool Check()
        {
            if(Left!=null)
                return (Priority!=-1&&Left.Check()&&Right.Check());
            else
                return (Priority==-1);
        }
        public bool Count(Dictionary<char,int> vars, List<int> data)
        {
            switch(Data)
            {
                case '^': return (Left.Count(vars,data)&&Right.Count(vars,data))^reverse;
                case 'v': return (Left.Count(vars,data)||Right.Count(vars,data))^reverse;
                case '→': return ((!Left.Count(vars,data))||Right.Count(vars,data))^reverse;
                default: return (data[vars[(char)Data]]==1^reverse);
            }
        }
    }
}
