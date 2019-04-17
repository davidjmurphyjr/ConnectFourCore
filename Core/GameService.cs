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
            if (gameState.Winner.HasValue)
            {
                return;
            }

            var moveColumnHasEmptySpace = gameState.Columns[columnNumber].Any(t => t == null);
            if (!moveColumnHasEmptySpace)
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

        private Token MoveNumberToToken(int moveNumber)
        {
            return moveNumber % 2 == 0 ? Token.Red : Token.Yellow;
        }

        public GameState GetGameState(Guid gameId)
        {
            var moves = _moveRepository.GetAll(gameId).OrderBy(m => m.MoveNumber).ToList();
            var gameState = new GameState
            {
                NumberOfMovesMade = moves.Count
            };
            foreach (var move in moves)
            {
                var moveColumn = gameState.Columns[move.ColumnNumber];
                var rowNumberOfFirstEmptySpace = Array.IndexOf(moveColumn, null);
                var tokenToDrop = MoveNumberToToken(move.MoveNumber);
                moveColumn[rowNumberOfFirstEmptySpace] = tokenToDrop;

                if (moves.Count >= 7)
                {
                    var hasConnection = HasConnectionAt(gameState, move.ColumnNumber, rowNumberOfFirstEmptySpace);
                    if (hasConnection)
                    {
                        gameState.Winner = tokenToDrop;
                        return gameState;
                    }
                }
            }

            gameState.TokenToDrop = MoveNumberToToken(moves.Count);
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
            var sequenceColor = gameState.Columns[columnNumber][rowNumber];
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
                    && gameState.Columns[colToCheck][rowToCheck] == sequenceColor)
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