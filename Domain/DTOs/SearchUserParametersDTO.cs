namespace Domain.DTOs;

public class SearchUserParametersDTO
{
    public string? UsernameContains { get; }

    public SearchUserParametersDTO(string? usernameContains)
    {
        UsernameContains = usernameContains;
    }
}