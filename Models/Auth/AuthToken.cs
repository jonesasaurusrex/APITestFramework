using System.Text.Json.Serialization;

namespace APITestFramework.Models.Auth
{
    public class AuthToken
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }
    }
}