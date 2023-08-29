using APITestFramework.Endpoints;
using APITestFramework.Models.Auth;
using APITestFramework.Models.Booking;
using System.Net;
using System.Text.Json;

namespace APITestFramework.Tests
{
    [TestFixture, Order(3)]
    public class BookingTests
    {
        private int _id;
        private AuthEndpoint _authEndpoint;
        private AuthToken _token;
        private BookingEndpoint _bookingEndpoint;
        private Booking _testBooking;
        private BookingDate _bookingDate;

        [SetUp]
        public void Setup()
        {
            _authEndpoint = new AuthEndpoint();
            _token = _authEndpoint.CreateAuthToken();
            _bookingEndpoint = new BookingEndpoint(_token);
            _bookingDate = new BookingDate
            {
                CheckIn = "2022-01-01",//new DateTime(2022, 01, 01).ToString("yyyy-MM-dd"),
                CheckOut = "2022-01-08"//new DateTime(2022, 02, 01).ToString("yyyy-MM-dd")
            };
            _testBooking = new Booking
            {
                FirstName = "Micheal",
                LastName = "McDoesntExist",
                TotalPrice = 1137,
                DepositPaid = true,
                BookingDates = _bookingDate,
                AdditionalNeeds = "None"
            };
        }

        [Test, Order(1)]
        [Author("Travis Schultz")]
        [Description("Executes the Post method on the BookingEndpoint and saves the id for future tests")]
        [Category("SmokeTest")]
        [Category("Booking")]
        public void BookingPostShouldCreateNewBooking()
        {
            //Arrange

            //Act
            var results = _bookingEndpoint.Post(_testBooking);
            var resultingBooking = JsonSerializer.Deserialize<BookingResponse>(results.Content);
            if(resultingBooking.BookingId == null)
            {
                _id = -1;
                Assert.Fail("ERROR: Booking ID not found. All subsequent tests are invalid");
            }else
                _id = (int)resultingBooking.BookingId;

            //Assert
            Assert.That(results.StatusCode, Is.EqualTo(HttpStatusCode.OK), "ERROR: HttpStatusCode mismatch");
            Assert.That(resultingBooking.Booking.FirstName, Is.EqualTo(_testBooking.FirstName), "ERROR: First name mismatch");
            Assert.That(resultingBooking.Booking.LastName, Is.EqualTo(_testBooking.LastName), "ERROR: Last name mismatch");
            Assert.That(resultingBooking.Booking.AdditionalNeeds, Is.EqualTo(_testBooking.AdditionalNeeds), "ERROR: Additional needs mismatch");
            Assert.That(resultingBooking.Booking.DepositPaid, Is.EqualTo(_testBooking.DepositPaid), "ERROR: Deposit paid mismatch");
            Assert.That(resultingBooking.Booking.TotalPrice, Is.EqualTo(_testBooking.TotalPrice), "ERROR: Total price mismatch");
        }

        [Test, Order(2)]
        [Author("Travis Schultz")]
        [Description("Executes the Get method on the Booking endpoint with the id from the prior test to check the data")]
        [Category("SmokeTest")]
        [Category("Booking")]
        public void BookingGetShouldRetrieveDataAboutNewlyCreatedBooking()
        {
            //Arrange

            //Act
            var results = _bookingEndpoint.Get(_id);
            var resultingBooking = JsonSerializer.Deserialize<Booking>(results.Content);

            //Assert
            Assert.That(results.StatusCode, Is.EqualTo(HttpStatusCode.OK), "ERROR: HttpStatusCode mismatch");
            Assert.That(resultingBooking.FirstName, Is.EqualTo(_testBooking.FirstName), "ERROR: First name mismatch");
            Assert.That(resultingBooking.LastName, Is.EqualTo(_testBooking.LastName), "ERROR: Last name mismatch");
            Assert.That(resultingBooking.AdditionalNeeds, Is.EqualTo(_testBooking.AdditionalNeeds), "ERROR: Additional needs mismatch");
            Assert.That(resultingBooking.DepositPaid, Is.EqualTo(_testBooking.DepositPaid), "ERROR: Deposit paid mismatch");
            Assert.That(resultingBooking.TotalPrice, Is.EqualTo(_testBooking.TotalPrice), "ERROR: Total price mismatch");
        }

        [Test, Order(3)]
        [Author("Travis Schultz")]
        [Description("Executes the Get method on the Booking endpoint to check that id from the prior test is present")]
        [Category("SmokeTest")]
        [Category("Booking")]
        public void BookingGetShouldIncludeIdForNewlyCreatedBooking()
        {
            //Arrange

            //Act
            var results = _bookingEndpoint.Get();
            var resultingBookings = JsonSerializer.Deserialize<List<BookingResponse>>(results.Content);
            var targetBooking = resultingBookings.Find(b => b.BookingId == _id);

            //Assert
            Assert.That(results.StatusCode, Is.EqualTo(HttpStatusCode.OK), "ERROR: HttpStatusCode mismatch");
            Assert.That(targetBooking.BookingId, Is.EqualTo(_id), "ERROR: target not found");
        }

        [Test, Order(4)]
        [Author("Travis Schultz")]
        [Description("Executes the Put method on the Booking endpoint with the id from the prior test to update the data")]
        [Category("SmokeTest")]
        [Category("Booking")]
        public void BookingPutShouldUpdateDataAboutNewlyCreatedBooking()
        {
            //Arrange
            Booking updatedBooking = new Booking 
                {
                FirstName = "Micheal",
                LastName = "McDoesntExist",
                TotalPrice = 1137,
                DepositPaid = true,
                BookingDates = _bookingDate,
                AdditionalNeeds = "Breakfast"
            };

            //Act
            var results = _bookingEndpoint.Put(_id, updatedBooking);
            var resultingBooking = JsonSerializer.Deserialize<Booking>(results.Content);

            //Assert
            Assert.That(results.StatusCode, Is.EqualTo(HttpStatusCode.OK), "ERROR: HttpStatusCode mismatch");
            Assert.That(resultingBooking.FirstName, Is.EqualTo(updatedBooking.FirstName), "ERROR: First name mismatch");
            Assert.That(resultingBooking.LastName, Is.EqualTo(updatedBooking.LastName), "ERROR: Last name mismatch");
            Assert.That(resultingBooking.AdditionalNeeds, Is.EqualTo(updatedBooking.AdditionalNeeds), "ERROR: Additional needs mismatch");
            Assert.That(resultingBooking.AdditionalNeeds, Is.Not.EqualTo(_testBooking.AdditionalNeeds), "ERROR: Additional needs not changed");
            Assert.That(resultingBooking.DepositPaid, Is.EqualTo(updatedBooking.DepositPaid), "ERROR: Deposit paid mismatch");
            Assert.That(resultingBooking.TotalPrice, Is.EqualTo(updatedBooking.TotalPrice), "ERROR: Total price mismatch");
        }

        [Test, Order(5)]
        [Author("Travis Schultz")]
        [Description("Executes the Patch method on the Booking endpoint with the id from the prior test to update the data")]
        [Category("SmokeTest")]
        [Category("Booking")]
        public void BookingPatchShouldUpdateDataAboutNewlyCreatedBooking()
        {
            //Arrange
            Booking updatedPartialBooking = new Booking
            {
                AdditionalNeeds = "Breakfast and Dinner"
            };

            //Act
            var results = _bookingEndpoint.Patch(_id, updatedPartialBooking);
            var resultingBooking = JsonSerializer.Deserialize<Booking>(results.Content);

            //Assert
            Assert.That(results.StatusCode, Is.EqualTo(HttpStatusCode.OK), "ERROR: HttpStatusCode mismatch");
            Assert.That(resultingBooking.AdditionalNeeds, Is.EqualTo(updatedPartialBooking.AdditionalNeeds), "ERROR: Additional needs mismatch");
            Assert.That(resultingBooking.AdditionalNeeds, Is.Not.EqualTo(_testBooking.AdditionalNeeds), "ERROR: Additional needs not changed");
            Assert.That(resultingBooking.FirstName, Is.EqualTo(_testBooking.FirstName), "ERROR: First name mismatch");
            Assert.That(resultingBooking.LastName, Is.EqualTo(_testBooking.LastName), "ERROR: Last name mismatch");
            Assert.That(resultingBooking.DepositPaid, Is.EqualTo(_testBooking.DepositPaid), "ERROR: Deposit paid mismatch");
            Assert.That(resultingBooking.TotalPrice, Is.EqualTo(_testBooking.TotalPrice), "ERROR: Total price mismatch");
        }

        [Test, Order(6)]
        [Author("Travis Schultz")]
        [Description("Executes the Delete method on the booking endpoint with the id from the prior test and then calls get to check that it's gone")]
        [Category("SmokeTest")]
        [Category("Booking")]
        public void BookingDeleteShouldDeleteNewlyCreatedBooking()
        {
            //Arrange

            //Act
            var deletionResults = _bookingEndpoint.Delete(_id);
            var getResults = _bookingEndpoint.Get(_id);

            //Assert
            Assert.That(deletionResults.StatusCode, Is.EqualTo(HttpStatusCode.Created), "ERROR: HttpStatusCode mismatch");
            Assert.That(getResults.StatusCode, Is.EqualTo(HttpStatusCode.NotFound), "ERROR: Booking still present");
        }
    }
}