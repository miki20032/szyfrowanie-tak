using System;
using System.Security.Cryptography;
using System.Text;

public class Szyfrowanie3 : Szyfrowanie
{
    public string Hash(string input)
    {
        using (var sha256 = SHA256.Create())
        {
            var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));
            return Convert.ToBase64String(hash);
        }
    }
}
