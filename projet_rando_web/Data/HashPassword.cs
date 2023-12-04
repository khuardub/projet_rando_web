using System.Security.Cryptography;
using System.Text;

namespace projet_rando_web.Data
{
    public class HashPassword
    {
        public static string HasherPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();
                foreach (byte b in hashedBytes)
                {
                    // Conversion du byte en hexadecimal
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }

        }
    }
}
