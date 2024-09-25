using Core.Model;

namespace Service.Interface;

public interface IUserService
{
    Task<User?> GetUserByIdAsync(string userId);
}