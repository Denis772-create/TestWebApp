using CSharpFunctionalExtensions;
using System.Threading.Tasks;
using TestWebApp.DAL.Models;
using System;

namespace TestWebApp.BLL.Services.Identity.Interfaces
{
    public interface IRefreshTokenService
    {
        Task<Maybe<RefreshToken>> GetByTokenAsync(string token);

        Task<Result> CreateAsync(RefreshToken refreshToken);

        Task<Result> DeleteAsync(Guid id);

        Task<Result> DeleteAll(string userId);
    }
}