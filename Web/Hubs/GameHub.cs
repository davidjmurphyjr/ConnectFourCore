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
        private readonly IMoveRepository _moveRepository;


        public GameHub(IMoveRepository moveRepository, IGameService gameService)
        {
            _moveRepository = moveRepository;
            _gameService = gameService;
        }

        public async Task GetGameState(Guid gameId)
        {
            var gameState =  _gameService.GetGameState(gameId);
            await Clients.All.SendAsync("GetGameStateResponse", gameState);
        }
        
        public void  MakeMove(Guid gameId, int columnNumber)
        {
           _gameService.MakeMove(gameId, columnNumber);
        }
    }
}