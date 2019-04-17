using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Core.Tests
{
    public class GameTests
    {
        [Fact]
        public void MakeMove()
        {
            var gameService = new GameService(new MoveRepository());
            var gameId = Guid.NewGuid();
            Assert.Null(gameService.GetGameState(gameId).Columns[0][0]);
            gameService.MakeMove(gameId, 0);
            Assert.Equal(Token.Red, gameService.GetGameState(gameId).Columns[0][0]);
        }

        [Fact]
        public void VerticalWin()
        {
            var gameService = new GameService(new MoveRepository());
            var gameId = Guid.NewGuid();
            gameService.MakeMove(gameId, 0);
            gameService.MakeMove(gameId, 1);
            gameService.MakeMove(gameId, 0);
            gameService.MakeMove(gameId, 1);
            gameService.MakeMove(gameId, 0);
            gameService.MakeMove(gameId, 1);

            Assert.Null(gameService.GetGameState(gameId).Winner);
            gameService.MakeMove(gameId, 0);
            Assert.Equal(Token.Red, gameService.GetGameState(gameId).Winner);
        }

        [Fact]
        public void HorizontalWin()
        {
            var gameService = new GameService(new MoveRepository());
            var gameId = Guid.NewGuid();
            gameService.MakeMove(gameId, 0);
            gameService.MakeMove(gameId, 0);
            gameService.MakeMove(gameId, 1);
            gameService.MakeMove(gameId, 1);
            gameService.MakeMove(gameId, 2);
            gameService.MakeMove(gameId, 2);

            Assert.Null(gameService.GetGameState(gameId).Winner);
            gameService.MakeMove(gameId, 3);
            Assert.Equal(Token.Red, gameService.GetGameState(gameId).Winner);
        }

        [Fact]
        public void HorizontalOnTheRightWin()
        {
            var gameService = new GameService(new MoveRepository());
            var gameId = Guid.NewGuid();
            gameService.MakeMove(gameId, 6);
            gameService.MakeMove(gameId, 6);
            gameService.MakeMove(gameId, 5);
            gameService.MakeMove(gameId, 5);
            gameService.MakeMove(gameId, 4);
            gameService.MakeMove(gameId, 4);

            Assert.Null(gameService.GetGameState(gameId).Winner);
            gameService.MakeMove(gameId, 3);
            Assert.Equal(Token.Red, gameService.GetGameState(gameId).Winner);
        }

        [Fact]
        public void DiagonalUpWin()
        {
            var gameService = new GameService(new MoveRepository());
            var gameId = Guid.NewGuid();
            gameService.MakeMove(gameId, 0);
            gameService.MakeMove(gameId, 1);
            gameService.MakeMove(gameId, 1);
            gameService.MakeMove(gameId, 2);
            gameService.MakeMove(gameId, 2);
            gameService.MakeMove(gameId, 3);
            gameService.MakeMove(gameId, 2);
            gameService.MakeMove(gameId, 3);
            gameService.MakeMove(gameId, 3);
            gameService.MakeMove(gameId, 0);

            Assert.Null(gameService.GetGameState(gameId).Winner);
            gameService.MakeMove(gameId, 3);
            Assert.Equal(Token.Red, gameService.GetGameState(gameId).Winner);
        }

        [Fact]
        public void DiagonalDownWin()
        {
            var gameService = new GameService(new MoveRepository());
            var gameId = Guid.NewGuid();
            gameService.MakeMove(gameId, 6);
            gameService.MakeMove(gameId, 5);
            gameService.MakeMove(gameId, 5);
            gameService.MakeMove(gameId, 4);
            gameService.MakeMove(gameId, 4);
            gameService.MakeMove(gameId, 3);
            gameService.MakeMove(gameId, 4);
            gameService.MakeMove(gameId, 3);
            gameService.MakeMove(gameId, 3);
            gameService.MakeMove(gameId, 0);

            Assert.Null(gameService.GetGameState(gameId).Winner);
            gameService.MakeMove(gameId, 3);
            Assert.Equal(Token.Red, gameService.GetGameState(gameId).Winner);
        }
    }
}