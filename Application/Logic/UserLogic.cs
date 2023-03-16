using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Exceptions;
using Domain.Models;

namespace Application.Logic;

public class UserLogic : IUserLogic
{

    private readonly IUserDAO userDAO;

    public UserLogic(IUserDAO userDao)
    {
        userDAO = userDao; 
    }

    public async Task<User> CreateAsync(UserCreationDTO userToCreate)
    {
        User? existing = await userDAO.GetByUserName(userToCreate.UserName);

        if (existing != null)
            throw new UnavailableUsernameException(userToCreate.UserName);

        ValidateData(userToCreate);

        User toCreate = new User
        {
            UserName = userToCreate.UserName
        };

        User created = await userDAO.CreateAsync(toCreate);
        return created; 
    }

    public Task<IEnumerable<User>> GetAsync(SearchUserParametersDTO searchUserParametersDto)
    {
        return userDAO.GetAsync(searchUserParametersDto);
    }

    private static void ValidateData(UserCreationDTO userToCreate)
    {
        string username = userToCreate.UserName;

        if (username.Length < 3)
            throw new InvalidUsernameLengthShort(username.Length.ToString());
        if (username.Length > 15)
            throw new InvalidUsernameLengthLong(username.Length.ToString());
    }

    public Task<User?> GetByUserNameAsync(string userName)
    {
        throw new NotImplementedException();
    }
}