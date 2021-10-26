using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TestWebApp.BLL.Services.Identity.Interfaces;
using TestWebApp.DAL.Models.Auth.Request;
using TestWebApp.DAL.Models.Auth.Response;
using TestWebApp.WebAPI.Contracts.V1;

namespace TestWebApp.WebAPI.Controllers
{
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IIdentityService _identityService;
        private readonly IRefreshTokenService _refreshTokenService;

        public AuthController(IIdentityService identityService, IRefreshTokenService refreshTokenService)
        {
            _identityService = identityService;
            _refreshTokenService = refreshTokenService;
        }

        [HttpPost(ApiRoutes.Auth.Login)]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new AuthResponse { Errors = ModelState.Values.SelectMany(x => x.Errors.Select(y => y.ErrorMessage)) });

            var resultLogin = await _identityService.LoginAsync(request);
            var response = resultLogin.Value;

            if (!response.Success)
                return BadRequest(new AuthResponse { Errors = response.Errors });

            return Ok(response);
        }

        [HttpPost(ApiRoutes.Auth.Register)]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new AuthResponse { Errors = ModelState.Values.SelectMany(x => x.Errors.Select(y => y.ErrorMessage)) });

            var resultLogin = await _identityService.RegisterAsync(request);
            var response = resultLogin.Value;

            if (!response.Success)
                return BadRequest(new AuthResponse { Errors = response.Errors });

            return Ok();
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpDelete(ApiRoutes.Auth.Logout)]
        public async Task<IActionResult> Logout()
        {
            var userId = HttpContext.User.FindFirstValue("id");

            if (userId == null)
                return Unauthorized();

            await _refreshTokenService.DeleteAll(userId);

            return NoContent();
        }

        [HttpPost(ApiRoutes.Auth.Refresh)]
        public async Task<IActionResult> Refresh([FromBody] RefreshRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(new AuthResponse { Errors = ModelState.Values.SelectMany(x => x.Errors.Select(y => y.ErrorMessage)) });

            var resultLogin = await _identityService.RefreshTokenAsync(request);
            var response = resultLogin.Value;

            if (!response.Success)
                return BadRequest(new AuthResponse { Errors = response.Errors });

            return Ok(response);
        }
    }
}