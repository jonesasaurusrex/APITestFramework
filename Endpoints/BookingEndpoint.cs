using APITestFramework.Models.Auth;
using APITestFramework.Models.Booking;
using RestSharp;

namespace APITestFramework.Endpoints
{
    /// <summary>
    /// Class for Booking endpoint, used to create, edit, and delete bookings
    /// </summary>
    public class BookingEndpoint
    {
        public const string URL = "https://restful-booker.herokuapp.com/booking";
        private string _token;

        private RestClient _client;
        private RestClientOptions _options;

        public BookingEndpoint(AuthToken token)
        {
            _token = token.Token;
        }

        /// <summary>
        /// Calls the Post method on the Booking endpoint to create a new booking
        /// </summary>
        /// <param name="booking"></param>
        /// <returns></returns>
        //public HttpResponseMessage Post(Booking booking)
        public RestResponse Post(Booking booking)
        {
            _options = new RestClientOptions(URL);
            _client = new RestClient(_options);
            var request = new RestRequest
            {
                Method = Method.Post
            };
            request.AddJsonBody(booking);
            request.AddHeader("Accept", "*/*");
            request.AddHeader("Content-Type", "application/json");
            return _client.PostAsync(request).Result;
        }

        /// <summary>
        /// Calls the Get method on the Booking endpoint to retrieve all booking ids as a list of booking JSON objects with only the booking id field
        /// </summary>
        /// <returns></returns>
        public RestResponse Get()
        {
            _options = new RestClientOptions(URL);
            _client = new RestClient(_options);
            var request = new RestRequest
            {
                Method = Method.Get
            };
            return _client.GetAsync(request).Result;
        }

        /// <summary>
        /// Calls the get method on the Booking endpoint with a specified id and retrieves all info for that booking
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RestResponse Get(int id)
        {
            _options = new RestClientOptions(URL + "/" + id);
            _client = new RestClient(_options);
            var request = new RestRequest
            {
                Method = Method.Get
            };
            request.AddHeader("Accept", "*/*");
            request.AddHeader("Content-Type", "application/json");
            return _client.GetAsync(request).Result;
        }

        /// <summary>
        /// Updates a current booking
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RestResponse Put(int id, Booking booking)
        {
            _options = new RestClientOptions(URL + "/" + id);
            _client = new RestClient(_options);
            var request = new RestRequest
            {
                Method = Method.Put
            };
            request.AddJsonBody(booking);
            request.AddHeader("Accept", "*/*");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Cookie", "token=" + _token);
            return _client.PutAsync(request).Result;
        }

        /// <summary>
        /// Deletes a booking
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public RestResponse Delete(int id)
        {
            _options = new RestClientOptions(URL + "/" + id);
            _client = new RestClient(_options);
            var request = new RestRequest
            {
                Method = Method.Delete
            };
            request.AddHeader("Accept", "*/*");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Cookie", "token=" + _token);
            return _client.DeleteAsync(request).Result;
        }

        /// <summary>
        /// Updates a booking with a partial payload
        /// </summary>
        /// <param name="id"></param>
        /// <param name="booking"></param>
        /// <returns></returns>
        public RestResponse Patch(int id, Booking booking)
        {
            _options = new RestClientOptions(URL + "/" + id);
            _client = new RestClient(_options);
            var request = new RestRequest
            {
                Method = Method.Patch
            };
            request.AddJsonBody(booking);
            request.AddHeader("Accept", "*/*");
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Cookie", "token=" + _token);
            return _client.PatchAsync(request).Result;
        }
    }
}