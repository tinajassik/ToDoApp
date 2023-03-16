using Domain.DTOs;
using Domain.Models;

namespace Application.DaoInterfaces;

public interface IUserDAO
{
    Task<User> CreateAsync(User user);
    Task<IEnumerable<User>> GetAsync(SearchUserParametersDTO searchUserParametersDto);
    Task<User?> GetByUserName(string userName);
    Task<User?> GetByIdAsync(int ownerId);
}