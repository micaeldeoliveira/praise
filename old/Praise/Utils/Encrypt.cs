using System.Security.Cryptography;
using System.Text;

namespace Praise.Utils
{
    public static class Encrypt
    {
        public static string Hash(string password)
        {
            if (string.IsNullOrEmpty(password)) return "";
            var data = MD5.Create().ComputeHash(Encoding.Default
                .GetBytes(password += "|9d341cca-f6c9-50c0-bb43-7e32989c2881"));
            var sbString = new StringBuilder();
            foreach (var t in data)
                sbString.Append(t.ToString("x2"));
            return sbString.ToString();
        }
    }
}
