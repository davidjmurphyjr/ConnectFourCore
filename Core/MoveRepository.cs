using System;
using System.Collections.Generic;
using System.Linq;

namespace Core
{
    public interface IMoveRepository
    {
        void Add(Move move);
        IEnumerable<Move>  GetAll(Guid gameId);
    }

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
}