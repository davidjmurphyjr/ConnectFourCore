using System;
using System.Collections.Generic;
using System.Linq;

namespace Core
{
    public class MoveRepository : IMoveRepository
    {
        private List<Move> _moves = new List<Move>();

        public void Add(Move move)
        {
            _moves.Add(move);
        }

        public IEnumerable<Move> GetAll(Guid gameId)
        {
            return _moves.Where(m => m.GameId == gameId).OrderBy(m => m.MoveNumber).Select(Move.Copy);
        }
    }

    public interface IMoveRepository
    {
        void Add(Move move);
        IEnumerable<Move>  GetAll(Guid gameId);
    }

    public class Move
    {
        public Guid GameId { get; set; }
        public string Username { get; set; }
        public int MoveNumber { get; set; }
        public int ColumnNumber { get; set; }

        public static Move Copy(Move move)
        {
            return new Move
            {
                GameId = move.GameId,
                Username = move.Username,
                MoveNumber = move.MoveNumber,
                ColumnNumber = move.ColumnNumber
            };
        }
    }
}