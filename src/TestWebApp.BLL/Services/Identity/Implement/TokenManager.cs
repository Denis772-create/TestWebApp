using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using TestWebApp.Common.Helpers.Authentication;
using Microsoft.AspNetCore.Identity;

namespace TestWebApp.BLL.Services.Identity.Implement
{
    public class TokenManager
    {
        private readonly JwtAuth _jwtAuth;

        public TokenManager(JwtAuth jwtAuth)
        {
            this._jwtAuth = jwtAuth;
        }

        public string GenerateToken(string secretKey, string issuer, string audience, double expiration, IEnumerable<Claim> claims = null)
        {
            SecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            SigningCredentials signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer,
                audience,
                claims,
                DateTime.UtcNow,
                DateTime.UtcNow.AddMinutes(expiration),
                signingCredentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public string GenerateRefreshToken()
        {
            return GenerateToken(
                _jwtAuth.RefreshTokenSecret,
                _jwtAuth.Issuer,
                _jwtAuth.Audience,
                _jwtAuth.RefreshTokenExpirationMinutes);
        }

        public bool ValidateRefreshToken(string refreshToken)
        {
            TokenValidationParameters validationParameters = new TokenValidationParameters()
            {
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtAuth.RefreshTokenSecret)),
                ValidIssuer = _jwtAuth.Issuer,
                ValidAudience = _jwtAuth.Audience,
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true
            };

            try
            {
                new JwtSecurityTokenHandler().ValidateToken(refreshToken, validationParameters, out SecurityToken sT);

                return true;
            }
            catch
            {
                return false;
            }
        }

        public string GenerateToken(IdentityUser user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, "User")
            };

            return GenerateToken(
                _jwtAuth.AccessTokenSecret,
                _jwtAuth.Issuer,
                _jwtAuth.Audience,
                _jwtAuth.ExpirationMinutes,
                claims);
        }
    }
}