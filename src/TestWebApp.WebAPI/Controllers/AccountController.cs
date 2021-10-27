using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestWebApp.BLL.Services.Identity.Implement;
using TestWebApp.DAL.Models.Auth.Request;
using TestWebApp.DAL.Models.Auth.Response;
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
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest request)
        {
            if (ModelState.IsValid)
                return BadRequest("Model is not valid");
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
            return code == null ? BadRequest() : Ok(code);
        }

        [HttpPost(ApiRoutes.Account.ResetPassword)]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest resetRequest)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var user = await _userManager.FindByEmailAsync(resetRequest.Email);
            if (user == null)
                return BadRequest();

            var result = await _userManager.ResetPasswordAsync(user, resetRequest.Code, resetRequest.Password);
            if (!result.Succeeded)
                return BadRequest();

            return Ok();
        }

        [HttpPost(ApiRoutes.Account.ChangePassword)]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest changePassword)
        {
            if (ModelState.IsValid)
            {
                User user = await _userManager.FindByEmailAsync(changePassword.Email);
                if (user == null)
                    return NotFound();

                if (user != null)
                {
                    IdentityResult result =
                        await _userManager.ChangePasswordAsync(user, changePassword.OldPassword, changePassword.NewPassword);
                    if (result.Succeeded)
                        return Ok();
                    else
                        return Ok(new AuthResponse { Errors = result.Errors.Select(x => x.Description) });
                }
            }
            return Ok(new AuthResponse { Errors = new List<string> { "User not found or not valid." } }); ;
        }
    }
}