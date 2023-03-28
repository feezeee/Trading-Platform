using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Users.Domain.Entities;
using Users.Models.Options;
using Users.Models.Tokens;
using Users.Models.Users;

namespace Users.Application.Utils.TokenGenerator
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly AuthOptions _authOptions;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public TokenGenerator(IOptions<AuthOptions> authOptions)
        {
            _authOptions = authOptions.Value;
        }

        //public async Task<(string jwtToken, string refreshToken)> RefreshToken(Account account, RefreshToken oldRefreshToken, string ipAddress)
        //{

        //    var jwtToken = GenerateJwtToken(account);
        //    var newRefreshToken = GenerateRefreshToken(ipAddress);
        //    var result = (jwtToken: jwtToken, refreshToken: newRefreshToken.Token);
        //    newRefreshToken.Token = encryption.Encrypt(newRefreshToken.Token);

        //    oldRefreshToken.Revoked = newRefreshToken.Created;
        //    oldRefreshToken.RevokedByIp = newRefreshToken.CreatedByIp;
        //    oldRefreshToken.ReplacedByToken = newRefreshToken.Token;

        //    account.RefreshTokens.Add(newRefreshToken);
        //    await unitOfWork.Save();
        //    return result;
        //}

        public string GenerateJwtToken(UserEntity user)
        {
            var claims = GetClaims(user);
            var now = DateTime.UtcNow;

            var jwt = new JwtSecurityToken(
                claims: claims,
                expires: now.AddMinutes(_authOptions.Lifetime),
                signingCredentials: new SigningCredentials(_authOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
        //private RefreshToken GenerateRefreshToken(string ipAddress)
        //{
        //    var randomNumber = new byte[32];
        //    using (var rng = RandomNumberGenerator.Create())
        //    {
        //        rng.GetBytes(randomNumber);
        //        var refreshToken = Convert.ToBase64String(randomNumber);

        //        int LifeTimeDays;
        //        int.TryParse(configuration["Refreshtoken:LifeTimeDays"], out LifeTimeDays);
        //        return new RefreshToken
        //        {
        //            Token = refreshToken,
        //            Expires = DateTime.UtcNow.AddDays(LifeTimeDays),
        //            Created = DateTime.UtcNow,
        //            CreatedByIp = ipAddress
        //        };
        //    }
        //}



        private List<Claim> GetClaims(UserEntity user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.FirstName),
                new Claim(ClaimTypes.Surname, user.LastName),
                new Claim(ClaimTypes.NameIdentifier, user.Nickname),
            };

            claims.AddRange(user.Roles.Select(roleEntity => new Claim(ClaimTypes.Role, roleEntity.Name)));

            return claims;
        }
    }
}
