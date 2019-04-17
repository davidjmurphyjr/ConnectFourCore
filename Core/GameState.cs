using System;
using System.Collections.Generic;
using System.Linq;

namespace Core
{
    public class GameState
    {
        public const int NumberOfRows = 6;
        public const int NumberOfColumns = 7;
        public Token? Winner { get; set; }
        public int NumberOfMovesMade { get; set; }

        public readonly IEnumerable<Space> Spaces = Enumerable.Range(0, NumberOfRows).SelectMany(rowNumber =>
            Enumerable.Range(0, NumberOfColumns).Select(columnNumber => new Space(rowNumber, columnNumber))).ToList();

        public Token?[][] Board {
            get {
                var board = new Token?[NumberOfColumns][];
                for (var columnNUmber = 0; columnNUmber < NumberOfColumns; columnNUmber++)
                {
                    board[columnNUmber] = Spaces
                        .Where(s => s.ColumnNumber == columnNUmber)
                        .OrderBy(s => s.RowNumber)
                        .Select(s => s.Token).ToArray();
                }

                return board;
            }
        }
    }
}