using Core.Model;
using Data.Data;
using Microsoft.EntityFrameworkCore;
using Service.Interface;

namespace Service.Service;

public class UserService(ReadDbContext readDbContext) : IUserService
{
    public async Task<User?> GetUserByIdAsync(string userId)
    {
        return await readDbContext.AppUser.FirstOrDefaultAsync(u => u.Id == userId);
    }
}