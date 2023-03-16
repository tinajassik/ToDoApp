namespace Domain.Exceptions;

public class TodoDoesNotExist : Exception
{
    public TodoDoesNotExist(int id) : base(
        String.Format("The given Todo id {0} does not correspond to any existing todos", id))
    {
    }
} 