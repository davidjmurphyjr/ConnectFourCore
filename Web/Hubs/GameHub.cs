using System;
using System.Threading.Tasks;
using Core;
using Microsoft.AspNetCore.SignalR;

namespace Web.Hubs
{
    public class GameHub : Hub
    {
        private readonly IMoveRepository _moveRepository;

        public GameHub(IMoveRepository moveRepository)
        {
            _moveRepository = moveRepository;
        }

        public async Task NewGame(string username, string message)
        {
            var gameId = Guid.NewGuid();
            var game = new Game();
            await Clients.All.SendAsync("gameCreated", gameId, game);
        }
        
        public async Task MakeMove(Guid gameId, string username, int columnNumber)
        {
            var moves = _moveRepository.GetAll(gameId);
            var game = Game.Create(moves);
            var move = new Move
            {
                Username = username,
                GameId = gameId,
                MoveNumber = game.PendingMoveNumber,
                ColumnNumber = columnNumber
            };
            var canMakeMove = game.CanMakeMove(move);
            if (canMakeMove)
            {
                _moveRepository.Add(move);
            }
            await Clients.All.SendAsync("updateGameState", game);
        }
    }
    
}