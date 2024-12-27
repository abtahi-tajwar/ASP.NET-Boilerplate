using System.Text;

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


}