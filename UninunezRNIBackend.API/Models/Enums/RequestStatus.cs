using System.Text.Json.Serialization;

namespace UninunezRNIBackend.Models.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum RequestStatus
    {
        Pending,
        InReview,
        Approved,
        Rejected,
        Cancelled
    }
}
