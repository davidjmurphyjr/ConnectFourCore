using System;
using System.Collections.Generic;
using System.Linq;

namespace Core
{
    public class Game
    {
        private readonly List<int> _moves = new List<int>();
        private Token TokenToDrop => _moves.Count % 2 == 0 ? Token.Red : Token.Yellow;
        public int PendingMoveNumber => _moves.Count + 1;
        public Board Board { get; } = new Board();
        public Token? Winner { get; private set; }

        public static Game Create(IEnumerable<int> moves)
        {
            var game = new Game();
            foreach (var move in moves)
            {
                game.MakeMove(move);
            }
            return game;
        }

        public bool CanMakeMove(int columnNumber)
        {
            var rowNumber =  Board.Column(columnNumber).Drop(TokenToDrop);
            return rowNumber != null;
        }

        public void MakeMove(int columnNumber)
        {
            var rowNumber =  Board.Column(columnNumber).Drop(TokenToDrop);
            if (rowNumber == null)
            {
                throw new ArgumentException("Invalid move");
            }
            if (Board.HasConnectionAt(columnNumber, rowNumber.Value))
            {
                Winner = TokenToDrop;
            }
            _moves.Add(columnNumber);
        }
    }
}