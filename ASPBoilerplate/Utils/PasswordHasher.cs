using System;
using System.Security.Cryptography;
using System.Text;

namespace ASPBoilerplate.Utils;

public class PasswordHasher
{
    public static string HashPassword(string password)
    {
        if (string.IsNullOrEmpty(password))
        {
            throw new ArgumentException("Password cannot be null or empty", nameof(password));
        }

        using (SHA256 sha256 = SHA256.Create())
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] hashBytes = sha256.ComputeHash(passwordBytes);

            return Convert.ToBase64String(hashBytes);
        }
    }

    // Constant-time comparison to prevent timing attacks
    public static bool AreHashesEqual(string password, string hash2)
    {
        string hash1 = HashPassword(password);
        
        var hashBytes1 = Convert.FromBase64String(hash1);
        var hashBytes2 = Convert.FromBase64String(hash2);

        if (hashBytes1.Length != hashBytes2.Length)
        {
            return false;
        }

        var result = true;
        for (int i = 0; i < hashBytes1.Length; i++)
        {
            result &= (hashBytes1[i] == hashBytes2[i]);
        }

        return result;
    }

}
