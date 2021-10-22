using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWebApp.DAL.Models.Auth;
using CSharpFunctionalExtensions;

namespace TestWebApp.BLL.Services.Identity.Interfaces
{
    public interface IRefreshTokenService
    {
        Task<Maybe<RefreshToken>> GetByTokenAsync(string token);

        Task<Result> CreateAsync(RefreshToken refreshToken);

        Task<Result> DeleteAsync(Guid id);
    }
}