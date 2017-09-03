using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using _04_SlowService.Model;

namespace _04_SlowService
{
    public class WeatherController : ApiController
    {
        public WeatherModel GetWeather()
        {
            Thread.Sleep(2000);
            return new WeatherModel() 
            {
                Temperature = 21.5, 
                AdditionalInformation = "Sunny" 
            };
        }
    }
}
