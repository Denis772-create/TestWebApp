using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestWebApp.BLL.Services.Identity.Interfaces;
using TestWebApp.DAL.Data;
using TestWebApp.DAL.Models.Auth;

namespace TestWebApp.BLL.Services.Identity.Implement
{
    public class RefreshTokenService : IRefreshTokenService
    {
        private ApplicationDbContext _context;

        public RefreshTokenService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Result> CreateAsync(RefreshToken refreshToken)
        {
            try
            {
                await _context.RefreshTokens.AddAsync(refreshToken);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return Result.Failure(e.Message);
            }

            return Result.Success();
        }

        public async Task<Result> DeleteAsync(Guid id)
        {
            try
            {
                var refreshToken = _context.RefreshTokens.FirstOrDefault(t => t.Id == id);
                if (refreshToken is null)
                    return Result.Failure("Can't find corresponding Token to delete.");

                _context.RefreshTokens.Remove(refreshToken);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                return Result.Failure(e.Message);
            }
            return Result.Success();
        }

        public async Task<Maybe<RefreshToken>> GetByTokenAsync(string token)
        {
            var refreshToken = await Task.Run(() => _context.RefreshTokens.FirstOrDefault(t => t.Token == token));

            if (refreshToken is null)
                return Maybe<RefreshToken>.None;

            return refreshToken;
        }
    }
}