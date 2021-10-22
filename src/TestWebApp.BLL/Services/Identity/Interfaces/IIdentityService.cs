using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using TestWebApp.DAL.Models.Auth;
using TestWebApp.DAL.Models.Auth.Request;
using TestWebApp.DAL.Models.Auth.Response;

namespace TestWebApp.BLL.Services.Identity.Interfaces
{
    public interface IIdentityService
    {
        Task<Maybe<AuthResponse>> RegisterAsync(RegisterRequest request);

        Task<Maybe<AuthResponse>> LoginAsync(LoginRequest request);

        Task<Maybe<AuthResponse>> RefreshTokenAsync(RefreshRequest request);
    }
}