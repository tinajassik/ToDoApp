using System.Net.Http.Json;
using System.Text.Json;
using Domain.DTOs;
using Domain.Models;
using HttpClients.ClientInterfaces;

namespace HttpClients.Implementations;

public class TodoHttpClient : ITodoService
{
    private readonly HttpClient client;

    public TodoHttpClient(HttpClient client)
    {
        this.client = client;
    }

    public async Task<Todo> CreateAsync(TodoCreationDto dto)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("/todos", dto);
        string result = await response.Content.ReadAsStringAsync();
        
        if (!response.IsSuccessStatusCode) {
            throw new Exception(result);
        }
        
        Todo todo = JsonSerializer.Deserialize<Todo>(result, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                })!;
        return todo;
    }
}