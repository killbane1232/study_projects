using System;

namespace Observer1
{
    public class CurrentConditionDisplay:IDisplayElement,IObserver
    {
        WeatherData weatherData;
        float humidity;
        float temperature;
        float pressure;
        public CurrentConditionDisplay(WeatherData data)
        {
            weatherData=data;
        }
        public void Display()
        {
            Console.WriteLine($"humidity:{humidity}, temperature:{temperature}, pressure:{pressure}");
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
