using System;
using System.Threading.Tasks;
using Core;
using Microsoft.AspNetCore.SignalR;

namespace Web.Hubs
{
    public class GameHub : Hub
    {
         public async Task NewGame(string username, string message)
        {
            Guid gameId = Guid.NewGuid();
            var game = new Game();
            await Clients.All.SendAsync("gameCreated", game.Board);
        }
    }
}