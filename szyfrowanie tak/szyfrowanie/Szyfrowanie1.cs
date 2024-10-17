using System;
using System.Security.Cryptography;
using System.Text;

public class Szyfrowanie1 : Szyfrowanie
{
    public string Hash(string input)
    {
        using (var md5 = MD5.Create())
        {
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
            return Convert.ToBase64String(hash);
        }
    }
}
