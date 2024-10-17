using BCrypt.Net;

namespace szyfrowanie_tak.szyfrowanie
{
    public class Szyfrowanie4 : Szyfrowanie
    {
        public string Hash(string input)
        {
            return BCrypt.Net.BCrypt.HashPassword(input);
        }
    }
}
