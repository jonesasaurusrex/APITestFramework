using System.Net;
using APITestFramework.Endpoints;

namespace APITestFramework.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void PingShouldReturnStatus201Created()
        {
            Ping pingEndpoint = new Ping();
            Assert.That(pingEndpoint.Get(), Is.EqualTo(HttpStatusCode.Created));
        }
    }
}