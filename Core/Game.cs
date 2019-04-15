using System;
using System.Collections.Generic;
using System.Linq;

namespace Core
{
    public class Game
    {
        private readonly List<Move> _moves = new List<Move>();
        private Token TokenToDrop => _moves.Count() % 2 == 0 ? Token.Yellow : Token.Red;
        public int PendingMoveNumber => _moves.Count() + 1;
        public Board Board { get; } = new Board();
        public Token? Winner { get; private set; }

        public static Game Create(IEnumerable<Move> moves)
        {
            var game = new Game();
            foreach (var move in moves)
            {
                game.MakeMove(move);
            }
            return game;
        }

        public bool CanMakeMove(Move move)
        {
            var rowNumber =  Board.Column(move.ColumnNumber).Drop(this.TokenToDrop);
            return rowNumber != null;
        }

        private void MakeMove(Move move)
        {
            var rowNumber =  Board.Column(move.ColumnNumber).Drop(this.TokenToDrop);
            if (rowNumber == null)
            {
                throw new ArgumentException("invalid move");
            }
            _moves.Add(move);
        }
    }
}