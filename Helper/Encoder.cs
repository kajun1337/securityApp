using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Buffers.Text;
using System.Runtime.Intrinsics.Arm;
namespace securityApp.Helper
{
    public class Encoder
    {
        
        public string EncodeUrlToBase64(string text)
        {
            var encoded = Base64UrlEncoder.Encode(text);
            return encoded;
        }
        public string EncodeFileToSHA265(IFormFile file)
        {
            using (var stream = file.OpenReadStream())
            {
                using (var sha256 = SHA256.Create())
                {
                    byte[] hashBytes = sha256.ComputeHash(stream);
                    return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
                }
            }
        }
    }
}
