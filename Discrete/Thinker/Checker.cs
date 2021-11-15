using System;
using System.Collections.Generic;

namespace Thinker
{
    public class Checker
    {
        
        public bool Check(List<List<int>> first, List<List<int>> seckond)
        {
            if(first.Count!=seckond.Count)
                return false;
            int cnt = 0;
            for(int i =0;i<first.Count;i++)
                cnt+=first[i][first[i].Count-1]-seckond[i][seckond[i].Count-1];
            if(cnt!=0)
                return false;
            return true;
        }
    }
}
