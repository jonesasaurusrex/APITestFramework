using APITestFramework.Models.Auth;
using System.Net.Http.Headers;
using System.Text.Json;

namespace APITestFramework.Endpoints
{
    /// <summary>
    /// Class for Auth endpoint, which is used for access to the PUT and DELETE methods for booking endpoint
    /// </summary>
    public class AuthEndpoint
    {
        public const string URL = "https://restful-booker.herokuapp.com/auth";
        public const string DEFAULT_USERNAME = "admin";
        public const string DEFAULT_PASSWORD = "password123";

        private HttpClient _httpClient;

        public AuthEndpoint()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri(URL);
        }

        /// <summary>
        /// Calls the Post method on the Auth endpoint
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        public HttpResponseMessage Post(AuthPostPayload payload)
        {
            string jsonString = JsonSerializer.Serialize(payload);
            HttpContent content = new StringContent(jsonString);
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return _httpClient.PostAsync(_httpClient.BaseAddress, content).Result;
        }

        /// <summary>
        /// Calls the Post method on the Auth endpoint and returns the resulting token
        /// </summary>
        /// <returns></returns>
        public AuthToken CreateAuthToken()
        {
            AuthPostPayload payload = new AuthPostPayload
            {
                Username = DEFAULT_USERNAME,
                Password = DEFAULT_PASSWORD
            };
            var response = Post(payload);
            string content = response.Content.ReadAsStringAsync().Result;
            try
            {
                return JsonSerializer.Deserialize<AuthToken>(content);
            }
            catch
            {
                return new AuthToken();
            }
        }
    }
}