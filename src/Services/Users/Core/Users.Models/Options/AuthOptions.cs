using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Users.Models.Options
{
    public class AuthOptions
    {
        public string Key { get; set; } = string.Empty;
        public double Lifetime { get; set; }
        

        public SymmetricSecurityKey GetSymmetricSecurityKey() => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Key));
    }
}
