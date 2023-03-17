using Domain.DTOs;
using Microsoft.AspNetCore.Components;

namespace BlazorServerApp.Pages
{
    public partial class CreateUser : ComponentBase
    {
        private string username = "";
        private string resultMsg = "";
        private string color = "";
    
        private async Task Create()
        {
            resultMsg = "";

            try
            {
                await userService.Create(new UserCreationDTO(username));
                username = "";
                resultMsg = "User successfully created";
                color = "green";
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                resultMsg = e.Message;
                color = "red";
            }
        }
    
    }
}
