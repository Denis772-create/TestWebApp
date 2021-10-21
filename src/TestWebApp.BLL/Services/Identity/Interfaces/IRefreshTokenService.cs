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
        Maybe<Task<RefreshToken>> GetByToken(string token);

        Maybe<Task> Create(RefreshToken refreshToken);

        Maybe<Task> Delete(Guid id);

        Maybe<Task> DeleteAll(Guid userId);
    }
}