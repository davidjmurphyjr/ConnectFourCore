using System;
using System.Linq;
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

        public async Task NewGame()
        {
            var gameId = Guid.NewGuid();
            var game = new Game();
            await Clients.All.SendAsync("gameCreated", gameId, game);
        }
        
        public async Task GetGameState(Guid gameId)
        {
            var moves = _moveRepository.GetAll(gameId).OrderBy(m => m.MoveNumber).Select(m => m.ColumnNumber);
            var game = Game.Create(moves);
            await Clients.All.SendAsync("GetGameStateResponse", gameId, game);
        }
        
        public void  MakeMove(Guid gameId, string username, int columnNumber)
        {
            var moves = _moveRepository.GetAll(gameId).OrderBy(m => m.MoveNumber).Select(m => m.ColumnNumber);
            var game = Game.Create(moves);
            
            var canMakeMove = game.CanMakeMove(columnNumber);
            if (!canMakeMove) return;
            var move = new Move
            {
                Username = username,
                GameId = gameId,
                MoveNumber = game.PendingMoveNumber,
                ColumnNumber = columnNumber
            };
            _moveRepository.Add(move);
        }
    }
}