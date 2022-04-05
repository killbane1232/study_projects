using System;
using System.Collections.Generic;

namespace Observer3
{
    public class WeatherData:ISubject
    {
        float humidity;
        float temperature;
        float pressure;

        public float Humidity
        {
            get=>GetHumidity();
            set
            {
                humidity=value;
                MeasureChanged();
            }
        }
        public float Temperature
        {
            get=>GetTemperature();
            set
            {
                temperature=value;
                MeasureChanged();
            }
        }
        public float Pressure
        {
            get=>GetPressure();
            set
            {
                pressure=value;
                MeasureChanged();
            }
        }
        public List<IObserver> observers;
        private delegate void observersHandler();
        observersHandler Handler;
        public WeatherData(float temperature, float pressure, float humidity)
        {
            observers=new List<IObserver>();
            Temperature=temperature;
            Pressure=pressure;
            Humidity=humidity;
        }
        public void RegisterObserver(IObserver observer)
        {
            observers.Add(observer);
            if(observers.Count==1)
                Handler=observer.Update;
            else
                Handler+=observer.Update;
        }
        public void RemoveObserver(IObserver observer)
        {
            observers.Remove(observer);
            if(observers.Count==1)
                Handler=null;
            else
                Handler-=observer.Update;
        }
        public void NotifyObservers()
        {
            if(Handler!=null)
                Handler();
        }

        public float GetTemperature()
        {
            return temperature;
        }

        public float GetHumidity()
        {
            return humidity;
        }

        public float GetPressure()
        {
            return pressure;
        }

        void MeasureChanged()
        {
            //ChangeSomeData

            NotifyObservers();
        }
    }
}
