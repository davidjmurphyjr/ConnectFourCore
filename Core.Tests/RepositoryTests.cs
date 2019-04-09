using System;
using Xunit;

namespace Core.Tests
{
    public class GameRepositoryTests
    {
        [Fact]
        public void Create()
        {
            var repo = new GameRepository();
            Assert.NotNull(repo);
            var gameId = repo.Create();
            Assert.IsType<Guid>(gameId);
        }

        [Fact]
        public void Get()
        {
            var repo = new GameRepository();
            Assert.NotNull(repo);
            var gameId = repo.Create();
            Assert.IsType<Guid>(gameId);

            var game = repo.Get(gameId);
            Assert.IsType<Game>(game);

        }

    }
}