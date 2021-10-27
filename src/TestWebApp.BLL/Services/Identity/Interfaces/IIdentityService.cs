using TestWebApp.DAL.Models.Auth.Responses;
using TestWebApp.DAL.Models.Auth.Requests;
using CSharpFunctionalExtensions;
using System.Threading.Tasks;

namespace TestWebApp.BLL.Services.Identity.Interfaces
{
    public interface IIdentityService
    {
        Task<Maybe<AuthResponse>> RegisterAsync(RegisterRequest request);

        Task<Maybe<AuthResponse>> LoginAsync(LoginRequest request);

        Task<Maybe<AuthResponse>> RefreshTokenAsync(RefreshRequest request);
    }
}