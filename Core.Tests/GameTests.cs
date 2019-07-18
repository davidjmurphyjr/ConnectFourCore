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
            var gameService = new GameService();
            var gameState = gameService.GetGameState(new List<int>{0});
            Assert.Equal(Token.Red, gameState.Columns[0][0]);
        }

        [Fact]
        public void VerticalWin()
        {
            var gameService = new GameService();
            var moves = new List<int> {0, 1, 0, 1, 0, 1};
            gameService.GetGameState(moves);
            var gameState =  gameService.GetGameState(moves);
            Assert.Null(gameState.Winner);
            moves.Add(0);
            gameState =  gameService.GetGameState(moves);
            Assert.Equal(Token.Red, gameState.Winner);
        }

        [Fact]
        public void HorizontalWin()
        {
            var gameService = new GameService();
            var moves = new List<int> {0, 0, 1, 1, 2, 2};
            Assert.Null(gameService.GetGameState(moves).Winner);
            moves.Add(3);
            Assert.Equal(Token.Red, gameService.GetGameState(moves).Winner);
        }

        [Fact]
        public void HorizontalOnTheRightWin()
        {
            var gameService = new GameService();
            var moves = new List<int> {6, 6, 5, 5, 4, 4};

            Assert.Null(gameService.GetGameState(moves).Winner);
            moves.Add(3);
            Assert.Equal(Token.Red, gameService.GetGameState(moves).Winner);
        }

        [Fact]
        public void DiagonalUpWin()
        {
            var gameService = new GameService();

            var moves = new List<int> { 0, 1, 1, 2, 2, 3, 2, 3, 3, 0 };

            var gameState = gameService.GetGameState(moves);
            Assert.Null(gameState.Winner);
            moves.Add(3);
            Assert.Equal(Token.Red, gameService.GetGameState(moves).Winner);
        }

        [Fact]
        public void DiagonalDownWin()
        {
            var gameService = new GameService();
            var moves = new List<int> {6, 5, 5, 4, 4, 3, 4, 3, 3, 0};
            Assert.Null(gameService.GetGameState(moves).Winner);
            moves.Add(3);
            Assert.Equal(Token.Red, gameService.GetGameState(moves).Winner);
        }
    }
}