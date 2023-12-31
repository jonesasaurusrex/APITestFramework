using System.Net;
using APITestFramework.Endpoints;

namespace APITestFramework.Tests
{
    [TestFixture, Order(1)]
    public class PingTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test, Order(1)]
        [Author("Travis Schultz")]
        [Description("Executes the Get method on the Ping endpoint and validates the return status")]
        [Category("SmokeTest")]
        [Category("Ping")]
        public void PingGetShouldReturnStatus201Created()
        {
            //Arrange

            //Act
            PingEndpoint pingEndpoint = new PingEndpoint();

            //Assert
            Assert.That(pingEndpoint.Get(), Is.EqualTo(HttpStatusCode.Created));
        }
    }
}