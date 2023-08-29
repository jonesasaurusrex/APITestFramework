using APITestFramework.Endpoints;
using APITestFramework.Models.Auth;
using System.Net;
using System.Text.Json;

namespace APITestFramework.Tests
{
    [TestFixture, Order(2)]
    public class AuthTests
    {
        [Test, Order(1)]
        [Author("Travis Schultz")]
        [Description("Executes the Post method on the Auth endpoint and validates the return status and that data is returned")]
        [Category("SmokeTest")]
        [Category("Auth")]
        public void AuthPostShouldReturnNewAuthTokenForGoodCredentials()
        {
            //Arrange
            AuthPostPayload authPostPayload = new AuthPostPayload
            {
                Username = "admin",
                Password = "password123"
            };
            AuthEndpoint auth = new AuthEndpoint();

            //Act
            var results = auth.Post(authPostPayload);
            var resultString = results.Content.ReadAsStreamAsync().Result;
            Assert.IsNotNull(resultString);
            AuthToken token = JsonSerializer.DeserializeAsync<AuthToken>(resultString).Result;

            //Assert
            Assert.That(results.StatusCode, Is.EqualTo(HttpStatusCode.OK), "ERROR: Status Code mismatch");
            Assert.That(string.IsNullOrEmpty(token.Token), Is.False, "ERROR: Token is empty string");
        }
    }
}