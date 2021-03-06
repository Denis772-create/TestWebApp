using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ardalis.Filters;
using TestWebApp.BLL.Services.Identity.Implement;
using TestWebApp.DAL.Models.Auth.Request;
using TestWebApp.DAL.Models.Auth.Responses;
using TestWebApp.DAL.Models.Entities;
using TestWebApp.WebAPI.Contracts.V1;

namespace TestWebApp.WebAPI.Controllers
{
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class AccountController : ControllerBase
    {
        private UserManager<User> _userManager;

        public AccountController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost(ApiRoutes.Account.ForgotPassword)]
        [ValidateModel]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            //todo: if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))

            var code = await _userManager.GeneratePasswordResetTokenAsync(user);

            var callbackUrl = Url.Action(
                action: "ResetPassword",
                controller: "Account",
                values: new { code = code },
                protocol: HttpContext.Request.Scheme);

            var emailService = new EmailService();
            await emailService.SendEmailAsync(request.Email, "Reset Password",
                $"To reset your password follow the <a href='{callbackUrl}'>link</a>.");

            return Ok();
        }

        [HttpGet(ApiRoutes.Account.ResetPassword)]
        [AllowAnonymous]
        public IActionResult ResetPassword(string code = null)
        {
            return code == null ? (IActionResult)BadRequest() : Ok(code);
        }

        [HttpPost(ApiRoutes.Account.ResetPassword)]
        [ValidateModel]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest resetRequest)
        {
            var user = await _userManager.FindByEmailAsync(resetRequest.Email);
            if (user == null)
                return BadRequest();

            var result = await _userManager.ResetPasswordAsync(user, resetRequest.Code, resetRequest.Password);
            if (!result.Succeeded)
                return BadRequest();

            return Ok();
        }

        [HttpPost(ApiRoutes.Account.ChangePassword)]
        [ValidateModel]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest changePassword)
        {
            User user = await _userManager.FindByEmailAsync(changePassword.Email);
            if (user == null)
                return NotFound();

            IdentityResult result =
                await _userManager.ChangePasswordAsync(user, changePassword.OldPassword, changePassword.NewPassword);
            if (result.Succeeded)
                return Ok();
            else
                return Ok(new AuthResponse { Errors = result.Errors.Select(x => x.Description) });
        }

        [HttpPost(ApiRoutes.Account.ConfirmEmail)]
        [ValidateModel]
        public async Task<IActionResult> ConfirmEmail(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            var callbackUrl = Url.Action(
            "ConfirmEmail",
            "Account",
            new
            {
                userId = user.Id,
                code = code
            },
                protocol: HttpContext.Request.Scheme);

            await new EmailService()
                .SendEmailAsync(email,
                "Confirm your account",
                $"Confirm your registration by following the <a href='{callbackUrl}'>link</a>.");

            return Ok();
        }

        [HttpGet(ApiRoutes.Account.ConfirmEmail)]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
                return BadRequest();

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
                return BadRequest();

            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (!result.Succeeded)
                return BadRequest();

            return Ok();
        }
    }
}