using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace pl.edu.wat.wcy.pz.restaurant_client_application.Controllers
{
    public abstract class PasswordHash
    {
        public static string GenerateSHA512String(string inputString)
        {
            SHA512 sha512 = SHA512.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(inputString);
            byte[] hash = sha512.ComputeHash(bytes);
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }
            return result.ToString();
        }
    }
}
