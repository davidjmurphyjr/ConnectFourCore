using System;
using System.Collections.Generic;
using Xunit;

namespace Core.Tests
{
    public class GameTests
    {
        [Fact]
        public void MakeMove()
        {
            var game = new GameBuilder();
            game.Create(0, Token.Yellow);
            Assert.True(true);
        }

        [Fact]
        public void Test_simple_win_vertical()
        {
            var game = new GameBuilder();
            game.Create(0, Token.Yellow);
            game.Create(1, Token.Red);
            game.Create(0, Token.Yellow);
            game.Create(1, Token.Red);
            game.Create(0, Token.Yellow);
            game.Create(1, Token.Red);
            Assert.Null(game.Winner);
            game.Create(0, Token.Yellow);
            Assert.True(game.Winner == Token.Yellow);
        }

        [Fact]
        public void Test_simple_win_horizontal()
        {
            var game = new GameBuilder();
            game.Create(0, Token.Yellow);
            game.Create(0, Token.Red);
            game.Create(1, Token.Yellow);
            game.Create(1, Token.Red);
            game.Create(2, Token.Yellow);
            game.Create(2, Token.Red);

            Assert.Null(game.Winner);
            game.Create(3, Token.Yellow);
            Assert.True(game.Winner == Token.Yellow);
        }

        [Fact]
        public void Test_simple_win_horizontal_on_the_right()
        {
            var game = new GameBuilder();
            game.Create(6, Token.Yellow);
            game.Create(6, Token.Red);
            game.Create(5, Token.Yellow);
            game.Create(5, Token.Red);
            game.Create(4, Token.Yellow);
            game.Create(4, Token.Red);

            Assert.Null(game.Winner);
            game.Create(3, Token.Yellow);
            Assert.True(game.Winner == Token.Yellow);
        }

        [Fact]
        public void Test_simple_win_diagonal_up()
        {
            var game = new GameBuilder();
            game.Create(0, Token.Yellow);
            game.Create(1, Token.Red);
            game.Create(1, Token.Yellow);
            game.Create(2, Token.Red);
            game.Create(2, Token.Yellow);
            game.Create(3, Token.Red);
            game.Create(2, Token.Yellow);
            game.Create(3, Token.Red);
            game.Create(3, Token.Yellow);
            game.Create(0, Token.Red);

            Assert.Null(game.Winner);
            game.Create(3, Token.Yellow);
            Assert.True(game.Winner == Token.Yellow);
        }

        [Fact]
        public void Test_simple_win_diagonal_down()
        {
            var game = new GameBuilder();
            game.Create(6, Token.Yellow);
            game.Create(5, Token.Red);
            game.Create(5, Token.Yellow);
            game.Create(4, Token.Red);
            game.Create(4, Token.Yellow);
            game.Create(3, Token.Red);
            game.Create(4, Token.Yellow);
            game.Create(3, Token.Red);
            game.Create(3, Token.Yellow);
            game.Create(0, Token.Red);

            Assert.Null(game.Winner);
            game.Create(3, Token.Yellow);
            Assert.True(game.Winner == Token.Yellow);
        }
    }

    public class GameBuilder
    {
        private readonly List<Move> _moves = new List<Move>();
        private Guid _gameId = Guid.NewGuid();
        public Token? Winner => Game.Create(_moves).Winner;

        public void Create(int columnNumber, Token token)
        {
            _moves.Add(new Move
            {
                GameId = _gameId,
                MoveNumber = _moves.Count,
                ColumnNumber = columnNumber,
                Username = "Tester"
            });
        }
    }
}