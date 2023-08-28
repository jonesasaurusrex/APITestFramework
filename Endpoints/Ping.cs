using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace APITestFramework.Endpoints
{
    public class Ping
    {
        public const string URL = "https://restful-booker.herokuapp.com/ping";
        private HttpClient _httpClient;
        public Ping() 
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(URL);
        }

        public HttpStatusCode Get()
        {
            var response = _httpClient.GetAsync(_httpClient.BaseAddress).Result;
            return response.StatusCode;
        }
    }
}
