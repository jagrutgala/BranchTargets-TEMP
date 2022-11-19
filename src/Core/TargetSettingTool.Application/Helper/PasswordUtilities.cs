using System.Text;
using System.Security.Cryptography;

namespace TargetSettingTool.Application.Helper
{
    public static class PasswordUtilities
    {
        public static string GeneratePassword(int passLength)
        {
            var chars = "abcdefghijklmnopqrstuvwxyz@#$&ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            var result = new string(
                Enumerable.Repeat(chars, passLength)
                          .Select(s => s[random.Next(s.Length)])
                          .ToArray());
            return result;
        }
        public static string HashPassword(string password)
        {
            byte[] passhash = new SHA256Managed().ComputeHash(Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(passhash).Replace("-", string.Empty);
        }
    }
}
