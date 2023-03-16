using Application.DaoInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace FileData.DAOs;

public class UserFileDao : IUserDAO
{
    private readonly FileContext context;

    public UserFileDao(FileContext context)
    {
        this.context = context; 
    }

    public Task<User> CreateAsync(User user)
    {
        int userId = 1;
        if (context.Users.Any())
        {
            userId = context.Users.Max(u => u.Id);
            userId++;
        }

        user.Id = userId;

        context.Users.Add(user);
        context.SaveChanges();

        return Task.FromResult(user);
    }

    public Task<IEnumerable<User>> GetAsync(SearchUserParametersDTO searchUserParametersDto)
    {
        IEnumerable<User> users = context.Users.AsEnumerable();

        if (searchUserParametersDto.UsernameContains != null)
        {
            users = context.Users.Where(u =>
                u.UserName.Contains(searchUserParametersDto.UsernameContains, StringComparison.OrdinalIgnoreCase));
        }

        return Task.FromResult(users);
    }

    public Task<User?> GetByUserName(string userName)
    {
        User? existing = context.Users.FirstOrDefault(u =>
            u.UserName.Equals(userName, StringComparison.OrdinalIgnoreCase)
        );
        return Task.FromResult(existing);
    }

    public Task<User?> GetByIdAsync(int ownerId)
    {
        User? existing = context.Users.FirstOrDefault(u => u.Id == ownerId);
        return Task.FromResult(existing);
        
    }
}