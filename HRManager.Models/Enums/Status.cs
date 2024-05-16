using System.Text.Json.Serialization;

namespace HRManager.Models.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Status
    {
        Approved,
        Pending,
        Rejected
    }
}
