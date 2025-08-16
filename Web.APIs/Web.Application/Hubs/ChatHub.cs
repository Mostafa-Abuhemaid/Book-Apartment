using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace Web.Application.Hubs
{
   
    public class ChatHub : Hub
    {
       
        public async Task SendMessage(string receiverUserId, object message)
        {
            await Clients.User(receiverUserId).SendAsync("ReceiveMessage", message);
        }

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await base.OnDisconnectedAsync(exception);
        }
    }
}
