using System.Linq;
using System;
using System.Collections.Generic;
namespace Thinker
{
    public class OPZBuilder
    {
        public Node Root;
        public List<char> Variables;
        public bool TryBuild(string input)
        {
            Variables = new List<char>();
            var list = (from each in input where !char.IsWhiteSpace(each) select new Node(each)).ToList();
            var outPut = new List<Node>();
            var stack = new Stack<Node>();
            var reverser = false;
            foreach (var each in list)
            {
                if (each.Data == '¬' || each.Data == '-' || each.Data == '~')
                {
                    reverser = true;
                    continue;
                }
                var prior = each.Priority;
                if (reverser && each.Data != '(' && prior > 0)
                {
                    return false;
                }
                switch (prior)
                {
                    case < 0:
                        {
                            if (!Variables.Contains((char)each.Data))
                                Variables.Add((char)each.Data);
                            each.reverse = reverser;
                            reverser = false;
                            outPut.Add(each);
                            continue;
                        }
                    case 0:
                        {
                            outPut.Add(stack.Pop());
                            if (stack.Count == 0 || stack.Peek().Data != '(')
                                return false;
                            outPut[^1].reverse = stack.Pop().reverse;
                            //if (stack.Count > 0 && stack.Peek().Data == '(')
                                //return false;
                            continue;
                        }
                    case 1:
                        each.reverse = reverser;
                        reverser = false;
                        if(each.Data != '(')
                            if (stack.Count == 0 || stack.Peek().Data != '(')
                                return false;
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
            Root = new Node(' ') { Data = null };
            var traveler = Root;
            var i = outPut.Count - 1;
            while (i >= 0)
            {
                var each = outPut[i];
                while (traveler.Left != null && traveler.Right != null)
                    traveler = traveler.Parent;
                if (traveler.Data == null)
                {
                    traveler.reverse = each.reverse;
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
            return true;
        }
        public List<List<int>> TruthTable()
        {
            var data = new List<int>();
            var len = Variables.Count;
            for(var i =0;i<len;i++)
                data.Add(0);
            var table = new List<List<int>>();
            while(data[0]!=2)
            {
                var str = new List<int>();
                for(var i =0;i<len;i++)
                {
                    str.Add(data[i]);
                }
                var count = Root.Count(Variables, data);
                str.Add(((bool)count?1:0));
                data[len-1]++;
                for(var i=len-1;i>0;i--)
                    if(data[i]==2)
                    {
                        data[i]=0;
                        data[i-1]++;
                    }
                table.Add(str);
            }
            return table;
        }
        public string BuildDNF()
        {
            var table = TruthTable();
            var flag = false;
            var res = "";
            var blocksCnt = 0;
            var cnt = table.Sum(x => x[^1]);
            if (cnt != 0)
                res = "(";
            else
                return res;
            for (var i = 0; i < table.Count; i++)
            {
                if (table[i][^1] == 0)
                    continue;
                if (flag)
                {
                    blocksCnt++;
                    if(blocksCnt>1)
                        cnt--;
                    res += "v";
                }
                flag = true;
                for (var j = 0; j < cnt - 2; j++)
                    res += "(";
                if (blocksCnt != 0 && table[i].Count != 2)
                    res += "(";
                for (var j = 0; j < table[i].Count - 1; j++)
                {
                    if (j == 0)
                    {
                        if (table[i][j] == 1)
                            res += $"{Variables[j]}";
                        else
                            res += $"¬{Variables[j]}";
                        continue;
                    }
                    if (table[i][j] == 1)
                        res += $"^{Variables[j]})";
                    else
                        res += $"^¬{Variables[j]})";
                }
            }

            for (int i = 0; i < blocksCnt; i++)
            {
                res += ")";
            }
            return res;
        }
        public string BuildKNF()
        {
            var table = TruthTable();
            var flag = false;
            var res = "";
            var blocksCnt = 0;
            var cnt = table.Sum(x => x[^1]-1)*-1;
            if (cnt != 0)
                res = "(";
            else
                return res;
            for (var i = 0; i < table.Count; i++)
            {
                if (table[i][^1]==1)
                    continue;
                if (flag)
                {
                    blocksCnt++;
                    if (blocksCnt > 1)
                        cnt--;
                    res += "^";
                }
                flag = true;
                for (var j = 0; j < cnt - 2; j++)
                    res += "(";
                if (blocksCnt != 0 && table[i].Count!=2)
                    res += "(";
                for (var j=0;j<table[i].Count-1;j++)
                {
                    if(j==0)
                    {
                        if (table[i][j] == 0)
                            res += $"{Variables[j]}";
                        else
                            res += $"¬{Variables[j]}";
                        continue;
                    }
                    if (table[i][j] == 0)
                        res += $"v{Variables[j]})";
                    else
                        res += $"v¬{Variables[j]})";
                }
            }

            for (int i = 0; i < blocksCnt; i++)
            {
                res += ")";
            }
            return res;
        }
        public static bool TruthTableCheck(OPZBuilder A, OPZBuilder B)
        {
            var union = new List<char>(A.Variables.Union(B.Variables));
            var data = new List<int>();
            var len = union.Count;
            union.Sort();
            for(var i =0;i<len;i++)
                data.Add(0);
            while(data[0]!=2)
            {
                var countA = A.Root.Count(union, data);
                var countB = B.Root.Count(union, data);
                if(countA!=countB && countA != null && countB!=null)
                    return false;
                data[len-1]++;
                for(var i=len-1;i>0;i--)
                    if(data[i]==2)
                    {
                        data[i]=0;
                        data[i-1]++;
                    }
            }
            return true;
        }
    }
}