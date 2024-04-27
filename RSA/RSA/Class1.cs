namespace RSA
{
    public class RSA
    {
        public static void Main()
        {
            List<long> result = new List<long>();
            result.Add(2);
            result.Add(3);
            result.Add(5);
            result.Add(7);
            for (long i = 9; i < int.MaxValue; i += 2) 
            {
                var flag = true;
                for(var j =0;j< result.Count; j++)
                {
                    if (i % result[j] == 0)
                    {
                        flag = false;
                        break;
                    }
                }
                if (flag)
                {
                    result.Add(i);
                }
            }
            using(StreamWriter writer = new StreamWriter("Result.txt"))
            {
                writer.Write("{" + result[0]);
                for (var i = 1; i < result.Count; i++)
                {
                    writer.Write("," + result[i]);
                }
                writer.Write("}");
            }
        }
    }
}