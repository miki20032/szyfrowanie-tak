using System;
using System.Security.Cryptography;
using System.Text;

public class Szyfrowanie2 : Szyfrowanie
{
    public string Hash(string input)
    {
        using (var sha1 = SHA1.Create())
        {
            var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
            return Convert.ToBase64String(hash);
        }
    }
}
