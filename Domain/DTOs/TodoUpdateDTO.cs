namespace Domain.DTOs;

public class TodoUpdateDTO
{

    public int Id { get; }
    public int? OwnerId { get; set; }
    public string? Title { get; set; }
    public bool? IsCompleted { get; set; }

    public TodoUpdateDTO(int id)
    {
        Id = id;
    }
}