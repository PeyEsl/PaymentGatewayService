using System.Security.Cryptography;
using System.Text;

namespace PaymentGatewayService.Tools
{
    public static class EncryptionHelper
    {
        public static string EncryptTripleDES(string plainText, string key)
        {
            using (var tripleDes = TripleDES.Create())
            {
                tripleDes.Key = Convert.FromBase64String(key);
                tripleDes.Mode = CipherMode.ECB;
                tripleDes.Padding = PaddingMode.PKCS7;

                var plainBytes = Encoding.UTF8.GetBytes(plainText);
                var encryptor = tripleDes.CreateEncryptor();
                var encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);

                return Convert.ToBase64String(encryptedBytes);
            }
        }
    }
}
