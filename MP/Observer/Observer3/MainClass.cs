using System;

namespace Observer3
{
    public class MainClass
    {
        public static void Main(string[] args)
        {
            WeatherData data = new WeatherData(0, 0, 0);
            while (true)
            {
                var input = int.Parse(Console.ReadLine());
                switch (input)
                {
                    case 1:
                        data.RegisterObserver(new CurrentConditionDisplay(data));
                        break;
                    case 2:
                        data.RegisterObserver(new ForcastDisplay(data));
                        break;
                    case 3:
                        data.RegisterObserver(new StatisticsDisplay(data));
                        break;
                    case 4:
                        data.RegisterObserver(new ThirdPartyDisplay(data));
                        break;
                    case 5:
                        data.RegisterObserver(new NewDisplay(data));
                        break;
                    case 6:
                        data.RemoveObserver(data.observers.Find(x=>x is CurrentConditionDisplay));
                        break;
                    case 7:
                        data.RemoveObserver(data.observers.Find(x=>x is ForcastDisplay));
                        break;
                    case 8:
                        data.RemoveObserver(data.observers.Find(x=>x is StatisticsDisplay));
                        break;
                    case 9:
                        data.RemoveObserver(data.observers.Find(x=>x is ThirdPartyDisplay));
                        break;
                    case 10:
                        data.RemoveObserver(data.observers.Find(x=>x is NewDisplay));
                        break;
                    case 11:
                        data.Temperature = DateTime.Now.Second;
                        break;
                    case 12:
                        data.Pressure=DateTime.Now.Second*2+24;
                        break;
                    case 13:
                        data.Humidity=DateTime.Now.Second+33;
                        break;
                }
            }
        }
    }
}
