using Xunit;

namespace Core.Tests
{
    public class GameTests
    {
        [Fact]
        public void MakeMove()
        {
            var game = new Game();
            game.MakeMove(0, Token.Black);
            Assert.True(true);
        }

        [Fact]
        public void Test_simple_win_vertical()
        {
            var game = new Game();
            game.MakeMove(0, Token.Black);
            game.MakeMove(1, Token.Red);
            game.MakeMove(0, Token.Black);
            game.MakeMove(1, Token.Red);
            game.MakeMove(0, Token.Black);
            game.MakeMove(1, Token.Red);
            Assert.Null(game.Winner);
            game.MakeMove(0, Token.Black);
            Assert.True(game.Winner == Token.Black);
        }

        [Fact]
        public void Test_simple_win_vertical_at_the_top()
        {
            var game = new Game();
            game.MakeMove(0, Token.Black);
            game.MakeMove(0, Token.Red);
            game.MakeMove(0, Token.Black);
            game.MakeMove(1, Token.Red);
            game.MakeMove(0, Token.Black);
            game.MakeMove(1, Token.Red);
            game.MakeMove(0, Token.Black);

            Assert.Null(game.Winner);
            game.MakeMove(0, Token.Black);
            Assert.True(game.Winner == Token.Black);
        }

        [Fact]
        public void Test_simple_win_horizontal()
        {
            var game = new Game();
            game.MakeMove(0, Token.Black);
            game.MakeMove(0, Token.Red);
            game.MakeMove(1, Token.Black);
            game.MakeMove(1, Token.Red);
            game.MakeMove(2, Token.Black);
            game.MakeMove(2, Token.Red);

            Assert.Null(game.Winner);
            game.MakeMove(3, Token.Black);
            Assert.True(game.Winner == Token.Black);
        }

        [Fact]
        public void Test_simple_win_horizontal_on_the_right()
        {
            var game = new Game();
            game.MakeMove(6, Token.Black);
            game.MakeMove(6, Token.Red);
            game.MakeMove(5, Token.Black);
            game.MakeMove(5, Token.Red);
            game.MakeMove(4, Token.Black);
            game.MakeMove(4, Token.Red);

            Assert.Null(game.Winner);
            game.MakeMove(3, Token.Black);
            Assert.True(game.Winner == Token.Black);
        }

        [Fact]
        public void Test_simple_win_diagonal_up()
        {
            var game = new Game();
            game.MakeMove(0, Token.Black);
            game.MakeMove(1, Token.Red);
            game.MakeMove(1, Token.Black);
            game.MakeMove(2, Token.Red);
            game.MakeMove(2, Token.Black);
            game.MakeMove(3, Token.Red);
            game.MakeMove(2, Token.Black);
            game.MakeMove(3, Token.Red);
            game.MakeMove(3, Token.Black);
            game.MakeMove(0, Token.Red);

            Assert.Null(game.Winner);
            game.MakeMove(3, Token.Black);
            Assert.True(game.Winner == Token.Black);
        }

        [Fact]
        public void Test_simple_win_diagonal_down()
        {
            var game = new Game();
            game.MakeMove(6, Token.Black);
            game.MakeMove(5, Token.Red);
            game.MakeMove(5, Token.Black);
            game.MakeMove(4, Token.Red);
            game.MakeMove(4, Token.Black);
            game.MakeMove(3, Token.Red);
            game.MakeMove(4, Token.Black);
            game.MakeMove(3, Token.Red);
            game.MakeMove(3, Token.Black);
            game.MakeMove(0, Token.Red);

            Assert.Null(game.Winner);
            game.MakeMove(3, Token.Black);
            Assert.True(game.Winner == Token.Black);
        }
    }
}