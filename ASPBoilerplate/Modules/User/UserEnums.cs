using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace ASPBoilerplate.Modules.User;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum USER_ROLES
{
    [EnumMember(Value="MEMBER")]
    MEMBER,
    [EnumMember(Value="ADMIN")]
    ADMIN,
    [EnumMember(Value ="SUPER_ADMIN")]
    SUPER_ADMIN
}
