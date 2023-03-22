using Domain.DTOs;
using Domain.Models;

namespace HttpClients.ClientInterfaces;

public interface ITodoService
{
    Task<Todo> CreateAsync(TodoCreationDto dto);
    Task<ICollection<Todo>> GetAsync(
        string? userName, 
        int? userId, 
        bool? completedStatus, 
        string? titleContains
    );

    Task UpdateAsync(TodoUpdateDTO dto);

    Task<TodoBasicDTO> GetByIdAsync(int id);

    Task DeleteAsync(int id);
}