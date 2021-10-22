using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWebApp.BLL.Services.Identity.Interfaces;
using TestWebApp.DAL.Models.Auth;
using TestWebApp.DAL.Models.Auth.Request;
using TestWebApp.DAL.Models.Auth.Response;

namespace TestWebApp.BLL.Services.Identity.Implement
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly Authenticator _authenticator;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly TokenManager _tokenManager;

        public IdentityService(
            IRefreshTokenService refreshTokenService,
            Authenticator authenticator,
            UserManager<IdentityUser> userManager,
            TokenManager tokenManager)
        {
            _refreshTokenService = refreshTokenService;
            _authenticator = authenticator;
            _userManager = userManager;
            _tokenManager = tokenManager;
        }

        public async Task<Maybe<AuthResponse>> LoginAsync(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user != null)
                return new AuthResponse { Errors = new[] { "Login and Password don't correct." } };

            var userHasValidPassword = await _userManager.CheckPasswordAsync(user, request.Password);

            if (userHasValidPassword)
                return new AuthResponse { Errors = new[] { "Login and Password don't correct." } };

            return await _authenticator.Authenticate(user);
        }

        public async Task<Maybe<AuthResponse>> RegisterAsync(RegisterRequest request)
        {
            var existingUserByEmail = await _userManager.FindByEmailAsync(request.Email);

            if (existingUserByEmail != null)
                return new AuthResponse { Errors = new[] { "Login and Password don't correct." } };

            if (request.Password != request.ConfirmPassword)
                return new AuthResponse { Errors = new[] { "Login and Password don't correct." } };

            var user = new IdentityUser
            {
                Email = request.Email
            };

            var resultCreate = await _userManager.CreateAsync(user);

            if (!resultCreate.Succeeded)
                return Maybe<AuthResponse>.None;

            return await _authenticator.Authenticate(user);
        }

        public async Task<Maybe<AuthResponse>> RefreshTokenAsync(RefreshRequest request)
        {
            bool isValidRefreshToken = _tokenManager.ValidateRefreshToken(request.RefreshToken);

            if (!isValidRefreshToken)
                return new AuthResponse { Errors = new[] { "Refresh Token not found" } };

            var refreshToken = await _refreshTokenService.GetByTokenAsync(request.RefreshToken);

            if (refreshToken.HasValue)
                return new AuthResponse { Errors = new[] { "Refresh not found" } };

            await _refreshTokenService.DeleteAsync(refreshToken.Value.Id);

            var user = await _userManager.FindByIdAsync(refreshToken.Value.UserId);

            if (user == null)
                return new AuthResponse { Errors = new[] { "User not found" } };

            return await _authenticator.Authenticate(user);
        }
    }
}