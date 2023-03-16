namespace Domain.Exceptions;

public class UnavailableUsernameException : Exception
{
    public UnavailableUsernameException()
    {
    }

    public UnavailableUsernameException(string name) : base(String.Format("Username already taken: {0}", name))
    {
    }
}