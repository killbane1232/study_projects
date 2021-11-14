using System;

namespace Thinker
{
    public class Checker
    {
        
        public bool Check(string input)
        {
            while(input.Contains(' '))
                input = input.Remove(input.IndexOf(' '));
            
            return true;
        }
    }
}
