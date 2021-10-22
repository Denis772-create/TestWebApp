using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWebApp.BLL.Services.Identity.Interfaces;
using TestWebApp.DAL.Models.Auth;
using TestWebApp.DAL.Models.Auth.Response;

namespace TestWebApp.BLL.Services.Identity.Implement
{
    public class Authenticator
    {
        private readonly TokenManager _tokenManager;

        private readonly IRefreshTokenService _refreshTokenService;

        public Authenticator(TokenManager tokenManager, IRefreshTokenService refreshTokenService)
        {
            _tokenManager = tokenManager;
            _refreshTokenService = refreshTokenService;
        }

        public async Task<AuthResponse> Authenticate(IdentityUser user)
        {
            var accessToken = _tokenManager.GenerateToken(user);
            var refreshToken = _tokenManager.GenerateRefreshToken();

            await _refreshTokenService.CreateAsync(
                new RefreshToken
                {
                    Token = refreshToken,
                    UserId = user.Id
                });

            return new AuthResponse
            {
                Success = true,
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
        }
    }
}