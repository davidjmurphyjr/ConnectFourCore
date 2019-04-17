using System;
using System.Linq;

namespace Core
{
    public interface IGameService
    {
        void MakeMove(Guid gameId, int columnNumber);
        GameState GetGameState(Guid gameId);
    }
    public class GameService : IGameService
    {
        private readonly IMoveRepository _moveRepository;

        public GameService(IMoveRepository moveRepository)
        {
            _moveRepository = moveRepository;
        }

        public void MakeMove(Guid gameId, int columnNumber)
        {
            var gameState = GetGameState(gameId);
            var canMakeMove = CanMakeMove(gameState, columnNumber);
            if (!canMakeMove)
            {
                return;
            }

            var move = new Move
            {
                GameId = gameId,
                MoveNumber = gameState.NumberOfMovesMade,
                ColumnNumber = columnNumber
            };
            _moveRepository.Add(move);
        }

        private bool CanMakeMove(GameState gameState, int columnNumber)
        {
            var anyEmptySpacesInColumn = gameState.Columns[columnNumber]
                .OrderBy(s => s.RowNumber)
                .Any(s => s.Token == null);
            return anyEmptySpacesInColumn;
        }

        public GameState GetGameState(Guid gameId)
        {
            var moves = _moveRepository.GetAll(gameId).OrderBy(m => m.MoveNumber).ToList();
            var gameState = new GameState();
            foreach (var move in moves)
            {
                var column = gameState.Columns[move.ColumnNumber];
                var firstEmpty = column
                    .OrderBy(s => s.RowNumber)
                    .First(s => s.Token == null);
                var tokenToDrop = move.MoveNumber % 2 == 0 ? Token.Red : Token.Yellow;
                firstEmpty.Token = tokenToDrop;

                var hasConnection = HasConnectionAt(gameState, move.ColumnNumber, firstEmpty.RowNumber);
                if (hasConnection)
                {
                    gameState.Winner = tokenToDrop;
                    break;
                }
            }

            gameState.NumberOfMovesMade = moves.Count;
            return gameState;
        }

        public bool HasConnectionAt(GameState gameState, int columnNumber, int rowNumber)
        {
            return CheckForConnectionAt(gameState, columnNumber, rowNumber, SearchPattern.Vertical)
                   || CheckForConnectionAt(gameState, columnNumber, rowNumber, SearchPattern.Horizontal)
                   || CheckForConnectionAt(gameState, columnNumber, rowNumber, SearchPattern.DiagonalUp)
                   || CheckForConnectionAt(gameState, columnNumber, rowNumber, SearchPattern.DiagonalDown);
        }

        private bool CheckForConnectionAt(GameState gameState, int columnNumber, int rowNumber, SearchPattern searchPattern)
        {
            var sequenceColor = gameState.Columns[columnNumber].First(c => c.RowNumber == rowNumber).Token;
            var connected = 1;
            var colToCheck = columnNumber;
            var rowToCheck = rowNumber;
            var rowDelta = 0;
            var colDelta = 0;
            var checkedReverseDirection = false;
            switch (searchPattern)
            {
                case SearchPattern.Horizontal:
                    colDelta = 1;
                    break;
                case SearchPattern.Vertical:
                    rowDelta = 1;
                    break;
                case SearchPattern.DiagonalUp:
                    rowDelta = 1;
                    colDelta = 1;
                    break;
                case SearchPattern.DiagonalDown:
                    rowDelta = -1;
                    colDelta = 1;
                    break;
            }

            while (connected < 4)
            {
                rowToCheck = rowToCheck + rowDelta;
                colToCheck = colToCheck + colDelta;
                if (colToCheck < GameState.NumberOfColumns
                    && colToCheck >= 0
                    && rowToCheck < GameState.NumberOfRows
                    && rowToCheck >= 0
                    && gameState.Columns[colToCheck].First(s => s.RowNumber == rowToCheck).Token == sequenceColor)
                {
                    connected++;
                }
                else
                {
                    if (!checkedReverseDirection)
                    {
                        checkedReverseDirection = true;
                        colToCheck = columnNumber;
                        rowToCheck = rowNumber;
                        rowDelta = rowDelta * -1;
                        colDelta = colDelta * -1;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            return connected == 4;
        }
    }
}