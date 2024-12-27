using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace ASPBoilerplate.Modules.File;


[JsonConverter(typeof(JsonStringEnumConverter))]
public enum FILE_STORAGE_TYPES
{
    [EnumMember(Value="LOCAL")]
    LOCAL,
    [EnumMember(Value="FIREBASE")]
    FIREBASE
};

