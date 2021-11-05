using System;

namespace Observer3
{
    public class ForcastDisplay:IDisplayElement,IObserver
    {
        WeatherData weatherData;
        float humidity;
        float temperature;
        float pressure;
        public ForcastDisplay(WeatherData data)
        {
            weatherData=data;
        }
        public void Display()
        {
            Console.WriteLine($"Weather must be... because pressure:{pressure}");
        }

        public void Update()
        {
            pressure = weatherData.GetPressure();
            humidity = weatherData.GetHumidity();
            temperature = weatherData.GetTemperature();
        
            Display();
        }
    }
}
