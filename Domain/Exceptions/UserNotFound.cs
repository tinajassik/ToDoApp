namespace Domain.Exceptions;

public class UserNotFound : Exception
{
    public UserNotFound(int id) : base(String.Format("User with id {0} not found", id))
    {
    }
}