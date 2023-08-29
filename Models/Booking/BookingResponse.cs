using System.Text.Json.Serialization;

namespace APITestFramework.Models.Booking
{
    public class BookingResponse
    {
        [JsonPropertyName("bookingid")]
        public int? BookingId { get; set; }

        [JsonPropertyName("booking")]
        public Booking? Booking { get; set; }
    }
}