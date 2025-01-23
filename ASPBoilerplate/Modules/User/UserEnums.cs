using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace ASPBoilerplate.Modules.User;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum USER_ROLES
{
    [EnumMember(Value="STAFF")]
    STAFF,
    [EnumMember(Value="ADMIN")]
    ADMIN,
    [EnumMember(Value ="SUPER_ADMIN")]
    SUPER_ADMIN,
    [EnumMember(Value ="INVALID")]
    INVALID
}
