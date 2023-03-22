using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Exceptions;
using Domain.Models;
using FileData.DAOs;

namespace Application.Logic;

public class TodoLogic : ITodoLogic
{

    private readonly ITodoDao todoDao;
    private readonly IUserDAO userDao;

    public TodoLogic(ITodoDao todoDao, IUserDAO userDao)
    {
        this.todoDao = todoDao;
        this.userDao = userDao;
    }

    public async Task<Todo> CreateAsync(TodoCreationDto todoCreationDto)
    {
        User? user = await userDao.GetByIdAsync(todoCreationDto.OwnerId);

        if (user == null)
            throw new Exception($"User with id {todoCreationDto.OwnerId} does not exist");

        ValidateTodo(todoCreationDto);

        Todo todo = new Todo(user, todoCreationDto.Title);
        Todo created = await todoDao.CreateAsync(todo);
        return created;
    }

    public Task<IEnumerable<Todo>> GetAsync(SearchTodoParametersDTO searchParameters)
    {
        return todoDao.GetAsync(searchParameters);
    }

    public async Task UpdateAsync(TodoUpdateDTO todoDto)
    {
        Todo? existingTodo = await todoDao.GetByIdAsync(todoDto.Id);

        if (existingTodo == null)
            throw new TodoDoesNotExist(todoDto.Id);

        User? existingUser = null;
        if (todoDto.OwnerId != null)
        {
            existingUser = await userDao.GetByIdAsync((int)todoDto.OwnerId);
            if (existingUser == null)
                throw new UserNotFound((int)todoDto.OwnerId);
        }

        User userToUse = existingUser ?? existingTodo.Owner;
        string titleToUse = todoDto.Title ?? existingTodo.Title;
        bool completedToUse = todoDto.IsCompleted ?? existingTodo.IsCompleted;

        Todo updated = new(userToUse, titleToUse)
        {
            IsCompleted = completedToUse,
            Id = existingTodo.Id,
        };
        
        ValidateTodo(updated);

        await todoDao.UpdateAsync(updated);
    }

    public async Task DeleteAsync(int id)
    {
        Todo? todo = await todoDao.GetByIdAsync(id);
        if (todo == null)
        {
            throw new TodoDoesNotExist(id);
        }

        if (!todo.IsCompleted)
        {
            throw new Exception("You cannot delete an uncompleted todo, sorry bro");
        }

        await todoDao.DeleteAsync(id);
        
    }

    public async Task<TodoBasicDTO> GetByIdAsync(int id)
    {
        Todo? todo = await todoDao.GetByIdAsync(id);
        if (todo == null)
        {
            throw new TodoDoesNotExist(id);
        }

        return new TodoBasicDTO(todo.Id, todo.Owner.UserName, todo.Title, todo.IsCompleted);
    }

    private void ValidateTodo(TodoCreationDto dto)
    {
        if (string.IsNullOrEmpty(dto.Title)) throw new Exception("title cannot be empty sorry");
        if (dto.Title.Length is < 4 or > 20) throw new Exception("title length is invalid sorry!");
    }

    private void ValidateTodo(Todo todo)
    {
        if (string.IsNullOrEmpty(todo.Title)) throw new Exception("title cannot be empty sorry");
        if (todo.Title.Length is < 4 or > 20) throw new Exception("title length is invalid sorry!");
    }
}