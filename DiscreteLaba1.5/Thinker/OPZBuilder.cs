using System.Linq;
using System;
using System.Collections.Generic;
namespace Thinker
{
    public class OPZBuilder
    {
        public Node Root;
        public List<char> Variables;
        public OPZBuilder(string input)
        {
            Variables = new List<char>();
            List<Node> list = new List<Node>();
            foreach (var each in input)
            {
                if(!Char.IsWhiteSpace(each))
                    list.Add(new Node(each));
            }   
            List<Node> outPut = new List<Node>();
            Stack<Node> stack = new Stack<Node>();
            bool reverser=false;
            foreach (var each in list)
            {
                if(each.Data=='¬' || each.Data=='-' || each.Data=='~')
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
                    if(!Variables.Contains((char)each.Data))
                        Variables.Add((char)each.Data);
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
            Variables.Sort();
        }
        public List<List<int>> TruthTable()
        {
            var Data = new List<int>();
            var len = Variables.Count;
            for(int i =0;i<len;i++)
                Data.Add(0);
            var table = new List<List<int>>();
            while(Data[0]!=2)
            {
                var str = new List<int>();
                for(int i =0;i<len;i++)
                {
                    str.Add(Data[i]);
                }
                var count = Root.Count(Variables, Data);
                str.Add((count?1:0));
                Data[len-1]++;
                for(int i=len-1;i>0;i--)
                    if(Data[i]==2)
                    {
                        Data[i]=0;
                        Data[i-1]++;
                    }
                table.Add(str);
            }
            return table;
        }
        public string BuildDNF()
        {
            var table = this.TruthTable();
            string res = "";
            for(int i =0;i<table.Count;i++)
            {
                if(table[i][table[i].Count-1]!=1)
                    continue;
                if(i!=0)
                    res+="v";
                res+="(";
                for(int j=0;j<table[i].Count-1;j++)
                {
                    if(j!=0)
                        res+="^";
                    if(table[i][j]==1)
                        res+=Variables[j];
                    else
                        res+=$"¬{Variables[j]}";
                }
                res+=")";
            }
            return res;
        }
        public string BuildKNF()
        {
            var table = this.TruthTable();
            string res = "";
            bool flag = false;
            for(int i =0;i<table.Count;i++)
            {
                if(table[i][table[i].Count-1]!=0)
                    continue;
                if(flag)
                    res+="^";
                flag = true;
                res+="(";
                for(int j=0;j<table[i].Count-1;j++)
                {
                    if(j!=0)
                        res+="v";
                    if(table[i][j]!=1)
                        res+=Variables[j];
                    else
                        res+=$"¬{Variables[j]}";
                }
                res+=")";
            }
            return res;
        }
        public static bool TruthTableCheck(OPZBuilder A, OPZBuilder B)
        {
            var union = new List<char>(A.Variables.Union(B.Variables));
            var Data = new List<int>();
            var len = union.Count;
            union.Sort();
            for(int i =0;i<len;i++)
                Data.Add(0);
            while(Data[0]!=2)
            {
                var str = new List<int>();
                for(int i =0;i<len;i++)
                {
                    str.Add(Data[i]);
                }
                var countA = A.Root.Count(A.Variables, Data);
                var countB = B.Root.Count(B.Variables, Data);
                if(countA!=countB)
                    return false;
                Data[len-1]++;
                for(int i=len-1;i>0;i--)
                    if(Data[i]==2)
                    {
                        Data[i]=0;
                        Data[i-1]++;
                    }
            }
            return true;
        }
    }
}