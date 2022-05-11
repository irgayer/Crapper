using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Crapper
{
    public class JwtOptions
    {
        public const string Jwt = "Jwt";

        public string Key { get; set; }
        public string Audience { get; set; }
        public string Issuer { get; set; }
    }
}
