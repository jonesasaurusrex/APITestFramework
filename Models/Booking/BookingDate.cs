using System.Text.Json.Serialization;

namespace APITestFramework.Models.Booking
{
    public class BookingDate
    {
        [JsonPropertyName("checkin")]
        public string CheckIn { get; set; }

        [JsonPropertyName("checkout")]
        public string CheckOut { get; set; }
    }
}