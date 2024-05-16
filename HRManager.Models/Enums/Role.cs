using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace HRManager.Models.Enums
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Role
    {
        Admin = 0,
        HR = 1,
        Employee = 2
    }
}
