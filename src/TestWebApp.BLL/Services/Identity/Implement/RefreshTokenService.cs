using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWebApp.BLL.Services.Identity.Interfaces;
using TestWebApp.DAL.Models.Auth;

namespace TestWebApp.BLL.Services.Identity.Implement
{
    public class RefreshTokenService : IRefreshTokenService
    {
        public Maybe<Task> Create(RefreshToken refreshToken)
        {
            throw new NotImplementedException();
        }

        public Maybe<Task> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Maybe<Task> DeleteAll(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Maybe<Task<RefreshToken>> GetByToken(string token)
        {
            throw new NotImplementedException();
        }
    }
}