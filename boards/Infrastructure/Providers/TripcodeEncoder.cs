using System.Security.Cryptography;
using System.Text;
using boards.Domain.Providers;

namespace boards.Infrastructure.Providers;

public sealed class TripcodeEncoder : ITripcodeEncoder
{
    public string? Encode(string tripcode)
    {
        var parts = tripcode.Split("#");
        if (parts.Length != 2)
        {
            return null;
        }

        var username = parts[0];
        var password = parts[1];

        var hashed = HashPassword(password);

        return $"{username}!{hashed}";
    }

    static string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] hashBytes = sha256.ComputeHash(passwordBytes);
            return Convert.ToBase64String(hashBytes);
        }
    }
}