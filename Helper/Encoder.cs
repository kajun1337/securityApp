using Microsoft.IdentityModel.Tokens;
using System.Buffers.Text;
namespace securityApp.Helper
{
    public class Encoder
    {
        public string EncodeUrl(string text)
        {
            string originalText = text;
            var encoded = Base64UrlEncoder.Encode(originalText);
            Console.WriteLine(encoded);
            var decoded = Base64UrlEncoder.Decode(encoded);
            Console.WriteLine(decoded);
            return encoded;
        }
        
    }
}
