using ChatBot.Common.ViewModels;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace UserManagementService.Services.Auth
{
    public class AuthService : IAuthService
    {
        public readonly IConfiguration _configuration;
        public AuthService(IConfiguration configuration) {
            
            this._configuration = configuration;
        
        }

        public void PopulateJwtTokenAsync(AuthenticationModel user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(this._configuration.GetSection("Secret").Value);
            //var key = Encoding.ASCII.GetBytes("!@#$%^&*()!@#$%^&*()");
            string minuetes = this._configuration.GetSection("TokenSettings:SessionExpiryInMinuetes").Value;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
               {
                 new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Email, user.Email.ToString()),
                //new Claim(ClaimTypes.Name, user.FirstName.ToString()),
                //new Claim("InterviewerId", user.InterviewerId.ToString()),
                //new Claim("CandidateId", user.CandidateId.ToString()),
                // new Claim("UserId", user.em.ToString()),
                // new Claim("IsAdmin", user.IsAdmin.ToString(),ClaimValueTypes.Boolean),

                 }),
                Expires = user.TokenExpiryDate = DateTime.UtcNow.AddMinutes(Convert.ToUInt64(minuetes)),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key)
       , SecurityAlgorithms.HmacSha256Signature),
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

             user.Token = tokenHandler.WriteToken(token);
        }
    }
}
