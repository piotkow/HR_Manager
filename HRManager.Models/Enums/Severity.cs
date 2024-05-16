using System.Text.Json.Serialization;

namespace HRManager.Models.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Severity
    {
        Cosmetic,
        Minor,
        Moderate,
        Major,
        Critical
    }
}
