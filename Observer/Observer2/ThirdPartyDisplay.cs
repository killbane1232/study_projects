using System;

namespace Observer2
{
    public class ThirdPartyDisplay:IDisplayElement,IObserver
    {
        WeatherData weatherData;
        float humidity;
        float temperature;
        float pressure;
        public ThirdPartyDisplay(WeatherData data)
        {
            weatherData=data;
        }
        public void Display()
        {
            Console.WriteLine("ThirdparyDisplay");
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
