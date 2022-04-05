using System;

namespace Observer1
{
    public class StatisticsDisplay:IDisplayElement,IObserver
    {
        WeatherData weatherData;
        float humidity;
        float temperature;
        float pressure;
        public StatisticsDisplay(WeatherData data)
        {
            weatherData=data;
        }
        public void Display()
        {
            float min, max, avg;
            if(humidity<temperature && humidity<pressure)
                min=humidity;
            else
            if(pressure<temperature && humidity>pressure)
                min=pressure;
            else
                min=temperature;
            if(humidity>temperature && humidity>pressure)
                max=humidity;
            else
            if(pressure>temperature && humidity<pressure)
                max=pressure;
            else
                max=temperature;
            avg=(humidity+temperature+pressure)/3;
            Console.WriteLine($"min:{min}, max:{max}, average:{avg}");
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
