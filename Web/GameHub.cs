using System;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Microsoft.AspNetCore.SignalR;

namespace Web
{
    public class GameHub : Hub
    {
        private readonly IGameService _gameService;
        private readonly IMoveRepository _moveRepository;
        
        public GameHub(IGameService gameService, IMoveRepository moveRepository)
        {
            _gameService = gameService;
            _moveRepository = moveRepository;
        }

        public async Task GetGameState(Guid gameId)
        {
            var moves = _moveRepository.GetAll(gameId).OrderBy(m => m.MoveNumber).ToList();
            var gameState =  _gameService.GetGameState(moves.Select(m => m.ColumnNumber).ToList());
            await Clients.All.SendAsync("GameStateAnnounce", gameId, gameState);
        }
        
        public void  MakeMove(Guid gameId, int columnNumber)
        {
            var moves = _moveRepository.GetAll(gameId).OrderBy(m => m.MoveNumber).ToList();
            var gameState = _gameService.GetGameState(moves.Select(m => m.ColumnNumber).ToList());
            if (!_gameService.CanMakeMove(gameState, columnNumber))
            {
                return;
            }

            var move = new Move
            {
                GameId = gameId,
                MoveNumber = gameState.NumberOfMovesMade,
                ColumnNumber = columnNumber
            };
            _moveRepository.Add(move);
        }
    }
}