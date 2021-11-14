
using System;
using System.Collections.Generic;
namespace Thinker
{
    public class TreeBuilder
    {
        public Node Root;
        Dictionary<char,int> Variables;
        List<int> Data;
        public TreeBuilder(string input)
        {
            Variables = new Dictionary<char, int>();
            List<Node> list = new List<Node>();
            foreach (var each in input)
                list.Add(new Node(each));
            List<Node> outPut = new List<Node>();
            Stack<Node> stack = new Stack<Node>();
            bool reverser=false;
            foreach (var each in list)
            {
                if(each.Data=='¬' || each.Data=='-')
                {
                    reverser=true;
                    continue;    
                }
                var prior = each.Priority;
                if(reverser && each.Data!='(' && prior>0)
                {
                    throw new Exception();
                }
                if (prior < 0)
                {
                    if(!Variables.ContainsKey((char)each.Data))
                        Variables.Add((char)each.Data, Variables.Count);
                    each.reverse=reverser;
                    reverser=false;
                    outPut.Add(each);
                    continue;
                }
                if (prior == 0)
                {
                    while (stack.Peek().Priority != 1)
                        outPut.Add(stack.Pop());
                    outPut[outPut.Count-1].reverse=stack.Pop().reverse;
                    continue;
                }
                if(prior==1)
                {
                    each.reverse=reverser;
                    reverser=false;
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
            foreach(var each in outPut)
            {
                if(each.reverse)
                    Console.Write($"-{each.Data} ");
                else
                    Console.Write($"{each.Data} ");
            }
            Root = new Node(' ');
            Root.Data = null;
            var traveler = Root;
            var i = outPut.Count - 1;
            while (i >= 0)
            {
                var each = outPut[i];
                while(traveler.Left!=null && traveler.Right!=null)
                    traveler = traveler.Parent;
                if (traveler.Data == null)
                {
                    traveler.reverse=each.reverse;
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
        public string TruthTable()
        {
            string result = "";
            Data = new List<int>();
            var len = Variables.Count;
            var header = new List<char>(len);
            for(int i =0;i<len;i++)
                header.Add(' ');
            foreach(var each in Variables)
                header[each.Value]=each.Key;
            for(int i =0;i<len;i++)
                result+=header[i]+" ";
            result+='\n';
            for(int i =0;i<len;i++)
                Data.Add(0);
            while(Data[0]!=2)
            {
                for(int i =0;i<len;i++)
                    result+=Data[i]+" ";
                var count = Root.Count(Variables, Data);
                if(count)
                    result+="1\n";
                else
                    result+="0\n";
                Data[len-1]++;
                for(int i=len-1;i>0;i--)
                    if(Data[i]==2)
                    {
                        Data[i]=0;
                        Data[i-1]++;
                    }
            }
            return result;
        }
    }
}