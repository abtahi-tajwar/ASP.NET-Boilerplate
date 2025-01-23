using System.Text;
using System.Text.RegularExpressions;

namespace ASPBoilerplate.Utils;

public class Helpers
{
    public static string GenerateRandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        Random random = new Random();
        StringBuilder result = new StringBuilder(length);

        for (int i = 0; i < length; i++)
        {
            result.Append(chars[random.Next(chars.Length)]);
        }

        return result.ToString();
    }

    public static void UpdateEntityWithDto<T, TDto>(T entity, TDto dto)
    {
        var entityType = typeof(T);
        var dtoType = typeof(TDto);

        foreach (var dtoProperty in dtoType.GetProperties())
        {
            var entityProperty = entityType.GetProperty(dtoProperty.Name);

            // Skip if the property doesn't exist in the entity or if the DTO property is null
            if (entityProperty != null && dtoProperty.CanRead)
            {
                var value = dtoProperty.GetValue(dto);

                if (value != null)
                {
                    entityProperty.SetValue(entity, value);
                }
            }
        }
    }

    public static T ParseEnum<T>(string Role)
    {
        if (Role == null) throw new ArgumentException($"Invalid role: {Role}");
        string UpperCaseRole = Role.ToUpper();
        if (Enum.TryParse(typeof(T), Role, true, out var result))
        {
            return (T)result;
        }
        else
        {
            throw new ArgumentException($"Invalid role: {Role}");
        }
    }

    public static string ConvertToSnakCase(string? input)
    {
        if (string.IsNullOrWhiteSpace(input))
        {
            return string.Empty;
        }

        // Replace spaces and special characters with underscores
        input = Regex.Replace(input, @"[^\w]", "_"); // Replace non-word characters with '_'

        // Insert underscores before uppercase letters (e.g., "CamelCase" -> "Camel_Case")
        input = Regex.Replace(input, @"(?<!^)([A-Z])", "_$1");

        // Convert to lowercase
        return input.ToLower();
    }


}