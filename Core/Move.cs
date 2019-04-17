using System;

namespace Core
{
    public class Move
    {
        public Guid GameId { get; set; }
        public int MoveNumber { get; set; }
        public int ColumnNumber { get; set; }

        public static Move Copy(Move move)
        {
            return new Move
            {
                GameId = move.GameId,
                MoveNumber = move.MoveNumber,
                ColumnNumber = move.ColumnNumber
            };
        }
    }
}