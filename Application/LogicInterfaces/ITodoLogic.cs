using Domain.DTOs;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface ITodoLogic
{
    Task<Todo> CreateAsync(TodoCreationDto todoCreationDto);
    Task<IEnumerable<Todo>> GetAsync(SearchTodoParametersDTO searchParameters);
    Task UpdateAsync(TodoUpdateDTO todoDto);
    Task DeleteAsync(int id);
    Task<TodoBasicDTO> GetByIdAsync(int id);

}