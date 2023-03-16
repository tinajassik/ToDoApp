
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;
[ApiController]
[Route("[controller]")]

public class TodosController : ControllerBase
{
    private readonly ITodoLogic todoLogic;

    public TodosController(ITodoLogic todoLogic)
    {
        this.todoLogic = todoLogic;
    }

    [HttpPost]
    public async Task<ActionResult<Todo>> CreateAsync(TodoCreationDto todoCreationDto)
    {
        try
        {
            Todo newTodo = await todoLogic.CreateAsync(todoCreationDto);
            return Created($"Todo {newTodo.Id} created! congrats bro", newTodo); 

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(400, e.Message);
        }
    }

    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Todo>>> GetAsync([FromQuery] string? username,
        [FromQuery] int? ownerId, [FromQuery] bool? completedStatus, [FromQuery] string? titleContains)
    {
        try
        {
            SearchTodoParametersDTO parametersDto =
                new SearchTodoParametersDTO(username, ownerId, completedStatus, titleContains);
            var todos = await todoLogic.GetAsync(parametersDto);
            return Ok(todos);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpPatch]
    public async Task<ActionResult> UpdateAsync([FromBody] TodoUpdateDTO dto)
    {
        try
        {
            await todoLogic.UpdateAsync(dto);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteAsync([FromRoute] int id)
    {
        try
        {
            await todoLogic.DeleteAsync(id);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Todo>> GetById([FromRoute] int id)
    {
        try
        {
            Todo result = await todoLogic.GetByIdAsync(id);
            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }



}