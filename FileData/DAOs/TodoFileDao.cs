using System.Globalization;
using Domain.DTOs;
using Domain.Exceptions;
using Domain.Models;

namespace FileData.DAOs;

public class TodoFileDao : ITodoDao
{

    private readonly FileContext context;

    public TodoFileDao(FileContext context)
    {
        this.context = context;
    }

    public Task<Todo> CreateAsync(Todo todo)
    {

        int todoId = 1;

        if (context.Todos.Any())
        {
            todoId = context.Todos.Max(t => t.Id);
            todoId++;
        }

        todo.Id = todoId;
        context.Todos.Add(todo);
        context.SaveChanges();

        return Task.FromResult(todo);
       
    }

    public Task<IEnumerable<Todo>> GetAsync(SearchTodoParametersDTO searchParameters)
    {
        IEnumerable<Todo> result = context.Todos.AsEnumerable();

        if (!string.IsNullOrEmpty(searchParameters.Username))
        {
            result = context.Todos.Where(t =>
                t.Owner.UserName.Equals(searchParameters.Username, StringComparison.OrdinalIgnoreCase));
           
        }

        if (searchParameters.UserId != null)
        {
            result = result.Where(t => t.Owner.Id == searchParameters.UserId);
            
        }

        if (searchParameters.CompletedStatus != null)
        {
            result = result.Where(t => t.IsCompleted == searchParameters.CompletedStatus);
           
        }

        if (!string.IsNullOrEmpty(searchParameters.TitleContains))
        {
            result = result.Where(t =>
                t.Title.Contains(searchParameters.TitleContains, StringComparison.OrdinalIgnoreCase));
           
        }

        return Task.FromResult(result);
    }

    public Task UpdateAsync(Todo todoNew)
    {
        Todo? existing = context.Todos.FirstOrDefault(t => t.Id == todoNew.Id);

        if (existing == null)
            throw new TodoDoesNotExist(todoNew.Id);

        context.Todos.Remove(existing);
        context.Todos.Add(todoNew);
        
        context.SaveChanges();
        
        return Task.CompletedTask;

    }

    public Task<Todo?> GetByIdAsync(int id)
    {
        Todo? existing = context.Todos.FirstOrDefault(t => t.Id == id);
        return Task.FromResult(existing);
    }

    public Task DeleteAsync(int id)
    {
        Todo? todoToBeDeleted = context.Todos.FirstOrDefault(t => t.Id == id);

        if (todoToBeDeleted == null)
        {
            throw new TodoDoesNotExist(id);
        }

        context.Todos.Remove(todoToBeDeleted);
        context.SaveChanges();
        return Task.CompletedTask;
    }
}