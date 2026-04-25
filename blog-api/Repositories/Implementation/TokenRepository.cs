using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using blog_api.Repositories.Inteface;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace blog_api.Repositories.Implementation
{
    public class TokenRepository : ITokenRepository
    {

        private readonly IConfiguration configuration;
        public TokenRepository(IConfiguration configuration) {

            this.configuration = configuration;
        }

        public string CreateJwtToken(IdentityUser user, List<string> roles)
        {
            // create clames
            var claims = new List<Claim> {
           new Claim(ClaimTypes.Email, user.Email)
           };

            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            // jwt sequirty Token Parameter
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));

            var credentials= new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: configuration["Jwt:Issuer"],
                audience: configuration["Jwt:Audience"],
                claims: claims,

                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials

                );


            //Return token

            return new JwtSecurityTokenHandler().WriteToken(token);

        }
    }
}
