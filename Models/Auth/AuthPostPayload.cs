using System.Text.Json.Serialization;

namespace APITestFramework.Models.Auth
{
    public class AuthPostPayload
    {
        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}