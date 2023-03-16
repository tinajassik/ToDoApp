using Domain.DTOs;
using Domain.Models;

namespace FileData.DAOs;

public interface ITodoDao
{
    Task<Todo> CreateAsync(Todo todo);
    Task<IEnumerable<Todo>> GetAsync(SearchTodoParametersDTO searchParameters);
    Task UpdateAsync(Todo todoNew);
    Task<Todo?> GetByIdAsync(int id);
    Task DeleteAsync(int id);
}