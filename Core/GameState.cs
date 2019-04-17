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
        public readonly IEnumerable<Space>[] Columns = Enumerable.Range(0, NumberOfColumns).Select(columnNUmber =>
            Enumerable.Range(0, NumberOfRows).Select(rowNumber => new Space(rowNumber)).ToList()).ToArray();
    }
}