using CSharpFunctionalExtensions;
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
        public Maybe<Task<AuthResponse>> LoginAsync(LoginRequest request)
        {
            throw new NotImplementedException();
        }

        public Maybe<Task<AuthResponse>> RefreshTokenAsync(RefreshRequest request)
        {
            throw new NotImplementedException();
        }

        public Maybe<Task<AuthResponse>> RegisterAsync(RegisterRequest request)
        {
            throw new NotImplementedException();
        }
    }
}