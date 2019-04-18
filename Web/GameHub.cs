using System;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Microsoft.AspNetCore.SignalR;

namespace Web.Hubs
{
    public class GameHub : Hub
    {
        private readonly IGameService _gameService;

        public GameHub(IGameService gameService)
        {
            _gameService = gameService;
        }

        public async Task GetGameState(Guid gameId)
        {
            var gameState =  _gameService.GetGameState(gameId);
            await Clients.All.SendAsync("GameStateAnnounce", gameId, gameState);
        }
        
        public void  MakeMove(Guid gameId, int columnNumber)
        {
           _gameService.MakeMove(gameId, columnNumber);
        }
    }
}