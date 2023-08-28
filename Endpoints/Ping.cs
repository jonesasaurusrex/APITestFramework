using System.Net;

namespace APITestFramework.Endpoints
{
    /// <summary>
    /// Class for Ping endpoint, used to confirm whether the API is up and running
    /// </summary>
    public class Ping
    {
        public const string URL = "https://restful-booker.herokuapp.com/ping";
        private HttpClient _httpClient;
        public Ping() 
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(URL);
        }

        /// <summary>
        /// Calls the Get method on the Ping endpoint
        /// </summary>
        /// <returns>The resulting http status code</returns>
        public HttpStatusCode Get()
        {
            var response = _httpClient.GetAsync(_httpClient.BaseAddress).Result;
            return response.StatusCode;
        }
    }
}