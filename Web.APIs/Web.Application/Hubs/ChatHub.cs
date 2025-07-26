using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Web.Application.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string senderUserId, string receiverUserId, string message)
        {
            await Clients.User(receiverUserId).SendAsync("ReceiveMessage", senderUserId, message);
        }

      
    }
}
