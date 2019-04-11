using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System;

namespace SignalRWebPack.Hubs
{
    public class GameHub : Hub
    {
         public async Task NewGame(string username, string message)
        {
            await Clients.All.SendAsync("gameCreated", username, Guid.NewGuid().ToString());
        }
    }
}