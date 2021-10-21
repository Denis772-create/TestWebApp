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
        Maybe<Task<AuthResponse>> RegisterAsync(RegisterRequest request);

        Maybe<Task<AuthResponse>> LoginAsync(LoginRequest request);

        Maybe<Task<AuthResponse>> RefreshTokenAsync(RefreshRequest request);
    }
}