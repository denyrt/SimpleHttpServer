using System;

namespace Core.Models
{
    public class WeatherInfo
    {
        public double Fahrenheit { get; set; }
        public int Celsius { get; set; }
        public WeatherState? Weather { get; set; }
        public DateTime Date { get; set; }

        public static WeatherInfo[] CreateRandWeathers()
        {
            var weathersInfo = new WeatherInfo[4];

            for (int i = 0; i < weathersInfo.Length; ++i)
            {                
                var rnd = new Random();
                var celsius = rnd.Next(-10, 11);
                weathersInfo[i] = new WeatherInfo
                {
                    Celsius = celsius,
                    Fahrenheit = celsius * 9d / 5d + 32d,
                    Weather = Enum.GetValues(typeof(WeatherState)).GetValue(rnd.Next(0, 4)) as WeatherState?,
                    Date = DateTime.Today.AddDays(i)
                };
            }

            return weathersInfo;
        }
    }

    public enum WeatherState
    {
        Rain = 0,
        Sun = 1,
        Cloudy = 2,
        Snowy = 3
    }
}