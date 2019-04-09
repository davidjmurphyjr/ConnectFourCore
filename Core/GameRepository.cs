using System;
using System.Collections.Generic;

namespace Core
{
    public class GameRepository
    {
        private Dictionary<Guid, Game> _games = new Dictionary<Guid, Game>();

        public Guid Create()
        {
            var game = new Game();
            var gameId = Guid.NewGuid();
            _games.Add(gameId, game);

            return  gameId;
        }

        public Game Get(Guid gameId)
        {
            var game = _games[gameId];
            return game;
        }
    }
}