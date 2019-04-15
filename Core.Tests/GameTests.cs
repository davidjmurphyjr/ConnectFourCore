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
            var game = new Game();
            game.MakeMove(0);
            Assert.True(true);
        }

        [Fact]
        public void Test_simple_win_vertical()
        {
            var game = new Game();
            game.MakeMove(0);
            game.MakeMove(1);
            game.MakeMove(0);
            game.MakeMove(1);
            game.MakeMove(0);
            game.MakeMove(1);
            Assert.Null(game.Winner);
            game.MakeMove(0);
            Assert.True(game.Winner == Token.Red);
        }

        [Fact]
        public void Test_simple_win_horizontal()
        {
            var game = new Game();
            game.MakeMove(0);
            game.MakeMove(0);
            game.MakeMove(1);
            game.MakeMove(1);
            game.MakeMove(2);
            game.MakeMove(2);

            Assert.Null(game.Winner);
            game.MakeMove(3);
            Assert.True(game.Winner == Token.Red);
        }

        [Fact]
        public void Test_simple_win_horizontal_on_the_right()
        {
            var game = new Game();
            game.MakeMove(6);
            game.MakeMove(6);
            game.MakeMove(5);
            game.MakeMove(5);
            game.MakeMove(4);
            game.MakeMove(4);

            Assert.Null(game.Winner);
            game.MakeMove(3);
            Assert.True(game.Winner == Token.Red);
        }

        [Fact]
        public void Test_simple_win_diagonal_up()
        {
            var game = new Game();
            game.MakeMove(0);
            game.MakeMove(1);
            game.MakeMove(1);
            game.MakeMove(2);
            game.MakeMove(2);
            game.MakeMove(3);
            game.MakeMove(2);
            game.MakeMove(3);
            game.MakeMove(3);
            game.MakeMove(0);

            Assert.Null(game.Winner);
            game.MakeMove(3);
            Assert.True(game.Winner == Token.Red);
        }

        [Fact]
        public void Test_simple_win_diagonal_down()
        {
            var game = new Game();
            game.MakeMove(6);
            game.MakeMove(5);
            game.MakeMove(5);
            game.MakeMove(4);
            game.MakeMove(4);
            game.MakeMove(3);
            game.MakeMove(4);
            game.MakeMove(3);
            game.MakeMove(3);
            game.MakeMove(0);

            Assert.Null(game.Winner);
            game.MakeMove(3);
            Assert.True(game.Winner == Token.Red);
        }
    }
}