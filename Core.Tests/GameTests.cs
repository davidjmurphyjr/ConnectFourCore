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
            Assert.Null(gameService.GetGameState(gameId).Columns[0].First(s => s.RowNumber == 0).Token);
            gameService.MakeMove(gameId, 0);
            Assert.Equal(Token.Red, gameService.GetGameState(gameId).Columns[0].First(s => s.RowNumber == 0).Token);
        }

        [Fact]
        public void Test_simple_win_vertical()
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
        public void Test_simple_win_horizontal()
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
        public void Test_simple_win_horizontal_on_the_right()
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
        public void Test_simple_win_diagonal_up()
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
        public void Test_simple_win_diagonal_down()
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