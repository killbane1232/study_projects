using System;

namespace Observer2
{
    public class NewDisplay:IDisplayElement,IObserver
    {
        WeatherData weatherData;
        float humidity;
        float temperature;
        float pressure;
        public NewDisplay(WeatherData data)
        {
            weatherData=data;
        }
        public void Display()
        {
            var HI = -42.379 + 2.04901523*temperature + 10.14333127*humidity-0.22475541*temperature*humidity-
                    0.00683783*temperature*temperature-0.05481717*humidity*humidity+
                    0.00122874*temperature*temperature*humidity+0.00085282*temperature*humidity*humidity-
                    0.00000199*temperature*temperature*humidity*humidity;
            Console.WriteLine($"Heating Index:{HI}");
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
