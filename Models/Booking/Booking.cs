using System.Text.Json.Serialization;

namespace APITestFramework.Models.Booking
{
    public class Booking
    {
        [JsonPropertyName("firstname")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? FirstName { get; set; }

        [JsonPropertyName("lastname")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? LastName { get; set; }

        [JsonPropertyName("totalprice")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public float? TotalPrice { get; set; }

        [JsonPropertyName("depositpaid")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? DepositPaid { get; set; }

        [JsonPropertyName("bookingdates")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public BookingDate? BookingDates { get; set; }

        [JsonPropertyName("additionalneeds")]
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? AdditionalNeeds { get; set; }
            
    }
}