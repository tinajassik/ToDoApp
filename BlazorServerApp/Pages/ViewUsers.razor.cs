using Domain.Models;
using Microsoft.AspNetCore.Components;

namespace BlazorServerApp.Pages
{
    public partial class ViewUsers : ComponentBase
    {
    
        private IEnumerable<User>? users;
        private string msg = "";

        protected override async Task OnInitializedAsync()
        {
            msg = "";
            try
            {
                users = await userService.GetUsersAsync();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                msg = e.Message;
            }
        }
    
    }
}

    

